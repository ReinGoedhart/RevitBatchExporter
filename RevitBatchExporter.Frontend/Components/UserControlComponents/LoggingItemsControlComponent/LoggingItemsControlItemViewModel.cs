using RevitBatchExporter.Frontend.Commands.LogCommands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingItemsControlComponent
{
    public class LoggingItemsControlItemViewModel : ViewModelBase
    {
        public LogFile LogFile { get; set; }
        public string ConfigurationName => LogFile.Configuration.ConfigurationName;
        public int ErrorsOccured => LogFile.ErrorsOccured;
        public DateTime DateTime { get; set; }
        public ICommand LogSelected { get; }
        private SelectedLogFileStore _selectedLogFileStore;
        public LoggingItemsControlItemViewModel(LogFile logFile, SelectedLogFileStore selectedLogFileStore)
        {
            _selectedLogFileStore = selectedLogFileStore;
            LogFile = logFile;
            FileInfo fileInfo = new FileInfo(LogFile.LogFilePath);
            LogSelected = new LogSelectedCommand(_selectedLogFileStore);
            DateTime = fileInfo.CreationTime;
        }
    }
}
