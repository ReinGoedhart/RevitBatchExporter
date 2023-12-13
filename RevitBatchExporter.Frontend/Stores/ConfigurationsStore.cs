using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class ConfigurationsStore
    {
        private readonly IGetAllConfigurationsQuery _getAllConfigurationsQuery;
        private readonly ICreateConfigurationCommand _createConfigurationCommand;
        private readonly IDeleteConfigurationCommand _deleteConfigurationCommand;
        private readonly IUpdateConfigurationCommand _updateConfigurationCommand;

        public ConfigurationsStore(IGetAllConfigurationsQuery getAllConfigurationsQuery, 
            ICreateConfigurationCommand createConfigurationCommand, 
            IDeleteConfigurationCommand deleteConfigurationCommand, 
            IUpdateConfigurationCommand updateConfigurationCommand)
        {
            _getAllConfigurationsQuery = getAllConfigurationsQuery;
            _createConfigurationCommand = createConfigurationCommand;
            _deleteConfigurationCommand = deleteConfigurationCommand;
            _updateConfigurationCommand = updateConfigurationCommand;
        }

        public event Action<Configuration> ConfigurationCreated;
        public event Action<Configuration> ConfigurationUpdated;
        public event Action<int> ConfigurationDeleted;

        public async Task Create(Configuration configuration)
        {
            await _createConfigurationCommand.Execute(configuration);
            ConfigurationCreated?.Invoke(configuration);
        }
        public async Task Update(Configuration configuration)
        {
            await _updateConfigurationCommand.Execute(configuration);
            ConfigurationUpdated?.Invoke(configuration);
        }
        public async Task Deleted(Configuration configuration)
        {
            await _deleteConfigurationCommand.Execute(configuration.Id);
            ConfigurationDeleted?.Invoke(configuration.Id);
        }

        public async Task Load()
        {
            var configurations = await _getAllConfigurationsQuery.Execute();
        }

    }
}
