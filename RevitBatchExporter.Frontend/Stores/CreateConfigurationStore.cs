using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores
{
    public class CreateConfigurationStore
    {
        public event Action<Configuration> OnCreatedConfiguration;
        public void CreateConfiguration(Configuration configuration)
        {
            OnCreatedConfiguration?.Invoke(configuration);
        }
    }
}
