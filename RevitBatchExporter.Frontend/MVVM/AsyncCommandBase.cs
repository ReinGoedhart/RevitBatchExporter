using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Frontend.MVVM
{
    public abstract class AsyncCommandBase : CommandBase
    {
        public override async void Execute(object parameter)
        {
            try
            {
                await ExcecuteAsync(parameter);
            }
            catch (Exception) { }
        }
        public abstract Task ExcecuteAsync(object parameter);
    }
}
