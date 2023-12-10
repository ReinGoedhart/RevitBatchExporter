using RevitBatchExporter.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class SelectedLogFileStore
    {
        public LogFile? CurrentSelectedLogFile { get; set; }
        public event Action<LogFile> selectedLogFileChanged;
        public void CurrentLogFileChanged(LogFile selectedLogFile)
        {
            CurrentSelectedLogFile=  selectedLogFile;
            selectedLogFileChanged?.Invoke(selectedLogFile);
        }
    }
}
