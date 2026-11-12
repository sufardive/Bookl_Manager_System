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

        public List<Books> GetBooks()
        {
            return bookList;
        }

        public void AddBook(Books book) {
            int id;
            if(bookList.Count == 0)
            {
                id = 0;
            }
            else
            {
                id = bookList.Max(id =>  id.Id)+1;
            }
            book.Id = id;
            bookList.Add(book);

        }

        public void RemoveBook(int id)
        {
            var book = bookList.FirstOrDefault(b => b.Id == id);
            if(book != null)
            {
                bookList.Remove(book);
            }
        }

        public void UpdateBook(int id,string name,double price)
        {
            var book = bookList.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                book.Title = name;
                book.Price = price;
            }
        }

        public Books SearcheBook(int id)
        {
            var book = bookList.FirstOrDefault(book => book.Id == id);
            return book;
        }
    }
}
