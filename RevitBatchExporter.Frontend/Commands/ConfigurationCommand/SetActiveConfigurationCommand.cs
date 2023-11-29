using RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationItemsControlComponent;
using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;

namespace RevitBatchExporter.Frontend.Commands.ConfigurationCommand
{
    public class SetActiveConfigurationCommand : CommandBase
    {
        private SelectedConfigurationStore _selectedConfigurationStore;

        public SetActiveConfigurationCommand(SelectedConfigurationStore selectedConfigurationStore)
        {
            _selectedConfigurationStore = selectedConfigurationStore;
        }

        public override void Execute(object parameter)
        {
            if (parameter is Configuration configuration)
            {
                _selectedConfigurationStore.ChangeCurrentConfiguration(configuration);
            }
        }
    }
}
