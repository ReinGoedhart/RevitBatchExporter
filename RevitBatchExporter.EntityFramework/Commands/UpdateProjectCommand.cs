using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public class UpdateProjectCommand : IUpdateProjectCommand
    {
        private readonly RevitBatchExporterDbContextFactory _contextFactory;
        public UpdateProjectCommand(RevitBatchExporterDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public Task Execute(Project project)
        {
            using (RevitBatchExporterDbContext context = _contextFactory.Create())
            {

            }
        }
    }
}
