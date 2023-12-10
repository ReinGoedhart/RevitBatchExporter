using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.LogCommands
{
    public class DeleteLogFileCommand : CommandBase
    {
        private INavigationService _deleteNavigationService;
        public DeleteLogFileCommand( INavigationService deleteNavigationService)
        {
            _deleteNavigationService = deleteNavigationService;
        }

        public override void Execute(object parameter)
        {
            _deleteNavigationService.Navigate();
        }
    }
}
