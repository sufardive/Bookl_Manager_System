using Bookl_Manager_System.Models;
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
                id = bookList.Max(id =>  id.bookId)+1;
            }
            book.bookId = id;
            bookList.Add(book);

        }

        public void RemoveBook(int id)
        {
            var book = bookList.FirstOrDefault(b => b.bookId == id);
            if(book != null)
            {
                bookList.Remove(book);
            }
        }

        public void UpdateBook(int id,string name,double price)
        {
            var book = bookList.FirstOrDefault(b => b.bookId == id);
            if (book != null)
            {
                book.bookName = name;
                book.price = price;
            }
        }

        public Books SearcheBook(int id)
        {
            var book = bookList.FirstOrDefault(book => book.bookId == id);
            return book;
        }
    }
}
