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
    public class GetAllConfigurationsQuery : IGetAllConfigurationsQuery
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public GetAllConfigurationsQuery(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Configuration>> Execute()
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                IEnumerable<ConfigurationDto> configurationFileDtos = await context.Configurations.ToListAsync();

                return configurationFileDtos.Select(y => new Configuration()
                {
                    ConfigurationName = y.ConfigurationName,
                    Id = y.Id,
                    IsVisible = y.IsVisible,
                    Projects = y.Projects,
                    RevitVersion = y.RevitVersion,
                });
            }
        }
    }
}
