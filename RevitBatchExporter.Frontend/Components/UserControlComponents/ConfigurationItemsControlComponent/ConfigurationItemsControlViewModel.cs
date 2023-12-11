using RevitBatchExporter.Frontend.Commands.ConfigurationCommand;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationItemsControlComponent
{
    public class ConfigurationItemsControlViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ConfigurationItemsControlItemViewModel> _configurationItemsControlItemViewModel;
        public IEnumerable<ConfigurationItemsControlItemViewModel> ConfigurationItemsControlItemViewModel => _configurationItemsControlItemViewModel;
        private SelectedConfigurationStore _selectedConfigurationStore;
        private CreateConfigurationStore _createConfigurationStore;
        private DeleteObjectsStore _deleteObjectsStore;

        List<Configuration> SeedDataForConfigurationsList { get; set; }

        public ConfigurationItemsControlViewModel(SelectedConfigurationStore selectedConfigurationStore, CreateConfigurationStore createConfigurationStore, DeleteObjectsStore deleteObjectsStore)
        {
            _createConfigurationStore = createConfigurationStore;
            _selectedConfigurationStore = selectedConfigurationStore;
            _deleteObjectsStore = deleteObjectsStore;
            _configurationItemsControlItemViewModel = new ObservableCollection<ConfigurationItemsControlItemViewModel>();
            SetActiveConfiguration = new SetActiveConfigurationCommand(_selectedConfigurationStore);

            SeedDataForConfigurationsList = new List<Configuration>();
            SeedDataForConfigurations();

            PopulateAndCategoriseConfigurations();

            _createConfigurationStore.OnCreatedConfiguration += OnCreatedConfiguration;
            _deleteObjectsStore.OnDeleteConfiguration += OnDeleteConfiguration;
        }

        public override void Dispose()
        {
            _createConfigurationStore.OnCreatedConfiguration -= OnCreatedConfiguration;
            _deleteObjectsStore.OnDeleteConfiguration -= OnDeleteConfiguration;
            base.Dispose();
        }

        private void OnDeleteConfiguration()
        {
            Configuration? selectedConfiguration = _selectedConfigurationStore.SelectedConfiguration;
            if (selectedConfiguration != null)
            {
                SeedDataForConfigurationsList.Remove(selectedConfiguration);
                PopulateAndCategoriseConfigurations();
            }
        }

        private void OnCreatedConfiguration(Configuration configuration)
        {
            SeedDataForConfigurationsList.Add(configuration);
            PopulateAndCategoriseConfigurations();
        }

        // Commands
        public ICommand SetActiveConfiguration { get; }

        public void PopulateAndCategoriseConfigurations()
        {
            _configurationItemsControlItemViewModel.Clear();
            var list = SeedDataForConfigurationsList.GroupBy(c => c.RevitVersion).OrderBy(g => g.Key).ToList();

            foreach (var item in list)
            {
                ConfigurationItemsControlItemViewModel itemsControlItem = new ConfigurationItemsControlItemViewModel(item);
                _configurationItemsControlItemViewModel.Add(itemsControlItem);
            }
        }

        private void SeedDataForConfigurations()
        {
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 1, ConfigurationName = "Rein", RevitVersion = RevitRelease.Revit2021 });
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 2, ConfigurationName = "Coen", RevitVersion = RevitRelease.Revit2022 });
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 3, ConfigurationName = "Jolie", RevitVersion = RevitRelease.Revit2023 });
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 4, ConfigurationName = "Jan", RevitVersion = RevitRelease.Revit2024 });
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 4, ConfigurationName = "Caroline", RevitVersion = RevitRelease.Revit2020 });
            SeedDataForConfigurationsList.Add(new Configuration() { Id = 4, ConfigurationName = "Aniek", RevitVersion = RevitRelease.Revit2021 });
        }

    }
}
