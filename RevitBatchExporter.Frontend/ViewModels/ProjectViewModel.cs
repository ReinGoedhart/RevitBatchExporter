﻿using RevitBatchExporter.Frontend.Commands;
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
        public INavigationService _createConfigurationModalNavigationService;
        public INavigationService _errorModalNavigationService;
        private ErrorMessagesStore _errorMessagesStore;

        public ProjectViewModel(SelectedProjectStore selectedProjectStore, ErrorMessagesStore errorMessagesStore, DeleteObjectsStore deleteObjectsStore, INavigationService deleteModalNavigationService, INavigationService createConfigurationModalNavigationService, INavigationService editProjectModalNavigationService, INavigationService errorModalNavigationService)
        {
            _errorMessagesStore = errorMessagesStore;
            _createConfigurationModalNavigationService = createConfigurationModalNavigationService;
            _errorModalNavigationService = errorModalNavigationService;

            ProjectDataGridViewModel = new ProjectDataGridViewModel(this, selectedProjectStore, errorMessagesStore, deleteObjectsStore, editProjectModalNavigationService);
            
            DeleteModal = new NavigateCommand(deleteModalNavigationService);
            CreateConfigurationModal = new CreateConfigurationModalCommand(_errorMessagesStore, _errorModalNavigationService, _createConfigurationModalNavigationService, ProjectDataGridViewModel);
        }
    }
}
