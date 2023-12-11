using RevitBatchExporter.Frontend.Components.ModalComponents;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Domain.Models;
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
using static RevitBatchExporter.Domain.Enums.Enums;

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
        private readonly CreateConfigurationStore _createConfigurationStore;
        private readonly DeleteObjectsStore _deleteObjectsStore;
        private readonly SelectedLogFileStore _selectedLogFileStore;


        public App()
        {
            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();
            _selectedProjectStore = new SelectedProjectStore();
            _errorMessagesStore = new ErrorMessagesStore();
            _selectedConfigurationStore = new SelectedConfigurationStore();
            _createConfigurationStore = new CreateConfigurationStore();
            _deleteObjectsStore = new DeleteObjectsStore();
            _selectedLogFileStore = new SelectedLogFileStore();
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
                    CreateLoggingViewModel(), CreateSettingsModalViewModel()))
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        // Views
        private INavigationService CreateProjectViewModel()
        {
            return new NavigationService<ProjectViewModel>(_navigationStore, () => new ProjectViewModel(_selectedProjectStore,_errorMessagesStore, _deleteObjectsStore, CreateDeleteProjectsModalViewModel("Delete project"), CreateConfigurationModalViewModel(), CreateEditProjectModalViewModel(), CreateErrorModalViewModel()));
        }
        private INavigationService CreateConfigurationViewModel()
        {
            return new NavigationService<ConfigurationViewModel>(_navigationStore, () => 
            new ConfigurationViewModel(_selectedConfigurationStore, _errorMessagesStore, _createConfigurationStore, _deleteObjectsStore, CreateDeleteConfigurationModalViewModel("Delete configuration"), CreateErrorModalViewModel(), ExportModalViewModel())); ;
        }
        private INavigationService CreateHomeViewModel()
        {
            return new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel());
        }
        private INavigationService CreateLoggingViewModel()
        {
            return new NavigationService<LoggingViewModel>(_navigationStore, () => new LoggingViewModel(_selectedLogFileStore, _deleteObjectsStore, CreateDeleteLoggingModalViewModel("Delete log file"), CreateExportToDeveloperModalViewModel()));
        }
        // Modals



        private INavigationService CreateExportToDeveloperModalViewModel()
        {
            return new ModalNavigationService<ExportToDeveloperModalViewModel>(_modalNavigationStore, () => new ExportToDeveloperModalViewModel(CreateCancelCompositeNavigationService(), _selectedLogFileStore));
        }  
        private INavigationService CreateSettingsModalViewModel()
        {
            return new ModalNavigationService<SettingsModalViewModel>(_modalNavigationStore, () => new SettingsModalViewModel(CreateCancelCompositeNavigationService()));
        }
        // Delete Modals
        private INavigationService CreateDeleteConfigurationModalViewModel(string deleteTitle)
        {
            return new ModalNavigationService<DeleteModalViewModel>(_modalNavigationStore, () => new DeleteModalViewModel(_deleteObjectsStore, CreateCancelCompositeNavigationService(), deleteTitle, Classes.Configuration));
        }
        private INavigationService CreateDeleteLoggingModalViewModel(string deleteTitle)
        {
            return new ModalNavigationService<DeleteModalViewModel>(_modalNavigationStore, () => new DeleteModalViewModel(_deleteObjectsStore, CreateCancelCompositeNavigationService(), deleteTitle, Classes.Log));
        }
        private INavigationService CreateDeleteProjectsModalViewModel(string deleteTitle)
        {
            return new ModalNavigationService<DeleteModalViewModel>(_modalNavigationStore, () => new DeleteModalViewModel(_deleteObjectsStore, CreateCancelCompositeNavigationService(), deleteTitle, Classes.Project));
        }
        private INavigationService CreateConfigurationModalViewModel()
        {
            CompositeNavigationService CreateConfigurationAndNavigate = new CompositeNavigationService(new CloseModalNavigationService(_modalNavigationStore), CreateConfigurationViewModel());
            return new ModalNavigationService<CreateConfigurationModalViewModel>(_modalNavigationStore, () => new CreateConfigurationModalViewModel(CreateConfigurationAndNavigate, CreateCancelCompositeNavigationService(), _createConfigurationStore));
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
