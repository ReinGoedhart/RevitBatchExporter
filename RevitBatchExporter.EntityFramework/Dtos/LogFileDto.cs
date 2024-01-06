using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework.Dtos
{
    public class LogFileDto
    {
        public int Id { get; set; }
        public Configuration Configurations { get; set; }
        public List<Project> Projects { get; set; }
        public string? LogFilePath { get; set; }
        public int ErrorsOccured { get; set; }
    }
}
