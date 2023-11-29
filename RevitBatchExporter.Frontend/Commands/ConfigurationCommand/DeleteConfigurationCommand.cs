using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.ConfigurationCommand
{
    public class DeleteConfigurationCommand : CommandBase
    {
        private INavigationService _deleteNavigationService;
        private ConfigurationViewModel _vm;

        public DeleteConfigurationCommand( ConfigurationViewModel vm, INavigationService deleteNavigationService)
        {
            _deleteNavigationService = deleteNavigationService;
            _vm = vm;
        }

        public override void Execute(object parameter)
        {
            _deleteNavigationService.Navigate();
            //if (_currentConfigurationId != -300)
            //{
            //    ConfigurationService cs = new ConfigurationService();
            //    cs.Delete(_currentConfigurationId);
            //    if (ConfigurationItemsControlViewModel != null)
            //    {
            //        ConfigurationItemsControlViewModel.PopulateAndCategoriseConfigurations(cs);
            //    }
            //}
        }
    }
}
