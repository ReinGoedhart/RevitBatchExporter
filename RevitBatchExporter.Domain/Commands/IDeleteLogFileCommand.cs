using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Commands
{
    public interface IDeleteLogFileCommand
    {
        Task Execute(int logFileId);
    }
}
