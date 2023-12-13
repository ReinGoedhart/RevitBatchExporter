using Microsoft.Extensions.Configuration;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.EntityFramework;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public class CreateLogFileCommand : ICreateLogFileCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public CreateLogFileCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(LogFile logFile)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                LogFileDto logFileDto = new LogFileDto()
                {
                    LogFilePath = logFile.LogFilePath,
                    Configuration = logFile.Configuration,
                    ErrorsOccured = logFile.ErrorsOccured,
                    Projects = logFile.Projects,                    
                };

                context.LogFiles.Add(logFileDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
