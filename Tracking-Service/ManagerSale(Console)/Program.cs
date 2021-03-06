﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BL;
using BL.Parser;
using BL.Reader;
using BL.Scheduler;
using BL.SettingUpEnvironment;
using log4net;

namespace ManagerSale_Console_
{
    public static class Program
    {
        private static TaskFactory _taskFactory;
        private static FileHandler _fileHandler;

        private static string _serverFolderPath;
        private static string _wrongFilesFolderPath;
        private static string _processedFiles;
        private static int _maxNummberOfConnections;
        private static ILog _logger;

        private static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            _logger = LogManager.GetLogger("Console application");

            try
            {

                _fileHandler = new FileHandler(new ManagerSalesParser(), new CsvReader());

                _maxNummberOfConnections = int.Parse(ConfigurationManager.AppSettings["MaxDatabaseConnections"]);
                _serverFolderPath = ConfigurationManager.AppSettings["ServerFolder"];
                _wrongFilesFolderPath = ConfigurationManager.AppSettings["NotAppropriateFilesFolder"];
                _processedFiles = ConfigurationManager.AppSettings["ProcessedFilesFolder"];

                SettingUpEnvironment.SettingUpFolders(new List<string>
                {
                    _serverFolderPath,
                    _wrongFilesFolderPath,
                    _processedFiles
                });

                _taskFactory = new TaskFactory(new ManagerTasksScheduler(_maxNummberOfConnections));

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

            ScanFolder();

            var watcher = new FileSystemWatcher
            {
                Path = _serverFolderPath,
                Filter = "*csv"
            };

            watcher.Created += WatcherOnCreated;
            watcher.EnableRaisingEvents = true;

            Console.Read();
        }

        private static void ScanFolder()
        {
            var files = Directory.GetFiles(_serverFolderPath);
            foreach (var fileName in files.Select(file => file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1)))
            {
                WatcherOnCreated(null, new FileSystemEventArgs(WatcherChangeTypes.Created, _serverFolderPath, fileName));
            }
        }

        private static void WatcherOnCreated(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            _taskFactory.StartNew(() =>
            {
                try
                {
                    _logger.Info($"Begin process file {fileSystemEventArgs.Name}");
                    _fileHandler.ProcessFile(_serverFolderPath, fileSystemEventArgs.Name);

                    File.Copy(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name), Path.Combine(_processedFiles, fileSystemEventArgs.Name), true);
                    File.Delete(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name));
                    _logger.Info($"File {fileSystemEventArgs.Name} wath processed.");
                }
                catch (Exception ex)
                {
                    if (ex.Data["Status"] != null && (string) ex.Data["Status"] == "Processed")
                    {
                        File.Copy(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name),
                            Path.Combine(_processedFiles, fileSystemEventArgs.Name), true);
                        File.Delete(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name));
                        Console.WriteLine(ex.Message);
                        _logger.Error("Eroor occurs with file by path " + fileSystemEventArgs.FullPath + ". " + ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.Data["info"] + "\n" + ex.Message );
                        _logger.Error("Eroor occurs with file by path " + fileSystemEventArgs.FullPath + ". " + ex.Message);
                        File.Copy(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name), Path.Combine(_wrongFilesFolderPath, fileSystemEventArgs.Name), true);
                        File.Delete(Path.Combine(_serverFolderPath, fileSystemEventArgs.Name));
                    }
                }
            });
        }
    }
}