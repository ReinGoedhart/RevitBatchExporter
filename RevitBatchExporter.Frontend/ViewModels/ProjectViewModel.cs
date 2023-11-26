using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectDataGridViewModel ProjectDataGridViewModel { get; set; }
        public ICommand DeleteModal {  get;}
        public ICommand CreateConfigurationModal {  get;}
        public ProjectViewModel(SelectedProjectStore selectedProjectStore, INavigationService deleteModalNavigationService, INavigationService CreateConfigurationModalNavigationService, INavigationService createEditProjectModalNavigationService)
        {
            ProjectDataGridViewModel = new ProjectDataGridViewModel(this, selectedProjectStore, createEditProjectModalNavigationService);
            ProjectDataGridViewModel.GetProjects();
            
            
            DeleteModal = new NavigateCommand(deleteModalNavigationService);
            CreateConfigurationModal = new NavigateCommand(CreateConfigurationModalNavigationService);

        }
    }
}
