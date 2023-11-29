using RevitBatchExporter.Frontend.Commands.ConfigurationCommand;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationItemsControlComponent;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationListViewComponent;
using RevitBatchExporter.Frontend.Models;
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
    public class ConfigurationViewModel : ViewModelBase
    {
        public ConfigurationListViewModel ConfigurationListingViewModel { get; set; }
        public ConfigurationItemsControlViewModel ConfigurationItemsControlViewModel { get; set; }
        private SelectedConfigurationStore _selectedConfigurationStore { get; set; }
        private ErrorMessagesStore _errorMessagesStore { get; set; }

        private INavigationService _deleteModalNavigationService;
        private INavigationService _errorModalNavigationService;
        private INavigationService _exportModalNavigationService;

        public Configuration SelectedConfiguration => _selectedConfigurationStore.SelectedConfiguration;
        public bool HasSelectedConfiguration => SelectedConfiguration != null;

        //public ConfigurationService ConfigurationService { get; set; }
        public ConfigurationViewModel(
            SelectedConfigurationStore selectedConfigurationStore,
            ErrorMessagesStore errorMessagesStore,
            INavigationService deleteModalNavigationService,
            INavigationService errorModalNavigationService,
            INavigationService exportModalNavigationService)
        {
            //ConfigurationService = new ConfigurationService();
            _deleteModalNavigationService = deleteModalNavigationService;
            _errorModalNavigationService = errorModalNavigationService;
            _exportModalNavigationService = exportModalNavigationService;
            _selectedConfigurationStore = selectedConfigurationStore;
            _errorMessagesStore = errorMessagesStore;

            ConfigurationListingViewModel = new ConfigurationListViewModel(_selectedConfigurationStore);
            ConfigurationItemsControlViewModel = new ConfigurationItemsControlViewModel(_selectedConfigurationStore);

            DeleteConfiguration = new DeleteConfigurationCommand(this, _deleteModalNavigationService);
            BeginExport = new BeginExportCommand(this,_errorMessagesStore, _exportModalNavigationService, _errorModalNavigationService);

            _selectedConfigurationStore.ConfigurationChanged += OnConfigurationChanged;
        }

        private void OnConfigurationChanged(Configuration configuration)
        {
            OnPropertyChanged(nameof(SelectedConfiguration));
            OnPropertyChanged(nameof(HasSelectedConfiguration));
        }

        // Commands
        public ICommand BeginExport { get; }
        public ICommand DeleteConfiguration { get; }

    }
}
