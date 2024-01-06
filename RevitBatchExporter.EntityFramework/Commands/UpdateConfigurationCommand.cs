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
    public class UpdateConfigurationCommand : IUpdateConfigurationCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public UpdateConfigurationCommand(RevitBatchExporterDbContextFactory contextFactory)
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
                    IsVisible = configuration.IsVisible,
                    Projects = configuration.Projects,
                    RevitVersion = configuration.RevitVersion,
                };
                context.Configurations.Update(configurationDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
