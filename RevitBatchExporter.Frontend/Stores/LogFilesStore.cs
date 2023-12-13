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

namespace RevitBatchExporter.Frontend.Stores
{
    public class LogFilesStore
    {


        private readonly IGetAllLogFilesQuery _getAllLogFilesQuery;
        private readonly IUpdateLogFileCommand _updateLogFileCommand;
        private readonly ICreateLogFileCommand _createLogFileCommand;
        private readonly IDeleteLogFileCommand _deleteLogFileCommand;

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

        public async Task Create(LogFile logFile)
        {
            await _createLogFileCommand.Execute(logFile);
            LogFileCreated?.Invoke(logFile);
        }
        public async Task Update(LogFile logFile)
        {
            await _updateLogFileCommand.Execute(logFile);
            LogFileUpdated?.Invoke(logFile);
        }
        public async Task Deleted(LogFile logFile)
        {
            await _deleteLogFileCommand.Execute(logFile.Id);
            LogFileDeleted?.Invoke(logFile.Id);
        }

        public async Task Load()
        {
            var logFiles = await _getAllLogFilesQuery.Execute();
        }
    }
}
