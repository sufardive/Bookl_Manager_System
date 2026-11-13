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
        public DbSet<Books> Books{ get; set; }
        public DbSet<BorrowOrders> BorrowOrders { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<BorrowOrderItems> BorrowOrderItems { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
    }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Books>()
                .HasKey(c => c.Id );
            modelBuilder.Entity<Books>()
                .Property(b => b.Title)
                .IsRequired();
            modelBuilder.Entity<BorrowOrders>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<BorrowOrders>()
                .HasOne<Students>()
                .WithMany()
                .HasForeignKey(o => o.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Students>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Students>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<BorrowOrderItems>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<BorrowOrderItems>()
                .HasOne<BorrowOrders>()
                .WithMany()
                .HasForeignKey(i => i.BorrowOrderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<BorrowOrderItems>()
                .HasOne<Books>()
                .WithMany()
                .HasForeignKey(i => i.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
