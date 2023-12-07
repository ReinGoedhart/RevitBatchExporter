using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.ProjectCommands
{
    public class CreateConfigurationModalCommand : CommandBase
    {
        private ErrorMessagesStore _errorMessagesStore;
        private INavigationService _errorModalNavigationService;
        private INavigationService _createConfigurationModalNavigationService;
        private ProjectDataGridViewModel _projectDataGridViewModel;

        public CreateConfigurationModalCommand(ErrorMessagesStore errorMessagesStore, INavigationService errorModalNavigationService, INavigationService createConfigurationModalNavigationService, ProjectDataGridViewModel projectDataGridViewModel)
        {
            _errorMessagesStore = errorMessagesStore;
            _errorModalNavigationService = errorModalNavigationService;
            _createConfigurationModalNavigationService = createConfigurationModalNavigationService;
            _projectDataGridViewModel = projectDataGridViewModel;
        }

        public override void Execute(object parameter)
        {
            _errorMessagesStore.ClearErrorMessages();
            _projectDataGridViewModel.ValidateCreateConfigurationCommand();
            if (_errorMessagesStore.CurrentErrorMessages.Count > 0)
            {
                _errorModalNavigationService.Navigate();
            }
            else
            {
                _createConfigurationModalNavigationService.Navigate();
            }
        }
    }
}
