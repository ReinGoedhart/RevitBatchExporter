using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.Services;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent
{
    public class ProjectDataGridViewModel : ViewModelBase
    {
        private INavigationService _createEditProjectModalNavigationService;
        private SelectedProjectStore _selectedProjectStore;
        private ErrorMessagesStore _errorMessagesStore;
        private DeleteObjectsStore _deleteObjectsStore;
        public ProjectDataGridViewModel(SelectedProjectStore selectedProjectStore, ErrorMessagesStore errorMessagesStore, DeleteObjectsStore deleteObjectsStore, INavigationService createEditProjectModalNavigationService)
        {
            _deleteObjectsStore = deleteObjectsStore;
            _createEditProjectModalNavigationService = createEditProjectModalNavigationService;
            _selectedProjectStore = selectedProjectStore;
            _errorMessagesStore = errorMessagesStore;

            _projectDataGridItemViewModel = new ObservableCollection<ProjectDataGridItemViewModel>();
            _checkedProjects = new List<ProjectDataGridItemViewModel>();
            ProjectsCollectionView = CollectionViewSource.GetDefaultView(_projectDataGridItemViewModel);
            ProjectsCollectionView.Filter = FilterProjects;

            GetProjects();

            _deleteObjectsStore.OnDeleteProject += OnDeletedProjects;
            _selectedProjectStore.ProjectIsEdited += _selectedProjectStore_ProjectIsEdited;

        }

        public override void Dispose()
        {
            _deleteObjectsStore.OnDeleteProject -= OnDeletedProjects;
            _selectedProjectStore.ProjectIsEdited -= _selectedProjectStore_ProjectIsEdited;
            base.Dispose();
        }

        private void _selectedProjectStore_ProjectIsEdited(Project editedProject)
        {
            ProjectsCollectionView.Refresh();
        }

        private void OnDeletedProjects()
        {
            List<ProjectDataGridItemViewModel> itemsToRemove = new List<ProjectDataGridItemViewModel>();

            foreach (var projectItem in _projectDataGridItemViewModel)
            {
                if (_checkedProjects.Contains(projectItem))
                {
                    itemsToRemove.Add(projectItem);
                }
            }

            foreach (var itemToRemove in itemsToRemove)
            {
                _projectDataGridItemViewModel.Remove(itemToRemove);
            }

            _checkedProjects.Clear();
        }

        public void DuplicateProject(ProjectDataGridItemViewModel ProjectDataGridItemViewModel)
        {
            Project project = ProjectDataGridItemViewModel.Project;
            Project duplicatedProject = new Project()
            {
                ProjectGuid = project.ProjectGuid,
                ProjectName = project.ProjectName,
                SaveAfterExport = project.SaveAfterExport,
                ConfigurationPath = project.ConfigurationPath,
                IsVisible = project.IsVisible,
                LocalModelPath = project.LocalModelPath,
                ModelGuid = project.ModelGuid,
                OutputName = project.OutputName,
                Region = project.Region,
                RevitExportType = project.RevitExportType,
                RevitVersion = project.RevitVersion,
                ViewName = project.ViewName,
            };

            ProjectDataGridItemViewModel DuplicatedProject = new ProjectDataGridItemViewModel(duplicatedProject, _selectedProjectStore, _createEditProjectModalNavigationService);

            _projectDataGridItemViewModel.Add(DuplicatedProject);
        }

        public void ValidateCreateConfigurationCommand()
        {
            if (!_checkedProjects.Any())
            {
                _errorMessagesStore.AddErrorMessage("No projects selected");
                return;
            }
            var firstRevitVersion = _checkedProjects.First().Project.RevitVersion;
            if (!_checkedProjects.All(p => p.Project.RevitVersion == firstRevitVersion))
            {
                _errorMessagesStore.AddErrorMessage("Not all projects have the same Revit version!");
            }
        }
        // Basic CRUD opererations
        private readonly ObservableCollection<ProjectDataGridItemViewModel> _projectDataGridItemViewModel;
        
        public void GetProjects()
        {
            if (_projectDataGridItemViewModel.Count > 0)
            {
                _projectDataGridItemViewModel.Clear();
                _checkedProjects.Clear();
            }
            //foreach (Project project in _vm.GetProjectService().GetAllVisible())
            //{
            //    var dataGridItem = new ProjectDataGridItemViewModel(project, _vm);
            //    dataGridItem.OnIsCheckedChanged += GridItem_OnIsCheckedChanged;
            //    _projectDataGridItemViewModel.Add(dataGridItem);
            //}
            var project = new Project()
            {
                SaveAfterExport = true,
                ConfigurationPath = @"C://asdasdasd/asdasdsadasd/asdasdasd",
                Id = 1,
                IsVisible = true,
                OutputName = "hello!",
                RevitExportType = RevitExportType.IFC,
                RevitVersion = RevitRelease.Revit2022,
                ViewName = "BIM360",
                Region = Region.EMEA,
                ProjectName = "Schiphol"
            };
            var project2 = new Project()
            {
                SaveAfterExport = true,
                ConfigurationPath = @"C://asdasdasd/asdasdsadasd/asdasdasd",
                Id = 1,
                IsVisible = true,
                OutputName = "asdasdsa!",
                RevitExportType = RevitExportType.IFC,
                RevitVersion = RevitRelease.Revit2022,
                ViewName = "BIM360",
                Region = Region.EMEA,
                ProjectName = "Schiphol"
            };
            var dataGridItem = new ProjectDataGridItemViewModel(project, _selectedProjectStore, _createEditProjectModalNavigationService);
            var dataGridItem2 = new ProjectDataGridItemViewModel(project2, _selectedProjectStore, _createEditProjectModalNavigationService);
            dataGridItem.OnIsCheckedChanged += GridItem_OnIsCheckedChanged;
            dataGridItem2.OnIsCheckedChanged += GridItem_OnIsCheckedChanged;
            _projectDataGridItemViewModel.Add(dataGridItem);
            _projectDataGridItemViewModel.Add(dataGridItem2);
        }
        // Checkbox changes
        public List<ProjectDataGridItemViewModel> _checkedProjects { get; set; }
        private void GridItem_OnIsCheckedChanged(object sender, EventArgs e)
        {
            if (sender is ProjectDataGridItemViewModel dataGridItem)
            {
                if (dataGridItem.IsChecked)
                {
                    if (!_checkedProjects.Contains(dataGridItem))
                    {
                        _checkedProjects.Add(dataGridItem);
                    }
                }
                else
                {
                    if (_checkedProjects.Contains(dataGridItem))
                    {
                        _checkedProjects.Remove(dataGridItem);
                    }
                }
            }
        }
        // Filtering the datagrid
        public ICollectionView ProjectsCollectionView { get; }
        private string _searchString { get; set; } = string.Empty;

        public void UpdateProjectCollectionWithSearchValue(string searchstring)
        {
            RefreshProjectCollection(searchstring);
        }
        public void RefreshProjectCollection(string projectFilter)
        {
            _searchString = projectFilter;
            ProjectsCollectionView.Refresh();
        }
        private bool FilterProjects(object obj)
        {
            if (obj is ProjectDataGridItemViewModel dataGridItem)
            {
                var project = dataGridItem.Project;
                return project.ProjectName.ToString().ToLower().Contains(_searchString.ToLower()) ||
                       project.RevitVersion.ToString().ToLower().Contains(_searchString.ToLower());
            }
            return false;
        }
    }
}
