using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Frontend.Enums.Enums;
using System.Windows.Input;
using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.Commands;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class ExportToDeveloperModalViewModel : ViewModelBase
    {

        public ICommand CreateAndNavigate { get; set; }
        public ICommand Cancel { get; }


        private string _messageTextBox;
        public string MessageTextBox
        {
            get
            {
                return _messageTextBox;
            }
            set
            {
                _messageTextBox = value;
                OnPropertyChanged(nameof(MessageTextBox));
            }
        }
        private SelectedLogFileStore _selectedLogFileStore;

        private INavigationService _cancelNavigationService { get; set; }
        public ExportToDeveloperModalViewModel(CompositeNavigationService cancelNavigationService, SelectedLogFileStore selectedLogFileStore)
        {
            _selectedLogFileStore = selectedLogFileStore;
            _cancelNavigationService = cancelNavigationService;

            CreateAndNavigate = new RelayCommand(SendEmail);
            Cancel = new NavigateCommand(_cancelNavigationService);
        }

        private void SendEmail()
        {
            _cancelNavigationService.Navigate();
            if (_selectedLogFileStore.CurrentSelectedLogFile != null)
            {
                //send email
            }
        }
    }
}
