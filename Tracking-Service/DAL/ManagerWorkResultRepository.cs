using System.Collections.Generic;
using EntytiModel;

namespace DAL
{
    public class ManagerWorkResultRepository : IModelRepository<WorkResults>
    {
        public void Add(WorkResults item)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(WorkResults item)
        {
            throw new System.NotImplementedException();
        }

        public void Update(WorkResults item)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<WorkResults> Items { get; private set; }
        public void SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}