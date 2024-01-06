using Microsoft.EntityFrameworkCore;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.EntityFramework;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework.Queries
{
    public class GetAllLogFilesQuery : IGetAllLogFilesQuery
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public GetAllLogFilesQuery(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<LogFile>> Execute()
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                IEnumerable<LogFileDto> logFileDtos = await context.LogFiles.ToListAsync();

                return logFileDtos.Select(y => new LogFile()
                {
                   Configurations = y.Configurations,
                   ErrorsOccured = y.ErrorsOccured,
                   LogFilePath = y.LogFilePath,
                   Projects = y.Projects,
                });
            }
        }
    }
}
