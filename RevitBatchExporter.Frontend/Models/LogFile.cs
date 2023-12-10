using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Models
{
    public class LogFile
    {
        public int Id { get; set; }
        public Configuration Configuration { get; set; }
        public List<Project> projectIds { get; set; }
        public string LogFilePath { get; set; }
        public int ErrorsOccured { get; set; }
    }
}
