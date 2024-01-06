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
    public class UpdateProjectCommand : IUpdateProjectCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;
        public UpdateProjectCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Project project)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                ProjectDto projectDto = new ProjectDto()
                {
                    ProjectGuid = project.ProjectGuid,
                    SaveAfterExport = project.SaveAfterExport,
                    ConfigurationPath = project.ConfigurationPath,
                    Configurations = project.Configurations,
                    Id = project.Id,
                    IsVisible = project.IsVisible,
                    LocalModelPath = project.LocalModelPath,
                    ModelGuid = project.ModelGuid,
                    OutputName = project.OutputName,
                    ProjectName = project.ProjectName,
                    Region = project.Region,
                    RevitExportType = project.RevitExportType,
                    RevitVersion = project.RevitVersion,
                    ViewName = project.ViewName,
                };

                context.Projects.Update(projectDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
