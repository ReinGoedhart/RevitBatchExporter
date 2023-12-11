using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework
{
    public class RevitBatchExporterDbContextFactory
    {
        private readonly DbContextOptions _options;

        public RevitBatchExporterDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public RevitBatchExporterDbContext Create()
        {
            return new RevitBatchExporterDbContext(_options);
        }
    }
}
