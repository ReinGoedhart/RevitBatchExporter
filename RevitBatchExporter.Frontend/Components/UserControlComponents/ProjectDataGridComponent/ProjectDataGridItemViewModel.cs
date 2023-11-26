﻿using RevitBatchExporter.Frontend.Enums;
using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Frontend.Enums.Enums;
using System.Windows.Input;
using RevitBatchExporter.Frontend.Stores;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Commands;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ProjectDataGridComponent
{
    public class ProjectDataGridItemViewModel: ViewModelBase
    {
        private SelectedProjectStore _selectedProjectStore { get; set; }
        public ICommand SelectionChangedCommand { get; private set; }
        private INavigationService _createEditProjectModalNavigationService;
        private ProjectViewModel _vm;
        public ProjectDataGridItemViewModel(Project project, ProjectViewModel vm, SelectedProjectStore selectedProjectStore, INavigationService createEditProjectModalNavigationService)
        {
            _createEditProjectModalNavigationService = createEditProjectModalNavigationService;
            _selectedProjectStore = selectedProjectStore; 
            Project = project;
            _vm = vm;
            RevitRelease release = project.RevitVersion;
            RevitVersion = (int)release;
            EditProject = new RelayCommand(OpenModalCommand);
        }

        public ICommand EditProject { get; }
        private bool _isChecked { get; set; }
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
                OnIsCheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public int Id => Project.Id;
        public string ProjectName => Project.ProjectName;
        public RevitExportType RevitExportType => Project.RevitExportType;
        public int RevitVersion { get; set; }
        public Project Project { get; set; }
        public string DisplayedSaveAfterExport
        {
            get
            {
                if (this.RevitExportType == RevitExportType.SYNCONLY)
                    return string.Empty; // Or string.Empty, depending on how you want to handle this case in your UI.
                else
                    return Project.SaveAfterExport.ToString();
            }
        }
        public string DisplayedConfigurationPath
        {
            get
            {
                if (this.RevitExportType == RevitExportType.SYNCONLY)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(Project.ConfigurationPath))
                {
                    return "Field required";
                }
                else
                {
                    return Project.ConfigurationPath;
                }
            }
        }
        public string DisplayedViewName
        {
            get
            {
                if (this.RevitExportType == RevitExportType.SYNCONLY)
                    return string.Empty;
                else
                    return Project.ViewName;
            }
        }
        public string DisplayedOutputName
        {
            get
            {
                if (this.RevitExportType == RevitExportType.SYNCONLY)
                {
                    return string.Empty;
                }
                if (string.IsNullOrEmpty(Project.OutputName))
                {
                    return "Field required";
                }
                else
                {
                    return Project.OutputName;
                }
            }
        }
        public event EventHandler OnIsCheckedChanged;
        public void OpenModalCommand()
        {
            _createEditProjectModalNavigationService.Navigate();
            _selectedProjectStore.CurrentProject(Project);
            //_vm.EditProjectModalViewModel.OpenModal();
            //_vm.EditProjectModalViewModel.ProjectsDataGridViewModel = _vm.ProjectsDataGridViewModel;
            //_vm.EditProjectModalViewModel.EditingProject = Project;
            //_vm.EditProjectModalViewModel.ProjectService = _vm._projectService;
        }
    }
}
