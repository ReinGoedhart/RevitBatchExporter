using RevitBatchExporter.Frontend.Commands.LogCommands;
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

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingItemsControlComponent
{
    public class LoggingItemsControlViewModel : ViewModelBase
    {
        private readonly ObservableCollection<LoggingItemsControlItemViewModel> _loggingItemsControlItemViewModel;
        public IEnumerable<LoggingItemsControlItemViewModel> LoggingItemsControlItemViewModel => _loggingItemsControlItemViewModel;
        private SelectedLogFileStore _selectedLogFileStore;

        public LoggingItemsControlViewModel(SelectedLogFileStore selectedLogFileStore)
        {
            _selectedLogFileStore = selectedLogFileStore;
            //PopulateLogFiles();
            _loggingItemsControlItemViewModel = new ObservableCollection<LoggingItemsControlItemViewModel>();
            PopulateLog();

        }

        public void DeleteLogFile(LogFile logFile)
        {
            var deletedLogFile = _loggingItemsControlItemViewModel.FirstOrDefault(x => x.LogFile == logFile);
            if(deletedLogFile != null)
            {
                _loggingItemsControlItemViewModel.Remove(deletedLogFile);
            }
        }
        

        //public void PopulateLogFiles()
        //{
        //    foreach (LogFile file in loggingFiles)
        //    {
        //        _loggingItemsControlItemViewModel.Add(new LoggingItemsControlItemViewModel(file, _navigationStore));
        //    }
        //}

        public void PopulateLog()
        {
            for (int i = 0; i < 50; i++)
            {
                LogFile file1 = new LogFile()
                {
                    Configuration = new Configuration() { ConfigurationName = "config" + i.ToString() },
                    ErrorsOccured = 3,
                    Id = 1,
                    LogFilePath = "C://asdasdasd"
                };
                _loggingItemsControlItemViewModel.Add(new LoggingItemsControlItemViewModel(file1, _selectedLogFileStore));

            }
        }
    }
}
