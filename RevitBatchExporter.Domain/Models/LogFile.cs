using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Models
{
    public class LogFile
    {
        public int Id { get; }
        public Configuration Configuration { get; set; }
        public List<Project> Projects { get; set; }
        public string LogFilePath { get; set; }
        public int ErrorsOccured { get; set; }
    }
}
