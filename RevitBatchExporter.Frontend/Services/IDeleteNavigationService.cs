using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Services
{
    public interface IDeleteNavigationService: INavigationService
    {
        public event Action DeleteObject;
    }
}
