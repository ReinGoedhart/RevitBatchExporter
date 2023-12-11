using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.EntityFramework;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public class DeleteProjectCommand : IDeleteProjectCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;

        public DeleteProjectCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(int projectId)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {
                ProjectDto projectDto = new ProjectDto()
                {
                    Id = projectId
                };

                context.Projects.Remove(projectDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
