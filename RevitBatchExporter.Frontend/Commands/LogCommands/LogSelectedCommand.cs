using RevitBatchExporter.Frontend.Components.UserControlComponents.LoggingItemsControlComponent;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.LogCommands
{
    class LogSelectedCommand : CommandBase
    {
        private SelectedLogFileStore _selectedLogFileStore;

        public LogSelectedCommand(SelectedLogFileStore selectedLogFileStore)
        {
            _selectedLogFileStore = selectedLogFileStore;
        }

        public override void Execute(object parameter)
        {
            if(parameter is LoggingItemsControlItemViewModel loggingItemsControlItemViewModel)
            {
                _selectedLogFileStore.CurrentLogFileChanged(loggingItemsControlItemViewModel.LogFile);
            }
        }
    }
}
