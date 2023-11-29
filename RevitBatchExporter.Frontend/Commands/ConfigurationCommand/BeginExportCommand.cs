using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.ViewModels;

namespace RevitBatchExporter.Frontend.Commands.ConfigurationCommand
{
    public class BeginExportCommand : CommandBase
    {
        private ConfigurationViewModel _vm;
        private ErrorMessagesStore _errorMessagesStore;
        private INavigationService _errorModalNavigationService;
        private INavigationService _exportModalNavigationService;

        public BeginExportCommand(ConfigurationViewModel vm, ErrorMessagesStore errorMessagesStore, INavigationService exportModalNavigationService, INavigationService errorModalNavigationService)
        {
            _vm = vm;
            _errorMessagesStore = errorMessagesStore;
            _errorModalNavigationService = errorModalNavigationService;
            _exportModalNavigationService = exportModalNavigationService;
        }

        public override void Execute(object parameter)
        {
            ValidateExportCommand(_vm.SelectedConfiguration);
            if (_errorMessagesStore.CurrentErrorMessages.Count > 0)
            {
                _errorModalNavigationService.Navigate();
            }
            else
            {
                _exportModalNavigationService.Navigate();
            }
        }
        public void ValidateExportCommand(Configuration configuration)
        {
            _errorMessagesStore.ClearErrorMessages();
        }
    }
}
