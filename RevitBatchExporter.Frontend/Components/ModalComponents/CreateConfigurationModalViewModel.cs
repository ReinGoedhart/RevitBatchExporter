using RevitBatchExporter.Frontend.Commands.ProjectCommands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RevitBatchExporter.Domain.Enums.Enums;
using RevitBatchExporter.Frontend.Commands;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class CreateConfigurationModalViewModel : ViewModelBase
    {
        public ICommand CreateAndNavigate { get; set; }
        public ICommand Cancel { get; }
        private readonly INavigationService _createConfigurationAndNavigate;
        private readonly ConfigurationsStore _configurationsStore;

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

        public CreateConfigurationModalViewModel(CompositeNavigationService configurationAndNavigate, 
            CompositeNavigationService cancel, 
            ConfigurationsStore configurationsStore)
        {
            _configurationsStore = configurationsStore;
            _createConfigurationAndNavigate = configurationAndNavigate;
            CreateAndNavigate = new CreateConfigurationCommand(_createConfigurationAndNavigate, _configurationsStore, ConfigurationNameText);
            Cancel = new RelayCommand(() => { cancel.Navigate(); });
        }
    }
}
