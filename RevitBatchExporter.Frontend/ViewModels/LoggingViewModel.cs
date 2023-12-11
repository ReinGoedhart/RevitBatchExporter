using RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingItemsControlComponent;
using RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingViewerComponent;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.ViewModels
{
    public class LoggingViewModel : ViewModelBase
    {
        public LoggingViewerViewModel LoggingViewerViewModel { get; set; }
        public LoggingItemsControlViewModel LoggingItemsControlViewModel { get; set; }
        private SelectedLogFileStore _selectedLogFileStore;
        private INavigationService _deleteNavigationService;
        private DeleteObjectsStore _deleteObjectsStore;

        public LogFile SelectedLogFile => _selectedLogFileStore?.CurrentSelectedLogFile;
        public bool HasSelectedLogFile => SelectedLogFile != null;

        public LoggingViewModel(SelectedLogFileStore selectedLogFileStore, DeleteObjectsStore deleteObjectsStore, INavigationService deleteNavigationService, INavigationService exportToDeveloperNavigationService)
        {
            _deleteObjectsStore = deleteObjectsStore;
            _selectedLogFileStore = selectedLogFileStore;
            _deleteNavigationService = deleteNavigationService;
            LoggingViewerViewModel = new LoggingViewerViewModel(_selectedLogFileStore, _deleteNavigationService, exportToDeveloperNavigationService);
            LoggingItemsControlViewModel = new LoggingItemsControlViewModel(_selectedLogFileStore);
            _deleteObjectsStore.OnDeleteLog += DeleteObjectsStore_OnDeleteLog;
            _selectedLogFileStore.selectedLogFileChanged += _selectedLogFileStore_selectedLogFileChanged;
        }

        private void _selectedLogFileStore_selectedLogFileChanged(LogFile obj)
        {
            if (obj != null)
            {
                OnPropertyChanged(nameof(SelectedLogFile));
                OnPropertyChanged(nameof(HasSelectedLogFile));
            }
        }

        public override void Dispose()
        {
            _deleteObjectsStore.OnDeleteLog -= DeleteObjectsStore_OnDeleteLog;
            _selectedLogFileStore.selectedLogFileChanged -= _selectedLogFileStore_selectedLogFileChanged;
            LoggingViewerViewModel.Dispose();
            LoggingItemsControlViewModel.Dispose();
            base.Dispose();
        }

        private void DeleteObjectsStore_OnDeleteLog()
        {
            LoggingItemsControlViewModel.DeleteLogFile(_selectedLogFileStore.CurrentSelectedLogFile);
            _selectedLogFileStore.CurrentSelectedLogFile = null;
            OnPropertyChanged(nameof(SelectedLogFile));
            OnPropertyChanged(nameof(HasSelectedLogFile));
        }
    }
}
