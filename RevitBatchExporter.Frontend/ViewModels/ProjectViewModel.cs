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
        public ICommand DeleteModal { get; }
        public ICommand CreateConfigurationModal { get; }
        public INavigationService _configurationModalNavigationService;
        public INavigationService _errorModalNavigationService;
        private ErrorMessagesStore _errorMessagesStore;

        public ProjectViewModel(SelectedProjectStore selectedProjectStore, ErrorMessagesStore errorMessagesStore, INavigationService deleteModalNavigationService, INavigationService configurationModalNavigationService, INavigationService editProjectModalNavigationService, INavigationService errorModalNavigationService)
        {
            ProjectDataGridViewModel = new ProjectDataGridViewModel(this, selectedProjectStore, errorMessagesStore, editProjectModalNavigationService);
            
            _errorMessagesStore = errorMessagesStore;

            _configurationModalNavigationService = configurationModalNavigationService;
            _errorModalNavigationService = errorModalNavigationService;

            DeleteModal = new NavigateCommand(deleteModalNavigationService);
            CreateConfigurationModal = new RelayCommand(CanCreateConfigurationModal);
        }

        public void CanCreateConfigurationModal()
        {
            ProjectDataGridViewModel.ValidateCreateConfigurationCommand();
            if (_errorMessagesStore.CurrentErrorMessages.Count > 0)
            {
                _errorModalNavigationService.Navigate();
            }
            else
            {
                _configurationModalNavigationService.Navigate();
            }
        }
    }
}
