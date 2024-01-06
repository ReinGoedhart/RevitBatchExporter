using Microsoft.Win32;
using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.Commands.LogCommands;
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

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingViewerComponent
{
    public class LoggingViewerViewModel : ViewModelBase
    {
        private INavigationService _deleteNavigationService;
        private INavigationService _exportToDeveloperNavigationService;
        private SelectedLogFileStore _selectedLogFileStore;


        public LoggingViewerViewModel(SelectedLogFileStore selectedLogFileStore,INavigationService deleteNavigationService, INavigationService exportToDeveloperNavigationService)
        {
            _deleteNavigationService = deleteNavigationService;
            _selectedLogFileStore = selectedLogFileStore;
            _exportToDeveloperNavigationService = exportToDeveloperNavigationService;


            ExportLogFileToDeveloper = new RelayCommand(ExportToDeveloperCommand);
            DeleteLogFile = new DeleteLogFileCommand(_deleteNavigationService);
            _selectedLogFileStore.selectedLogFileChanged += _selectedLogFileStore_selectedLogFileChanged;
        }

        public override void Dispose()
        {
            _selectedLogFileStore.selectedLogFileChanged -= _selectedLogFileStore_selectedLogFileChanged;
            base.Dispose();
        }

        private void ExportToDeveloperCommand()
        {
            _exportToDeveloperNavigationService.Navigate();
        }

        public ICommand ExportLogFileToDeveloper { get; }
        public ICommand DeleteLogFile { get; }
        public string ConfigurationName { get; set; }

        private void _selectedLogFileStore_selectedLogFileChanged(LogFile logFile)
        {
            ConfigurationName = logFile.Configurations.ConfigurationName;
            OnPropertyChanged(nameof(ConfigurationName));
        }
    }
}
