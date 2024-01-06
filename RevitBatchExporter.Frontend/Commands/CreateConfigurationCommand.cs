using Microsoft.VisualBasic;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.Components.ModalComponents;
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
        private readonly CreateConfigurationModalViewModel _vm;
        private SelectedProjectStore _selectedProjectStore;

        public CreateConfigurationCommand(
            INavigationService createConfigurationAndNavigate,
            ConfigurationsStore configurationsStore, SelectedProjectStore selectedProjectStore, CreateConfigurationModalViewModel vm)
        {
            _selectedProjectStore = selectedProjectStore;
            _createConfigurationAndNavigate = createConfigurationAndNavigate;
            _configurationsStore = configurationsStore;
            _vm  = vm;
        }

        public override async Task ExcecuteAsync(object parameter)
        {
            if (_selectedProjectStore.CheckedProjects.Count() <= 0 || _selectedProjectStore.CheckedProjects == null)
            {
                return;
            }
            Configuration configuration = new Configuration()
            {
                Id = 0,
                ConfigurationName = _vm.ConfigurationNameText,
                IsVisible = true,
                Projects = _selectedProjectStore.CheckedProjects,
                RevitVersion = _selectedProjectStore.CheckedProjects[0].RevitVersion
            };
            try
            {
                await _configurationsStore.Create(configuration);
                _selectedProjectStore.CheckedProjects.Clear();
            }
            catch (Exception ex)
            {
                string wow = ex.Message;
                _selectedProjectStore.CheckedProjects.Clear();
            }
            _createConfigurationAndNavigate.Navigate();
        }
    }
}
