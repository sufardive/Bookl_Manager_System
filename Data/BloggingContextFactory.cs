using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookl_Manager_System.Data
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            string dbPath = Path.Combine(Path.GetDirectoryName(typeof(AppDbContext).Assembly.Location), "library.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
