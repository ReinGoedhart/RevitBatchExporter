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

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationListViewComponent
{
    public class ConfigurationListViewModel: ViewModelBase
    {
        public Configuration CurrentConfiguration { get; set; }

        private readonly ObservableCollection<ConfigurationListItemViewModel> _configurationProjectListingItemViewModel;
        public IEnumerable<ConfigurationListItemViewModel> ConfigurationProjectListingItemViewModel => _configurationProjectListingItemViewModel;

        private SelectedConfigurationStore _selectedConfigurationStore;
        public ConfigurationListViewModel(SelectedConfigurationStore selectedConfigurationStore)
        {
            _selectedConfigurationStore = selectedConfigurationStore;
            _configurationProjectListingItemViewModel = new ObservableCollection<ConfigurationListItemViewModel>();
            
            _selectedConfigurationStore.ConfigurationChanged += OnConfigurationChanged;
        }
        public override void Dispose()
        {
            _selectedConfigurationStore.ConfigurationChanged += OnConfigurationChanged;
            base.Dispose();
        }


        private void OnConfigurationChanged(Configuration currentConfiguration)
        {
            CurrentConfiguration = currentConfiguration;
            OnPropertyChanged(nameof(CurrentConfiguration));
            //populate(currentConfiguration);
        }

        private Configuration populate(Configuration currentConfiguration)
        {
            //_configurationProjectListingItemViewModel.Clear();

            //ConfigurationService configurationService = new ConfigurationService();
            //IEnumerable<Project> projectList = configurationService.GetProjects(id);
            //_currentConfiguration = configurationService.Get(id);
            //var Config = configurationService.Get(id);

            //foreach (var project in projectList)
            //{
            //    _configurationProjectListingItemViewModel.Add(new ConfigurationProjectListingItemViewModel(project));
            //}

            return currentConfiguration;

        }
    }
}
