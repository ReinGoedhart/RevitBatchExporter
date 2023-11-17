using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.ViewModels
{
    public class MainViewModel: ViewModelBase
    {

        private NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public ICommand NavigateLogging {  get; }
        public ICommand NavigateConfiguration {  get; }
        public ICommand NavigateProjects {  get; }
        public ICommand NavigateHome {  get; }
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            NavigateHome = new NavigateCommand<HomeViewModel>(_navigationStore, () => new HomeViewModel());
            NavigateProjects= new NavigateCommand<ProjectViewModel>(_navigationStore, () => new ProjectViewModel());
            NavigateConfiguration = new NavigateCommand<ConfigurationViewModel>(_navigationStore, () => new ConfigurationViewModel());
            NavigateLogging = new NavigateCommand<LoggingViewModel>(_navigationStore, () => new LoggingViewModel());



            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }


    }
}
