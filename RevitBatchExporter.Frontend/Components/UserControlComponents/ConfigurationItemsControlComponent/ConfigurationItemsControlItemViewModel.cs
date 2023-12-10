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
    public class ConfigurationItemsControlItemViewModel : ViewModelBase
    {
        public string Version { get; set; }
        public ObservableCollection<Configuration> Configurations { get; set; }

        public ConfigurationItemsControlItemViewModel(IGrouping<RevitRelease, Configuration> groupedConfigs)
        {
            Version = $"Revit {groupedConfigs.Key}";
            Configurations = new ObservableCollection<Configuration>(groupedConfigs);

        }
    }
}
