using Microsoft.EntityFrameworkCore;
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
                ConfigurationDto configurationDto;

                if (configuration.Id != 0)
                {
                    // Load existing configuration
                    configurationDto = await context.Configurations
                        .Include(c => c.Projects)
                        .FirstOrDefaultAsync(c => c.Id == configuration.Id);

                    // Clear existing projects to avoid duplicates
                    configurationDto.Projects.Clear();
                }
                else
                {
                    // Create a new configuration
                    configurationDto = new ConfigurationDto
                    {
                        ConfigurationName = configuration.ConfigurationName,
                        IsVisible = configuration.IsVisible,
                        RevitVersion = configuration.RevitVersion,
                        Projects = new List<Project>() // Initialize the projects list
                    };

                    context.Configurations.Add(configurationDto);
                }

                // Link existing ProjectDto entities
                foreach (var project in configuration.Projects)
                {
                    var projectDto = await context.Projects.FindAsync(project.Id);
                    if (projectDto != null)
                    {
                        // Convert ProjectDto to Project
                        var projectEntity = ConvertToProjectEntity(projectDto);
                        configurationDto.Projects.Add(projectEntity); // Add the Project entity
                    }
                }

                // Save changes
                await context.SaveChangesAsync();
            }
        }


        private Project ConvertToProjectEntity(ProjectDto projectDto)
        {
            return new Project
            {
                SaveAfterExport = projectDto.SaveAfterExport,
                ConfigurationPath = projectDto.ConfigurationPath,
                Configurations = projectDto.Configurations,
                Id = projectDto.Id,
                IsVisible = projectDto.IsVisible,
                LocalModelPath = projectDto.LocalModelPath,
                ModelGuid = projectDto.ModelGuid,
                OutputName = projectDto.OutputName,
                ProjectGuid = projectDto.ProjectGuid,
                ProjectName = projectDto.ProjectName,
                Region = projectDto.Region,
                RevitExportType = projectDto.RevitExportType,
                RevitVersion = projectDto.RevitVersion,
                ViewName = projectDto.ViewName,

            };
        }
    }
}
