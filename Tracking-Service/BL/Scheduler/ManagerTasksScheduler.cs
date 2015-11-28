using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BL.Scheduler
{
    public class ManagerTasksScheduler : TaskScheduler
    {
        [ThreadStatic] private static bool _currentThreadIsProcessingItems;

        private readonly int _maxDegreeOfParallelism;

        private readonly LinkedList<Task> _tasks = new LinkedList<Task>();

        private int _delegatesQueuedOrRunning;

        public ManagerTasksScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException("maxDegreeOfParallelism");
            _maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        public sealed override int MaximumConcurrencyLevel => _maxDegreeOfParallelism;

        protected override void QueueTask(Task task)
        {
            lock (_tasks)
            {
                _tasks.AddLast(task);

                if (_delegatesQueuedOrRunning >= _maxDegreeOfParallelism) return;

                ++_delegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }

        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                _currentThreadIsProcessingItems = true;
                try
                {
                    while (true)
                    {
                        Task item;
                        lock (_tasks)
                        {
                            if (_tasks.Count == 0)
                            {
                                --_delegatesQueuedOrRunning;
                                break;
                            }

                            item = _tasks.First.Value;
                            _tasks.RemoveFirst();
                        }

                        TryExecuteTask(item);
                    }
                }
                finally
                {
                    _currentThreadIsProcessingItems = false;
                }
            }, null);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!_currentThreadIsProcessingItems) return false;

            if (taskWasPreviouslyQueued)
                return TryDequeue(task) && TryExecuteTask(task);
            return TryExecuteTask(task);
        }

        protected sealed override bool TryDequeue(Task task)
        {
            lock (_tasks) return _tasks.Remove(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            var lockTaken = false;
            try
            {
                Monitor.TryEnter(_tasks, ref lockTaken);
                if (lockTaken) return _tasks;
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(_tasks);
            }
        }
    }
}