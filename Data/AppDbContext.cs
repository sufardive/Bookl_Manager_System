using Bookl_Manager_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookl_Manager_System.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Books> Books
        {
            get;
            set;
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>()
                .HasKey(c => c.Id );
            modelBuilder.Entity<Books>()
    .Property(b => b.Title)
    .IsRequired();
        }
    }
}
