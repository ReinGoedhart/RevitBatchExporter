using System.Collections.Generic;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.Domain.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public RevitRelease RevitVersion { get; set; } 
        public List<Project> Projects { get; set; }
        public bool IsVisible { get; set; }
    }
}
