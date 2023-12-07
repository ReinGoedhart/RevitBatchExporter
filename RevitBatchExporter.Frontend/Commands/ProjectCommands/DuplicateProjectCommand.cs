using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.ProjectCommands
{
    public class DuplicateProjectCommand : CommandBase
    {
        private ProjectViewModel _projectViewModel;

        public DuplicateProjectCommand(ProjectViewModel projectViewModel)
        {
            _projectViewModel = projectViewModel;
        }

        public override void Execute(object parameter)
        {
            List<ProjectDataGridItemViewModel> checkedProjects = _projectViewModel.ProjectDataGridViewModel._checkedProjects;

            foreach (ProjectDataGridItemViewModel project in checkedProjects)
            {
                _projectViewModel.ProjectDataGridViewModel.DuplicateProject(project);
            }
            //InteractionWith Database




        }
    }
}
