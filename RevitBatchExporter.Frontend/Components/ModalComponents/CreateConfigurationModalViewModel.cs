using RevitBatchExporter.Frontend.Commands.ProjectCommands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{

    public class CreateConfigurationModalViewModel : ViewModelBase
    {
        public ICommand CreateConfiguration { get; set; }
        public ICommand Cancel { get; }

        public CreateConfigurationModalViewModel(CompositeNavigationService createConfigurationAndNavigate, CompositeNavigationService cancel)
        {
            CreateConfiguration = new RelayCommand(() =>
            {
                //CreateNewProjects
                createConfigurationAndNavigate.Navigate();
            });
            Cancel = new RelayCommand(() => { cancel.Navigate(); });
        }
    }
}
