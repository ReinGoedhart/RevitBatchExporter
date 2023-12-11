using Microsoft.EntityFrameworkCore;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.EntityFramework;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public class CreateConfigurationCommand : ICreateConfigurationCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public CreateConfigurationCommand(RevitBatchExporterDbContextFactory contextFactory)
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

                context.configurations.Add(configurationDto);
                await context.SaveChangesAsync();   
            }
        }
    }
}
