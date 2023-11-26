using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using RevitBatchExporter.Frontend.Services;
using RevitBatchExporter.Frontend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static RevitBatchExporter.Frontend.Enums.Enums;

namespace RevitBatchExporter.Frontend.Components.ModalComponents
{
    public class EditProjectModalViewModel : ViewModelBase
    {
        public List<RevitExportType> ExportTypes => Enum.GetValues(typeof(RevitExportType)).Cast<RevitExportType>().ToList();
        private SelectedProjectStore _selectedProjectStore { get; set; }
        public Project EditingProject { get; set; }
        public ICommand EditProject { get; }
        public ICommand Cancel { get; }
        public EditProjectModalViewModel(CompositeNavigationService cancel, SelectedProjectStore selectedProjectStore)
        {
            _selectedProjectStore = selectedProjectStore;
            EditProject = new RelayCommand(() => { cancel.Navigate(); }); // create Logic
            Cancel = new RelayCommand(() => { cancel.Navigate(); });
            _selectedProjectStore.selectedProjectChanged += SelectedProjectStore_selectedProjectChanged;
        }
        private void SelectedProjectStore_selectedProjectChanged(Project obj)
        {
            if (obj != null)
            {
                EditingProject = obj;
            }
        }
    }
}
