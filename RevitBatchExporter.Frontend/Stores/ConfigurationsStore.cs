using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.Frontend.Stores.CRUDBaseClass;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class ConfigurationsStore: CrudBase<Configuration>
    {
        private readonly IGetAllConfigurationsQuery _getAllConfigurationsQuery;
        private readonly ICreateConfigurationCommand _createConfigurationCommand;
        private readonly IDeleteConfigurationCommand _deleteConfigurationCommand;
        private readonly IUpdateConfigurationCommand _updateConfigurationCommand;

        private readonly List<Configuration> _configurations;
        public IEnumerable<Configuration> Configurations => _configurations;

        public ConfigurationsStore(IGetAllConfigurationsQuery getAllConfigurationsQuery, 
            ICreateConfigurationCommand createConfigurationCommand, 
            IDeleteConfigurationCommand deleteConfigurationCommand, 
            IUpdateConfigurationCommand updateConfigurationCommand)
        {
            _getAllConfigurationsQuery = getAllConfigurationsQuery;
            _createConfigurationCommand = createConfigurationCommand;
            _deleteConfigurationCommand = deleteConfigurationCommand;
            _updateConfigurationCommand = updateConfigurationCommand;

            _configurations = new List<Configuration>();
        }

        public event Action<Configuration> ConfigurationCreated;
        public event Action<Configuration> ConfigurationUpdated;
        public event Action<int> ConfigurationDeleted;
        public event Action<IEnumerable<Configuration>> GetAllConfigurations;
        public event Action ConfigurationsLoaded;

        public override async Task Create(Configuration configuration)
        {
            await _createConfigurationCommand.Execute(configuration);
            ConfigurationCreated?.Invoke(configuration);
        }
        public override async Task Update(Configuration configuration)
        {
            await _updateConfigurationCommand.Execute(configuration);
            ConfigurationUpdated?.Invoke(configuration);
        }
        public override async Task Deleted(Configuration configuration)
        {
            await _deleteConfigurationCommand.Execute(configuration);
            ConfigurationDeleted?.Invoke(configuration.Id);
        }

        public override async Task Load()
        {
            var configurations = await _getAllConfigurationsQuery.Execute();

            _configurations.Clear();
            _configurations.AddRange(configurations);
            ConfigurationsLoaded?.Invoke();
        }

    }
}
