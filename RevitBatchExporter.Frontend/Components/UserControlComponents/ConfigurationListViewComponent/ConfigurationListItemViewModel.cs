using RevitBatchExporter.Frontend.Models;
using RevitBatchExporter.Frontend.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Components.UserControlComponents.ConfigurationListViewComponent
{
    public class ConfigurationListItemViewModel : ViewModelBase
    {
        public Project Project { get; set; }
        public string ProjectName => Project.ProjectName;
        public string ViewName => Project.ViewName;

        public ConfigurationListItemViewModel(Project project)
        {
            Project = project;
        }

    }
}
