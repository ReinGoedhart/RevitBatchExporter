using Microsoft.VisualBasic;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Frontend.Commands
{
    public class CreateConfigurationCommand : AsyncCommandBase
    {
        private readonly INavigationService _createConfigurationAndNavigate;
        private ConfigurationsStore _configurationsStore;
        private readonly string _configurationNameText;

        public CreateConfigurationCommand(
            INavigationService createConfigurationAndNavigate, 
            ConfigurationsStore configurationsStore,
            string configurationNameText)
        {
            _createConfigurationAndNavigate = createConfigurationAndNavigate;
            _configurationsStore = configurationsStore;
            _configurationNameText = configurationNameText;
        }

        public override async Task ExcecuteAsync(object parameter)
        {
            Configuration configuration = new Configuration()
            {
                Id = 3,
                ConfigurationName = _configurationNameText,
                IsVisible = true,
                // Add the projects!
                RevitVersion = RevitRelease.Revit2021
            };
            try
            {
                await _configurationsStore.Create(configuration);
            }
            catch (Exception)
            {
                throw;
            }
            _createConfigurationAndNavigate.Navigate();
        }
    }
}
