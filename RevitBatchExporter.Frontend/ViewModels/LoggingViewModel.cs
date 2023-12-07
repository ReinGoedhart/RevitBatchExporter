using RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingItemsControlComponent;
using RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingViewerComponent;
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

        public LoggingViewModel(SelectedLogFileStore selectedLogFileStore, INavigationService deleteNavigationService)
        {
            _selectedLogFileStore = selectedLogFileStore;
            _deleteNavigationService = deleteNavigationService;
            LoggingViewerViewModel = new LoggingViewerViewModel();
            LoggingItemsControlViewModel = new LoggingItemsControlViewModel();
        }
    }
}
