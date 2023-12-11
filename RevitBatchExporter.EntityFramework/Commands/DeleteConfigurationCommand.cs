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
    public class DeleteConfigurationCommand : IDeleteConfigurationCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public DeleteConfigurationCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(int configurationId)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                ConfigurationDto configurationDto = new ConfigurationDto()
                {
                    Id = configurationId,
                };

                context.configurations.Remove(configurationDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
