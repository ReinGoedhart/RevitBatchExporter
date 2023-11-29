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
        private ConfigurationViewModel _vm;
        private Configuration _currentConfiguration;

        private readonly ObservableCollection<ConfigurationListItemViewModel> _configurationProjectListingItemViewModel;
        public IEnumerable<ConfigurationListItemViewModel> ConfigurationProjectListingItemViewModel => _configurationProjectListingItemViewModel;
  
        public ConfigurationListViewModel(SelectedConfigurationStore selectedConfigurationStore)
        {
            _configurationProjectListingItemViewModel = new ObservableCollection<ConfigurationListItemViewModel>();
            selectedConfigurationStore.ConfigurationChanged += OnConfigurationChanged;
        }

        private void OnConfigurationChanged(Configuration currentConfiguration)
        {

            //populate(currentConfiguration);
        }

        public Configuration GetCurrentConfiguration()
        {
            return _currentConfiguration;
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
