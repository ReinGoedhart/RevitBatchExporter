using RevitBatchExporter.Frontend.Models;
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

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent
{
    public class ProjectDataGridViewModel : ViewModelBase
    {
        private INavigationService _createEditProjectModalNavigationService;
        private SelectedProjectStore _selectedProjectStore;
        private ProjectViewModel _vm;
        public ProjectDataGridViewModel(ProjectViewModel vm, SelectedProjectStore selectedProjectStore, INavigationService createEditProjectModalNavigationService)
        {
            _createEditProjectModalNavigationService = createEditProjectModalNavigationService;
            _selectedProjectStore = selectedProjectStore;
            _vm = vm;

            _projectDataGridItemViewModel = new ObservableCollection<ProjectDataGridItemViewModel>();
            _checkedProjects = new List<ProjectDataGridItemViewModel>();

            ProjectsCollectionView = CollectionViewSource.GetDefaultView(_projectDataGridItemViewModel);
            ProjectsCollectionView.Filter = FilterProjects;
        }

        public void CheckButtonConfigCommand()
        {
            if (!_checkedProjects.Any())
            {
                System.Windows.MessageBox.Show("Error: No projects selected!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            var firstRevitVersion = _checkedProjects.First().Project.RevitVersion;
            if (!_checkedProjects.All(p => p.Project.RevitVersion == firstRevitVersion))
            {
                System.Windows.MessageBox.Show("Error: Not all projects have the same Revit version!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Basic CRUD opererations
        private readonly ObservableCollection<ProjectDataGridItemViewModel> _projectDataGridItemViewModel;
        public void DeleteProjectsCommand()
        {
            foreach (var item in _checkedProjects)
            {
                //_vm.GetProjectService().Delete(item.Project.Id);
            }
            GetProjects();
            _checkedProjects.Clear();
        }
        public void DuplicateProjectCommand()
        {
            foreach (var project in _checkedProjects)
            {
                //_vm.GetProjectService().Duplicate(project.Project.Id);
            }
            GetProjects();
            _checkedProjects.Clear();
        }
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
                RevitExportType = Enums.Enums.RevitExportType.IFC,
                RevitVersion = Enums.Enums.RevitRelease.Revit2022,
                ViewName = "BIM360",
                Region = Enums.Enums.Region.EMEA,
                ProjectName = "Schiphol"
            };
            var dataGridItem = new ProjectDataGridItemViewModel(project, _vm, _selectedProjectStore, _createEditProjectModalNavigationService);
            dataGridItem.OnIsCheckedChanged += GridItem_OnIsCheckedChanged;
            _projectDataGridItemViewModel.Add(dataGridItem);

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

        public void Refresh(string projectFilter)
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
