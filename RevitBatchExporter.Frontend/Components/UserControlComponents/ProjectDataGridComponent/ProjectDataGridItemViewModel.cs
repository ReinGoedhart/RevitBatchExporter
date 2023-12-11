using RevitBatchExporter.Domain.Enums;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Domain.Enums.Enums;
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
        public ProjectDataGridItemViewModel(Project project, SelectedProjectStore selectedProjectStore, INavigationService createEditProjectModalNavigationService)
        {
            _createEditProjectModalNavigationService = createEditProjectModalNavigationService;
            _selectedProjectStore = selectedProjectStore; 
            Project = project;
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
                    return string.Empty; 
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
        }
    }
}
