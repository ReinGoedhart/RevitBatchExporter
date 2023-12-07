using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBatchExporter.Frontend.Models;

namespace RevitBatchExporter.Frontend.Stores
{
    public class SelectedProjectStore
    {
        public Project CurrentSelectedProject { get; set; }
        public event Action<Project> selectedProjectChanged;
        public void CurrentProject(Project selectedProject)
        {
            selectedProjectChanged?.Invoke(selectedProject);
        }
        public event Action<Project> ProjectIsEdited;
        public void ProjectEdited(Project editedProject)
        {
            ProjectIsEdited(editedProject);
        }


    }
}
