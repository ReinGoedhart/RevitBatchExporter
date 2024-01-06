using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Frontend.Commands.ProjectCommands;
using RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent;
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
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectDataGridViewModel ProjectDataGridViewModel { get; set; }
        public ICommand DeleteModal { get; }
        public ICommand CreateConfigurationModal { get; }
        public ICommand DuplicateProject { get; }
        public ICommand RefreshData { get; }

        private INavigationService _createConfigurationModalNavigationService;
        private INavigationService _errorModalNavigationService;
        private INavigationService _deleteModalNavigationService;
        private INavigationService _editProjectModalNavigationService;
        private ErrorMessagesStore _errorMessagesStore;

        private SelectedProjectStore _selectedProjectStore;
        private DeleteObjectsStore _deleteObjectsStore;
        private ProjectsStore _projectStore;


        public ProjectViewModel(SelectedProjectStore selectedProjectStore, ErrorMessagesStore errorMessagesStore, DeleteObjectsStore deleteObjectsStore, ProjectsStore projectStore, INavigationService deleteModalNavigationService, INavigationService createConfigurationModalNavigationService, INavigationService editProjectModalNavigationService, INavigationService errorModalNavigationService)
        {
            _errorMessagesStore = errorMessagesStore;
            _createConfigurationModalNavigationService = createConfigurationModalNavigationService;
            _errorModalNavigationService = errorModalNavigationService;
            _selectedProjectStore = selectedProjectStore;
            _projectStore = projectStore;
            _deleteObjectsStore = deleteObjectsStore;
            _editProjectModalNavigationService = editProjectModalNavigationService;


            ProjectDataGridViewModel = ProjectDataGridViewModel.LoadViewModel(_selectedProjectStore, _errorMessagesStore, _deleteObjectsStore, _projectStore, _editProjectModalNavigationService);

            DuplicateProject = new DuplicateProjectCommand(this);
            DeleteModal = new NavigateCommand(deleteModalNavigationService);
            CreateConfigurationModal = new CreateConfigurationModalCommand(_errorMessagesStore, _errorModalNavigationService, _createConfigurationModalNavigationService, ProjectDataGridViewModel);
            RefreshData = new RelayCommand(RefreshDataCommand);
        }

        private void RefreshDataCommand()
        {
            ProjectDataGridViewModel = ProjectDataGridViewModel.LoadViewModel(_selectedProjectStore, _errorMessagesStore, _deleteObjectsStore, _projectStore, _editProjectModalNavigationService);
        }


        public override void Dispose()
        {
            ProjectDataGridViewModel.Dispose();
            base.Dispose();
        }
        private string _searchString;
        public string SearchString
        {
            get
            {
                return _searchString;
            }
            set
            {
                _searchString = value;
                ProjectDataGridViewModel.UpdateProjectCollectionWithSearchValue(value);
                OnPropertyChanged(nameof(SearchString));
            }
        }
    }
}
