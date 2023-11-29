using static RevitBatchExporter.Frontend.Enums.Enums;

namespace RevitBatchExporter.Frontend.Models
{
    public class Configuration
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public RevitRelease RevitVersion { get; set; } 
    }
}
