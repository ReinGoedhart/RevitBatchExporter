using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class DeleteModalViewModel : ViewModelBase
    {
        public ICommand DeleteProjects { get; }
        public ICommand Cancel { get; }
        public string DeleteTitle { get; set; }
        private Classes _classType;
        private DeleteObjectsStore _deleteObjectsStore;

        INavigationService _cancelNavigationService;
        public DeleteModalViewModel(DeleteObjectsStore deleteObjectsStore, INavigationService cancelNavigationService, string deleteTitle, Classes classType)
        {
            _classType = classType;
            _deleteObjectsStore = deleteObjectsStore;
            _cancelNavigationService = cancelNavigationService;
            DeleteTitle = deleteTitle;
            DeleteProjects = new RelayCommand(DeleteObject); // create Logic
            Cancel = new RelayCommand(() => { _cancelNavigationService.Navigate(); });
        }

        public void DeleteObject()
        {
            _deleteObjectsStore.DeleteObjects(_classType);
            _cancelNavigationService.Navigate();
        }
    }
}
