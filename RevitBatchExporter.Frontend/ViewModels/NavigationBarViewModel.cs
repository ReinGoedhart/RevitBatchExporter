using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System.Windows.Input;

namespace RevitBatchExporter.Frontend.ViewModels
{
   
    public class NavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateLogging { get; }
        public ICommand NavigateConfiguration { get; }
        public ICommand NavigateProjects { get; }
        public ICommand NavigateHome { get; }

        public NavigationBarViewModel(INavigationService homeNavigationService,
            INavigationService projectNavigationService,
            INavigationService configurationNavigationService,
            INavigationService loggingNavigationService)
        {
            NavigateHome = new NavigateCommand(homeNavigationService);
            NavigateProjects = new NavigateCommand(projectNavigationService);
            NavigateConfiguration = new NavigateCommand(configurationNavigationService);
            NavigateLogging = new NavigateCommand(loggingNavigationService);
        }
    }
}
