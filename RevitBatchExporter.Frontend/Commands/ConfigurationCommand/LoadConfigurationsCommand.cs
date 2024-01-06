using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Commands.ConfigurationCommand
{
    public class LoadConfigurationsCommand: AsyncCommandBase
    {
        private readonly ConfigurationsStore _configurationStore;

        public LoadConfigurationsCommand(ConfigurationsStore configurationStore)
        {
            _configurationStore = configurationStore;
        }

        public override async Task ExcecuteAsync(object parameter)
        {
            try
            {
                await _configurationStore.Load();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
