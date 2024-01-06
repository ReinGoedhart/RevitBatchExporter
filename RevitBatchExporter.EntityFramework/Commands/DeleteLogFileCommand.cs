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
    public class DeleteLogFileCommand : IDeleteLogFileCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public DeleteLogFileCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(int configurationId)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                LogFileDto logFileDto = new LogFileDto()
                {
                    Id = configurationId
                };

                context.LogFiles.Remove(logFileDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
