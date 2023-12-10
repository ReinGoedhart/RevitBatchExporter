using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Properties;
using RevitBatchExporter.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    class SettingsModalViewModel: ViewModelBase
    {
        private string _saveDirectory;
        public string SaveDirectory
        {
            get
            {
                return _saveDirectory;
            }
            set
            {
                if (_saveDirectory != value)
                {
                    Settings.Default.SaveDirectory= value;
                    _saveDirectory = value;
                    OnPropertyChanged(nameof(SaveDirectory));
                }
            }
        }
        private string _logDirectory;
        public string LogDirectory
        {
            get
            {
                return _logDirectory;
            }
            set
            {
                if (_logDirectory != value)
                {
                    Settings.Default.LogDirectory = value;
                    _logDirectory = value;
                    OnPropertyChanged(nameof(LogDirectory));
                }
            }
        }
        private string _createNewLocalDirectory;
        public string CreateNewLocalDirectory
        {
            get
            {
                return _createNewLocalDirectory;
            }
            set
            {
                if (_createNewLocalDirectory != value)
                {
                    Settings.Default.CreateNewLocalDirectory = value;
                    _createNewLocalDirectory = value;
                    OnPropertyChanged(nameof(CreateNewLocalDirectory));
                }
            }
        }
        private INavigationService _cancelNavigationService;
        public ICommand SaveSettings { get; }
        public ICommand Cancel { get; }
        public ICommand ChooseOutputFolder { get; }
        public ICommand ChooseLoggingFolder { get; }
        public ICommand ChooseLocalFolder { get; }

        public SettingsModalViewModel(INavigationService cancelNavigationService)
        {
            _cancelNavigationService = cancelNavigationService;
            SaveDirectory = Settings.Default.SaveDirectory;
            LogDirectory = Settings.Default.LogDirectory;
            CreateNewLocalDirectory = Settings.Default.CreateNewLocalDirectory;
            SaveSettings = new RelayCommand(SaveSettingsCommand);
            Cancel = new NavigateCommand(_cancelNavigationService);

            ChooseOutputFolder = new ChooseFolderCommand(this,1);
            ChooseLoggingFolder = new ChooseFolderCommand(this,2);
            ChooseLocalFolder = new ChooseFolderCommand(this,3);
        }

        private void SaveSettingsCommand()
        {
            Settings.Default.Save();
            _cancelNavigationService.Navigate();
        }
    }
}
