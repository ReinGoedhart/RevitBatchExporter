using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.EntityFramework;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework.Commands
{
    public class UpdateLogFileCommand : IUpdateLogFileCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public UpdateLogFileCommand(RevitBatchExporterDbContextFactory contextFactory)
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
                    Configurations = logFile.Configurations,
                    ErrorsOccured = logFile.ErrorsOccured,
                    Projects = logFile.Projects,
                };

                context.LogFiles.Update(logFileDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
