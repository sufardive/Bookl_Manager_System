using Bookl_Manager_System.Data;
using Bookl_Manager_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookl_Manager_System.Services
{
    public class BookService
    {
        private readonly List<Books> bookList = [];
        private readonly DbContextOptions<AppDbContext> _dbOptions;

        public BookService(DbContextOptions<AppDbContext> options)
        {
            _dbOptions = options;
        }

        public async Task<List<Books>> GetBooks()
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                return await context.Books.ToListAsync();
            }
        }

        public async Task AddBook(Books book) {
            using (var context = new AppDbContext(_dbOptions))
            {
                context.Books.Add(book);
                await context.SaveChangesAsync();
            }
        }

        public async Task RemoveBook(int id)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var blog = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                context.Books.Remove(blog);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateBook(int id,string name,double price)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var blog = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (blog == null) return;
                blog.Title = name;
                blog.Price = price;
                await context.SaveChangesAsync();
            }
        }

        public async Task<Books> SearcheBook(int id)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var blog = await context.Books.SingleOrDefaultAsync(b => b.Id == id);
                if (blog == null) return null;
                return blog;
            }
            
        }
    }
}
