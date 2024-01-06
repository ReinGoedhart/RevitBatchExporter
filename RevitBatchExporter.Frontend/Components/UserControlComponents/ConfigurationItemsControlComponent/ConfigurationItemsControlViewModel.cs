using RevitBatchExporter.Frontend.Commands.ConfigurationCommand;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static RevitBatchExporter.Domain.Enums.Enums;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.Services;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationItemsControlComponent
{
    public class ConfigurationItemsControlViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ConfigurationItemsControlItemViewModel> _configurationItemsControlItemViewModel;
        public IEnumerable<ConfigurationItemsControlItemViewModel> ConfigurationItemsControlItemViewModel => _configurationItemsControlItemViewModel;
        private SelectedConfigurationStore _selectedConfigurationStore;
        private ConfigurationsStore _configurationsStore;
        private DeleteObjectsStore _deleteObjectsStore;


        public ConfigurationItemsControlViewModel(SelectedConfigurationStore selectedConfigurationStore, ConfigurationsStore configurationsStore, DeleteObjectsStore deleteObjectsStore)
        {
            _configurationsStore = configurationsStore;
            _selectedConfigurationStore = selectedConfigurationStore;
            _deleteObjectsStore = deleteObjectsStore;
            _configurationItemsControlItemViewModel = new ObservableCollection<ConfigurationItemsControlItemViewModel>();
            SetActiveConfiguration = new SetActiveConfigurationCommand(_selectedConfigurationStore);

            LoadConfigurationsCommand = new LoadConfigurationsCommand(configurationsStore);


            _configurationsStore.ConfigurationCreated += OnCreatedConfiguration;
            _deleteObjectsStore.OnDeleteConfiguration += OnDeleteConfiguration;
            _configurationsStore.ConfigurationsLoaded += _configurationsStore_ConfigurationsLoaded;
        }

        private void _configurationsStore_ConfigurationsLoaded()
        {
            PopulateAndCategoriseConfigurations();
        }

        public static ConfigurationItemsControlViewModel LoadViewModel(SelectedConfigurationStore selectedConfigurationStore, ConfigurationsStore configurationsStore, DeleteObjectsStore deleteObjectsStore)
        {
            ConfigurationItemsControlViewModel viewModel = new ConfigurationItemsControlViewModel(selectedConfigurationStore, configurationsStore, deleteObjectsStore);
            viewModel.LoadConfigurationsCommand.Execute(null);
            return viewModel;
        }
        public ICommand LoadConfigurationsCommand { get; }

        public override void Dispose()
        {
            _configurationsStore.ConfigurationCreated -= OnCreatedConfiguration;
            _deleteObjectsStore.OnDeleteConfiguration -= OnDeleteConfiguration;
            _configurationsStore.ConfigurationsLoaded -= _configurationsStore_ConfigurationsLoaded;
            base.Dispose();
        }

        private void OnDeleteConfiguration()
        {
            new ConfigurationItemsControlViewModel(_selectedConfigurationStore, _configurationsStore, _deleteObjectsStore);
        }

        private void OnCreatedConfiguration(Configuration configuration)
        {
            new ConfigurationItemsControlViewModel(_selectedConfigurationStore, _configurationsStore, _deleteObjectsStore);
        }

        // Commands
        public ICommand SetActiveConfiguration { get; }

        public void PopulateAndCategoriseConfigurations()
        {
            _configurationItemsControlItemViewModel.Clear();
            var list = _configurationsStore.Configurations.GroupBy(c => c.RevitVersion).OrderBy(g => g.Key).ToList();

            foreach (var item in list)
            {
                ConfigurationItemsControlItemViewModel itemsControlItem = new ConfigurationItemsControlItemViewModel(item);
                _configurationItemsControlItemViewModel.Add(itemsControlItem);
            }
        }
    }
}
