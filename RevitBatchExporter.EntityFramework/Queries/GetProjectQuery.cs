using Microsoft.EntityFrameworkCore;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework.Queries
{
    public class GetProjectQuery : IGetProjectQuery
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public GetProjectQuery(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Project> Execute(int Id)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                ProjectDto projectDto = await context.Projects.Where(x => x.Id == Id).FirstOrDefaultAsync();
                Project project = new Project()
                {
                    Id = projectDto.Id,
                    IsVisible = projectDto.IsVisible,
                    ProjectGuid = projectDto.ProjectGuid,
                    ModelGuid = projectDto.ModelGuid,
                    LocalModelPath = projectDto.LocalModelPath,
                    ProjectName = projectDto.ProjectName,
                    ViewName = projectDto.ViewName,
                    RevitVersion = projectDto.RevitVersion,
                    RevitExportType = projectDto.RevitExportType,
                    SaveAfterExport = projectDto.SaveAfterExport,
                    Configurations = projectDto.Configurations,
                    OutputName = projectDto.OutputName,
                    Region = projectDto.Region,
                    ConfigurationPath = projectDto.ConfigurationPath,
                };

                return project;
            }
        }
    }
}
