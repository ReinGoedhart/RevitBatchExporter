using RevitBatchExporter.Frontend.Components.ModalComponents;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Frontend.Models;
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
        private readonly SelectedConfigurationStore _selectedConfigurationStore;
        private readonly ErrorMessagesStore _errorMessagesStore;

        public App()
        {
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();
            _selectedProjectStore = new SelectedProjectStore();
            _errorMessagesStore = new ErrorMessagesStore();
            _selectedConfigurationStore = new SelectedConfigurationStore();
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
            return new NavigationService<ProjectViewModel>(_navigationStore, () => new ProjectViewModel(_selectedProjectStore,_errorMessagesStore, CreateDeleteModalViewModel("Delete project"), CreateConfigurationModalViewModel(), CreateEditProjectModalViewModel(), CreateErrorModalViewModel()));
        }
        private INavigationService CreateConfigurationViewModel()
        {
            return new NavigationService<ConfigurationViewModel>(_navigationStore, () => 
            new ConfigurationViewModel(_selectedConfigurationStore, _errorMessagesStore, CreateDeleteModalViewModel("Delete configuration"), CreateErrorModalViewModel(), ExportModalViewModel())); ;
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
        private INavigationService CreateDeleteModalViewModel(string deleteTitle)
        {
            return new ModalNavigationService<DeleteModalViewModel>(_modalNavigationStore, () => new DeleteModalViewModel(CreateCancelCompositeNavigationService(), deleteTitle));
        }
        private INavigationService CreateConfigurationModalViewModel()
        {
            CompositeNavigationService CreateConfigurationAndNavigate = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore), CreateConfigurationViewModel());
            return new ModalNavigationService<CreateConfigurationModalViewModel>(_modalNavigationStore, () => new CreateConfigurationModalViewModel(CreateConfigurationAndNavigate, CreateCancelCompositeNavigationService()));
        }
        private INavigationService CreateEditProjectModalViewModel()
        {
            return new ModalNavigationService<EditProjectModalViewModel>(_modalNavigationStore, () => new EditProjectModalViewModel(CreateCancelCompositeNavigationService(), _selectedProjectStore));
        }
        private INavigationService CreateErrorModalViewModel()
        {
            return new ModalNavigationService<ErrorModalViewModel>(_modalNavigationStore, () => new ErrorModalViewModel(CreateCancelCompositeNavigationService(), _errorMessagesStore));
        } 
        private INavigationService ExportModalViewModel()
        {
            return new ModalNavigationService<ExportModalViewModel>(_modalNavigationStore, () => new ExportModalViewModel(CreateCancelCompositeNavigationService()));
        }
        private CompositeNavigationService CreateCancelCompositeNavigationService()
        {
            return new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore));
        }
    }
}
