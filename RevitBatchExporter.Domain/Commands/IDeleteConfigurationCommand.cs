using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public interface IDeleteConfigurationCommand
    {
        Task Execute(Configuration configuration);
    }
}
