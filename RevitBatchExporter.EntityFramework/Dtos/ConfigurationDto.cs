using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.EntityFramework.Dtos
{
    public class ConfigurationDto
    {
        public int Id { get; set; }
        public string? ConfigurationName { get; set; }
        public RevitRelease RevitVersion { get; set; }
        public List<Project> Projects { get; set; }
        public bool IsVisible { get; set; }
    }
}
