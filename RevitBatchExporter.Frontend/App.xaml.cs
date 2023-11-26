using RevitBatchExporter.Frontend.Components.ModalComponents;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.ViewModels;
using RevitBatchExporter.Frontend.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace RevitBatchExporter.Frontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedProjectStore _selectedProjectStore;
        private readonly ErrorMessagesStore _errorMessagesStore;

        public App()
        {
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();
            _selectedProjectStore = new SelectedProjectStore();
            _errorMessagesStore = new ErrorMessagesStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            INavigationService homeNaviationService = CreateHomeViewModel();
            homeNaviationService.Navigate();

            MainView mainWindow = new MainView()
            {
                DataContext = new MainViewModel(_navigationStore, _modalNavigationStore,
                new NavigationBarViewModel(
                    CreateHomeViewModel(),
                    CreateProjectViewModel(),
                    CreateConfigurationViewModel(),
                    CreateLoggingViewModel()))
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        // Views
        private INavigationService CreateProjectViewModel()
        {
            return new NavigationService<ProjectViewModel>(_navigationStore, () => new ProjectViewModel(_selectedProjectStore,_errorMessagesStore, CreateDeleteModalViewModel(), CreateConfigurationModalViewModel(), CreateEditProjectModalViewModel(), CreateErrorModalViewModel()));
        }
        private INavigationService CreateConfigurationViewModel()
        {
            return new NavigationService<ConfigurationViewModel>(_navigationStore, () => new ConfigurationViewModel());
        }
        private INavigationService CreateHomeViewModel()
        {
            return new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel());
        }
        private INavigationService CreateLoggingViewModel()
        {
            return new NavigationService<LoggingViewModel>(_navigationStore, () => new LoggingViewModel());
        }

        // Modals
        private INavigationService CreateDeleteModalViewModel()
        {
            CompositeNavigationService cancel = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore));

            return new ModalNavigationService<DeleteModalViewModel>(_modalNavigationStore, () => new DeleteModalViewModel(cancel));
        }
        private INavigationService CreateConfigurationModalViewModel()
        {
            CompositeNavigationService CreateConfigurationAndNavigate = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore), CreateConfigurationViewModel());
            CompositeNavigationService cancel = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore));

            return new ModalNavigationService<CreateConfigurationModalViewModel>(_modalNavigationStore, () => new CreateConfigurationModalViewModel(CreateConfigurationAndNavigate, cancel));
        }
        private INavigationService CreateEditProjectModalViewModel()
        {
            CompositeNavigationService cancel = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore));
            return new ModalNavigationService<EditProjectModalViewModel>(_modalNavigationStore, () => new EditProjectModalViewModel(cancel, _selectedProjectStore));
        }
        private INavigationService CreateErrorModalViewModel()
        {
            CompositeNavigationService cancel = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore));
            return new ModalNavigationService<ErrorModalViewModel>(_modalNavigationStore, () => new ErrorModalViewModel(cancel, _errorMessagesStore));
        }
        

    }
}
