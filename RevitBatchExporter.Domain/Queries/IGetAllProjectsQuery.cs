using RevitBatchExporter.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.Domain.Queries
{
    public interface IGetAllProjectsQuery
    {
        Task<IEnumerable<Project>> Execute();
    }
}
