using RevitBatchExporter.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class SelectedConfigurationStore
    {
        public Configuration? SelectedConfiguration { get; set; }
        public event Action<Configuration> ConfigurationChanged;
        public SelectedConfigurationStore()
        {
            SelectedConfiguration = null;
        }
        public void ChangeCurrentConfiguration(Configuration configuration)
        {
            SelectedConfiguration = configuration;
            ConfigurationChanged?.Invoke(configuration);
        }
    }
}
