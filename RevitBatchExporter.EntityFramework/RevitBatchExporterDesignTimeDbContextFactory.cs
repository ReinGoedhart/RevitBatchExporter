using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework
{
    public class RevitBatchExporterDesignTimeDbContextFactory : IDesignTimeDbContextFactory<RevitBatchExporterDbContext>
    {
        public RevitBatchExporterDbContext CreateDbContext(string[] args = null)
        {
            return new RevitBatchExporterDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=RevitBatchExporter.db").Options);
        }
    }
}
