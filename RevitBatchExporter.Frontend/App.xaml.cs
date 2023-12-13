using RevitBatchExporter.Frontend.Components.ModalComponents;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.ViewModels;
using RevitBatchExporter.Frontend.Views;
using System.Windows;
using static RevitBatchExporter.Domain.Enums.Enums;
using RevitBatchExporter.Domain.Commands;
using RevitBatchExporter.Domain.Queries;
using RevitBatchExporter.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RevitBatchExporter.Frontend
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly RevitBatchExporterDbContextFactory _dbContextFactory;
        private readonly IGetAllProjectsQuery _getAllProjectsQuery;
        private readonly ICreateProjectCommand _createProjectCommand;
        private readonly IDeleteProjectCommand _deleteProjectCommand;
        private readonly IUpdateProjectCommand _updateProjectCommand;

        

        private readonly IGetAllLogFilesQuery _getAllLogFilesQuery;
        private readonly IUpdateLogFileCommand _updateLogFileCommand;
        private readonly ICreateLogFileCommand _createLogFileCommand;
        private readonly IDeleteLogFileCommand _deleteLogFileCommand;

        private readonly IGetAllConfigurationsQuery _getAllConfigurationsQuery;
        private readonly ICreateConfigurationCommand _createConfigurationCommand;
        private readonly IDeleteConfigurationCommand _deleteConfigurationCommand;
        private readonly IUpdateConfigurationCommand _updateConfigurationCommand;

        private readonly NavigationStore _navigationStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly SelectedProjectStore _selectedProjectStore;
        private readonly SelectedConfigurationStore _selectedConfigurationStore;
        private readonly ErrorMessagesStore _errorMessagesStore;
        private readonly DeleteObjectsStore _deleteObjectsStore;
        private readonly SelectedLogFileStore _selectedLogFileStore;
        private readonly ConfigurationsStore _configurationsStore;
        private readonly ProjectsStore _projectsStore;
        private readonly LogFilesStore _logFilesStore;

        public App()
        {
            string connectionString = "Data Source=RevitBatchExporter.db";
            _dbContextFactory = new RevitBatchExporterDbContextFactory(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);

            _getAllProjectsQuery = new GetAllProjectsQuery(_dbContextFactory);
            _createProjectCommand = new CreateProjectCommand(_dbContextFactory);
            _deleteProjectCommand = new DeleteProjectCommand(_dbContextFactory);
            _updateProjectCommand = new UpdateProjectCommand(_dbContextFactory);

            _getAllConfigurationsQuery = new GetAllConfigurationsQuery(_dbContextFactory);
            _createConfigurationCommand = new CreateConfigurationCommand(_dbContextFactory);
            _deleteConfigurationCommand = new DeleteConfigurationCommand(_dbContextFactory);
            _updateConfigurationCommand = new UpdateConfigurationCommand(_dbContextFactory);

            _getAllLogFilesQuery = new GetAllLogFilesQuery(_dbContextFactory);
            _createLogFileCommand = new CreateLogFileCommand(_dbContextFactory);
            _deleteLogFileCommand = new DeleteLogFileCommand(_dbContextFactory);
            _updateLogFileCommand = new UpdateLogFileCommand(_dbContextFactory);

            _configurationsStore = new ConfigurationsStore( _getAllConfigurationsQuery, _createConfigurationCommand, _deleteConfigurationCommand, _updateConfigurationCommand);
            _projectsStore = new ProjectsStore( _getAllProjectsQuery, _createProjectCommand, _deleteProjectCommand, _updateProjectCommand);
            _logFilesStore = new LogFilesStore( _getAllLogFilesQuery, _createLogFileCommand, _deleteLogFileCommand, _updateLogFileCommand);


            _navigationStore = new NavigationStore();
            _modalNavigationStore = new ModalNavigationStore();
            _selectedProjectStore = new SelectedProjectStore();
            _errorMessagesStore = new ErrorMessagesStore();
            _selectedConfigurationStore = new SelectedConfigurationStore();
            _deleteObjectsStore = new DeleteObjectsStore();
            _selectedLogFileStore = new SelectedLogFileStore();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            using(RevitBatchExporterDbContext context = _dbContextFactory.Create())
            {
                context.Database.Migrate();
            }

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
            return new NavigationService<ProjectViewModel>(_navigationStore, () => new ProjectViewModel(_selectedProjectStore, _errorMessagesStore, _deleteObjectsStore, _projectsStore, CreateDeleteProjectsModalViewModel("Delete project"), CreateConfigurationModalViewModel(), CreateEditProjectModalViewModel(), CreateErrorModalViewModel()));
        }
        private INavigationService CreateConfigurationViewModel()
        {
            return new NavigationService<ConfigurationViewModel>(_navigationStore, () =>
            new ConfigurationViewModel(_selectedConfigurationStore, _errorMessagesStore, _configurationsStore, _deleteObjectsStore, CreateDeleteConfigurationModalViewModel("Delete configuration"), CreateErrorModalViewModel(), ExportModalViewModel())); ;
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
            return new ModalNavigationService<CreateConfigurationModalViewModel>(_modalNavigationStore, () => new CreateConfigurationModalViewModel(CreateConfigurationAndNavigate, CreateCancelCompositeNavigationService(), _configurationsStore));
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
