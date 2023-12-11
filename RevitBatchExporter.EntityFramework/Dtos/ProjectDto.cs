using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RevitBatchExporter.Domain.Enums.Enums;

namespace RevitBatchExporter.EntityFramework.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        public string ProjectName { get; set; }
        public Guid? ModelGuid { get; set; }
        public Guid? ProjectGuid { get; set; }
        public string? LocalModelPath { get; set; }
        public string? OutputName { get; set; }
        public bool SaveAfterExport { get; set; }
        public string? ViewName { get; set; }
        public string? ConfigurationPath { get; set; }
        public RevitRelease RevitVersion { get; set; }
        public RevitExportType RevitExportType { get; set; }
        public Region Region { get; set; }
        public List<Configuration>? Configurations { get; set; }
    }
}
