using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.Stores.CRUDBaseClass
{
    public abstract class CrudBase<T> 
    { 
        public abstract Task Create(T item);
        public abstract Task Update(T item);
        public abstract Task Deleted(T item);
        public abstract Task Load();
    }
}
