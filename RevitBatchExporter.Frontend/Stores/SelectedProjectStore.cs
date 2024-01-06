using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitBatchExporter.Domain.Models;

namespace RevitBatchExporter.Frontend.Stores
{
    public class SelectedProjectStore
    {
        public List<Project> CheckedProjects { get; set; }

        public void AddProject(Project project)
        {
            CheckedProjects.Add(project);
        }
        public void RemoveProject(Project project)
        {
            CheckedProjects.Remove(project);
        }
        public SelectedProjectStore()
        {
            CheckedProjects = new List<Project>();  
        }
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
