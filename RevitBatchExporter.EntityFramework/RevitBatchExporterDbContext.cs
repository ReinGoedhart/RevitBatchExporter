using Microsoft.EntityFrameworkCore;
using RevitBatchExporter.EntityFramework.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitBatchExporter.EntityFramework
{
    public class RevitBatchExporterDbContext: DbContext
    {
        public RevitBatchExporterDbContext(DbContextOptions options) : base(options) { }
        public DbSet<ConfigurationDto> Configurations {  get; set; }
        public DbSet<LogFileDto> LogFiles { get; set;}
        public DbSet<ProjectDto> Projects { get; set; }
    }
}
