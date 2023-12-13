using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.ProjectCommands
{
    public class LoadProjectsCommand : AsyncCommandBase
    {
        private readonly ProjectsStore _projectsStore;

        public LoadProjectsCommand(ProjectsStore projectsStore)
        {
            _projectsStore = projectsStore;
        }

        public override async Task ExcecuteAsync(object parameter)
        {
            try
            {
                await _projectsStore.Load();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
