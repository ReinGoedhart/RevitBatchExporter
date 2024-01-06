using RevitBatchExporter.Frontend.Commands;
using RevitBatchExporter.Domain.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class EditProjectModalViewModel : ViewModelBase
    {
        public List<RevitExportType> ExportTypes => Enum.GetValues(typeof(RevitExportType)).Cast<RevitExportType>().ToList();
        private SelectedProjectStore _selectedProjectStore { get; set; }
        private ProjectsStore _projectsStore { get; set; }
        public Project EditingProject { get; set; }
        public ICommand EditProject { get; }
        public ICommand ChooseConfigurationJson { get; }
        public ICommand Cancel { get; }
        public EditProjectModalViewModel(INavigationService cancel, SelectedProjectStore selectedProjectStore, ProjectsStore projectsStore )
        {
            _selectedProjectStore = selectedProjectStore;
            _projectsStore = projectsStore;
            EditProject = new RelayCommand(() => 
            { 
                cancel.Navigate(); 
                _selectedProjectStore.ProjectEdited(EditingProject);
                _projectsStore.Update(EditingProject);
            });
            Cancel = new RelayCommand(() => { cancel.Navigate(); });
            _selectedProjectStore.selectedProjectChanged += SelectedProjectStore_selectedProjectChanged;
            ChooseConfigurationJson = new ChooseFolderCommand(this);
        }

        public void UpdateJsonCommand()
        {
            OnPropertyChanged(nameof(EditingProject));
        }

        private void SelectedProjectStore_selectedProjectChanged(Project obj)
        {
            if (obj != null)
            {
                EditingProject = obj;
            }
        }

        public override void Dispose()
        {
            _selectedProjectStore.selectedProjectChanged -= SelectedProjectStore_selectedProjectChanged;
            base.Dispose();
        }
    }
}
