using RevitBatchExporter.Frontend.Commands.ProjectCommands;
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
using static RevitBatchExporter.Frontend.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class CreateConfigurationModalViewModel : ViewModelBase
    {
        public ICommand CreateAndNavigate { get; set; }
        public ICommand Cancel { get; }
        INavigationService _createConfigurationAndNavigate;
        CreateConfigurationStore _createConfigurationStore;

        private string _configurationNameText;
        public string ConfigurationNameText
        {
            get
            {
                return _configurationNameText;
            }
            set
            {
                _configurationNameText = value;
                OnPropertyChanged(nameof(ConfigurationNameText));
            }
        }

        public CreateConfigurationModalViewModel(CompositeNavigationService createConfigurationAndNavigate, CompositeNavigationService cancel, CreateConfigurationStore createConfigurationStore)
        {
            _createConfigurationStore = createConfigurationStore;
            _createConfigurationAndNavigate = createConfigurationAndNavigate;
            CreateAndNavigate = new RelayCommand(CreateConfiguration);
            Cancel = new RelayCommand(() => { cancel.Navigate();});
        }

        private void CreateConfiguration()
        {
            Configuration configuration = new Configuration()
            {
                ConfigurationName = ConfigurationNameText,
                Id = 3,
                RevitVersion = RevitRelease.Revit2021
            };
            _createConfigurationAndNavigate.Navigate();
            _createConfigurationStore.CreateConfiguration(configuration);

        }
    }
}
