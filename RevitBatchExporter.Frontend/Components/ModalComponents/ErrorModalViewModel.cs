using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class ErrorModalViewModel : ViewModelBase
    {
        public List<string> ErrorMessages { get; set; }
        public ICommand Cancel { get; }
        public ErrorModalViewModel(CompositeNavigationService cancel, ErrorMessagesStore errorMessagesStore)
        {
            ErrorMessages = errorMessagesStore.CurrentErrorMessages;
            Cancel = new RelayCommand(() => {
                cancel.Navigate();
                errorMessagesStore.ClearErrorMessages();
            });
        }
    }
}
