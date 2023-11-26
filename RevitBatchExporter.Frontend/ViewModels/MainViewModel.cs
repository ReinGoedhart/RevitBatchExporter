using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ViewModelBase CurrentModalViewModel => _modalNavigationStore.CurrentViewModel;
        public NavigationBarViewModel NavigationBarViewModel { get; set; }
        public bool IsOpen => _modalNavigationStore.IsOpen;
        public MainViewModel(
            NavigationStore navigationStore, 
            ModalNavigationStore modelNavigationStore,NavigationBarViewModel navigationBarViewModel
            )
        {
            NavigationBarViewModel = navigationBarViewModel;
            _navigationStore = navigationStore;
            _modalNavigationStore = modelNavigationStore;



            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _modalNavigationStore.CurrentViewModelChanged += OnCurrentModalViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentModalViewModel));
            OnPropertyChanged(nameof(IsOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
