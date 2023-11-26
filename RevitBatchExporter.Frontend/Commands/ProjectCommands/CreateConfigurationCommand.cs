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
    public class CreateConfigurationCommand : CommandBase
    {
        private ProjectViewModel _vm;
        public CreateConfigurationCommand(ProjectViewModel vm)
        {
            _vm = vm;
        }

        public override void Execute(object parameter)
        {
            //ConfigurationService configurationService = new ConfigurationService();
            //RevitConfigurationFactory configurationFactory = new RevitConfigurationFactory();
            //List<ProjectConfiguration> projectConfigurations = new List<ProjectConfiguration>();
            ProjectDataGridViewModel gridData = _vm.ProjectDataGridViewModel;


            foreach (ProjectDataGridItemViewModel dataGridItem in gridData._checkedProjects)
            {
                //projectConfigurations.Add(new ProjectConfiguration
                //{
                //    ProjectId = dataGridItem.Project.Id,
                //});
            }
            //var newConfig = configurationFactory.Create(_vm.ConfigurationNameText, gridData._checkedProjects.First().Project.RevitVersion, projectConfigurations);
            //configurationService.Insert(newConfig);
        }
    }
}
