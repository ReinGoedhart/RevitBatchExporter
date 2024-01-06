using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.Frontend.Commands.ConfigurationCommand;
using RevitBatchExporter.Frontend.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBatchExporter.Frontend.Stores.CRUDBaseClass;

namespace RevitBatchExporter.Frontend.Stores
{
    public class LogFilesStore: CrudBase<LogFile>
    {
        private readonly IGetAllLogFilesQuery _getAllLogFilesQuery;
        private readonly IUpdateLogFileCommand _updateLogFileCommand;
        private readonly ICreateLogFileCommand _createLogFileCommand;
        private readonly IDeleteLogFileCommand _deleteLogFileCommand;

        private readonly List<LogFile> _logFiles;
        public IEnumerable<LogFile> projects => _logFiles;

        public LogFilesStore(IGetAllLogFilesQuery getAllLogFilesQuery, ICreateLogFileCommand createLogFileCommand, IDeleteLogFileCommand deleteLogFileCommand, IUpdateLogFileCommand updateLogFileCommand)
        {
            _getAllLogFilesQuery = getAllLogFilesQuery;
            _updateLogFileCommand = updateLogFileCommand;
            _createLogFileCommand = createLogFileCommand;
            _deleteLogFileCommand = deleteLogFileCommand;
        }

        public event Action<LogFile> LogFileCreated;
        public event Action<LogFile> LogFileUpdated;
        public event Action<int> LogFileDeleted;
        public event Action<IEnumerable<LogFile>> GetAllLogFiles;
        public event Action LogFilesLoaded;

        public override async Task Create(LogFile logFile)
        {
            await _createLogFileCommand.Execute(logFile);
            LogFileCreated?.Invoke(logFile);
        }
        public override async Task Update(LogFile logFile)
        {
            await _updateLogFileCommand.Execute(logFile);
            LogFileUpdated?.Invoke(logFile);
        }
        public override async Task Deleted(LogFile logFile)
        {
            await _deleteLogFileCommand.Execute(logFile.Id);
            LogFileDeleted?.Invoke(logFile.Id);
        }

        public override async Task Load()
        {
            var logFiles = await _getAllLogFilesQuery.Execute();
            _logFiles.Clear();
            _logFiles.AddRange(projects);
            LogFilesLoaded?.Invoke();
        }
    }
}
