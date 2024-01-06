using Microsoft.Extensions.Configuration;
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
    public class DeleteConfigurationCommand : IDeleteConfigurationCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public DeleteConfigurationCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Configuration configuration)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                ConfigurationDto configurationDto = new ConfigurationDto()
                {
                    Id = configuration.Id,
                    ConfigurationName = configuration.ConfigurationName,
                    IsVisible = false,
                    Projects = configuration.Projects,
                    RevitVersion = configuration.RevitVersion,
                };

                context.Configurations.Update(configurationDto);
                await context.SaveChangesAsync();

            }
        }
    }
}
