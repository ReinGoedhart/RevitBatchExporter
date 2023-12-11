using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public interface IUpdateLogFileCommand
    {
        Task Execute(LogFile logFile);
    }
}
