using RevitBatchExporter.Frontend.Commands.ConfigurationCommand;
using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RevitBatchExporter.Frontend.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationItemsControlComponent
{
    public class ConfigurationItemsControlViewModel : ViewModelBase
    {
        private readonly ObservableCollection<ConfigurationItemsControlItemViewModel> _configurationItemsControlItemViewModel;
        public IEnumerable<ConfigurationItemsControlItemViewModel> ConfigurationItemsControlItemViewModel => _configurationItemsControlItemViewModel;


        private SelectedConfigurationStore _selectedConfigurationStore;
        public ConfigurationItemsControlViewModel(SelectedConfigurationStore selectedConfigurationStore)
        {
            _selectedConfigurationStore = selectedConfigurationStore;
            _configurationItemsControlItemViewModel = new ObservableCollection<ConfigurationItemsControlItemViewModel>();
            SetActiveConfiguration = new SetActiveConfigurationCommand(_selectedConfigurationStore);
            PopulateAndCategoriseConfigurations(/*_vm.ConfigurationService*/);
        }
        // Commands
        public ICommand SetActiveConfiguration { get; }

        public void PopulateAndCategoriseConfigurations(/*ConfigurationService configurationService*/)
        {
            List<Configuration> configurations = new List<Configuration>()
            {
                new Configuration() { Id = 1, ConfigurationName = "Rein", RevitVersion = Enums.Enums.RevitRelease.Revit2021 },
                new Configuration() { Id = 2, ConfigurationName = "Coen", RevitVersion = Enums.Enums.RevitRelease.Revit2022 },
                new Configuration() { Id = 3, ConfigurationName = "Jolie", RevitVersion = Enums.Enums.RevitRelease.Revit2023 },
                new Configuration() { Id = 4, ConfigurationName = "Jan", RevitVersion = Enums.Enums.RevitRelease.Revit2024 },
                new Configuration() { Id = 4, ConfigurationName = "Caroline", RevitVersion = Enums.Enums.RevitRelease.Revit2020 },
                new Configuration() { Id = 4, ConfigurationName = "Aniek", RevitVersion = Enums.Enums.RevitRelease.Revit2021 },
            };

            var list = configurations.GroupBy(c => c.RevitVersion).OrderBy(g => g.Key).ToList();

            foreach (var item in list)
            {
                ConfigurationItemsControlItemViewModel itemsControlItem = new ConfigurationItemsControlItemViewModel(item);
                _configurationItemsControlItemViewModel.Add(itemsControlItem);
            }

            //    if (_configurationItemsControlItemViewModel.Count > 0)
            //    {
            //        _configurationItemsControlItemViewModel.Clear();
            //    }
            //    var groupedConfigurations = configurationService.GetAllProjects()
            //                                                    .GroupBy(c => c.RevitVersion)
            //                                                    .OrderBy(g => g.Key);
            //    foreach (var group in groupedConfigurations)
            //    {
            //        ConfigurationItemsControlItemViewModel itemsControlItem = new ConfigurationItemsControlItemViewModel(group);
            //        _configurationItemsControlItemViewModel.Add(itemsControlItem);
            //    }


        }
    }
}
