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
    public class GetAllProjectsQuery : IGetAllProjectsQuery
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public GetAllProjectsQuery(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Project>> Execute()
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                IEnumerable<ProjectDto> projectDtos = await context.Projects.Where(x=> x.IsVisible == true).ToListAsync();

                return projectDtos.Select(y => new Project()
                {
                    Id = y.Id,
                    IsVisible = y.IsVisible,
                    ProjectGuid =  y.ProjectGuid, 
                    ModelGuid = y.ModelGuid,
                    LocalModelPath = y.LocalModelPath,
                    ProjectName = y.ProjectName,
                    ViewName = y.ViewName,
                    RevitVersion = y.RevitVersion,
                    RevitExportType = y.RevitExportType,
                    SaveAfterExport = y.SaveAfterExport,
                    Configurations = y.Configurations,
                    OutputName = y.OutputName,
                    Region = y.Region,
                    ConfigurationPath = y.ConfigurationPath,
                });
            }
        }
    }
}
