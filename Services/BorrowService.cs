using Bookl_Manager_System.Data;
using Bookl_Manager_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookl_Manager_System.Services
{
    public class BorrowService
    {
        private readonly DbContextOptions<AppDbContext> _dbOptions;

        public BorrowService(DbContextOptions<AppDbContext> options)
        {
            _dbOptions = options;
        }

        public async Task BorrowBook(int studentid, int bookid, int number, DateTime duetime)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var sid =await context.Students.AnyAsync(s => s.Id == studentid);
                var bid =await  context.Books.AnyAsync(book => book.Id == bookid);
                if (!sid || !bid)
                {
                    MessageBox.Show("学生或者书籍不存在");
                    return;
                }
                var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bookid);
                int availbook = book.AvailableStock;
                if(number > availbook || number <= 0)
                {
                    MessageBox.Show("借阅数量异常");
                    return;
                }
                var borrowOrder = new BorrowOrders
                {
                    StudentId = studentid,
                    BorrowTime = DateTime.Now,
                    DueTime = duetime
                };
                context.BorrowOrders.Add(borrowOrder);
                await using var transaction = await context.Database.BeginTransactionAsync();
                await context.SaveChangesAsync();
                var borrowOrderItem = new BorrowOrderItems
                {
                    BorrowOrderId = borrowOrder.Id,
                    BookId = bookid,
                    Quantity = number
                };
                book.AvailableStock -= number;
                context.BorrowOrderItems.Add(borrowOrderItem);
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
                return;
            }
        }

        public async Task ReturnBook(int BorrowOrderId)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var borrowOrder = await context.BorrowOrders.FirstOrDefaultAsync(b => b.Id == BorrowOrderId);
                if (borrowOrder == null)
                {
                    return;
                }
                if(borrowOrder.ReturnTime != null)
                {
                    return;
                }
                var borrowItems = await context.BorrowOrderItems
                    .Where(item => item.BorrowOrderId == BorrowOrderId)
                    .ToListAsync();
                foreach (var borrowItem in borrowItems)
                {
                    var bid = borrowItem.BookId;
                    var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bid);
                    book.AvailableStock += borrowItem.Quantity;
                }
                borrowOrder.ReturnTime = DateTime.Now;
                await context.SaveChangesAsync();
                return;
            }
        }

        public async Task<List<BorrowOrders>> GetCurrentBorrowings()
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var borrowOrders = await context.BorrowOrders
                .Where(item => item.ReturnTime == null)
                .ToListAsync();
                return borrowOrders;
            }
        }

        public async Task<List<BorrowOrders>> GetOverdueBorrowings()
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var now = DateTime.Now;
                var borrowOrders = await context.BorrowOrders
                .Where(item => item.ReturnTime == null && item.DueTime < now)
                .ToListAsync();
                return borrowOrders;
            }
        }

        public async Task<List<BookBorrowHistoryDto>> GetBookBorrowingHistory(int bookId)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var borrowList = await context.BorrowOrderItems
                    .Where(s => s.BookId == bookId)
                    .ToListAsync ();
                var historyList = new List<BookBorrowHistoryDto>();
                foreach (var item in borrowList) {
                    var borroworder = await context.BorrowOrders.FirstOrDefaultAsync(b => b.Id == item.BorrowOrderId);
                    var student = await context.Students.FirstOrDefaultAsync(s => s.Id == borroworder.StudentId);
                    var his = new BookBorrowHistoryDto
                    {
                        StudentName = student.Name,
                        BorrowTime = borroworder.BorrowTime,
                        DueTime = borroworder.DueTime,
                        ReturnTime = borroworder.ReturnTime,
                        Quantity = item.Quantity
                    };
                    historyList.Add(his);
                }
                return historyList;
            };
        }
        public async Task<List<BorrowOrders>> GetStudentBorrowingHistory(int studentId)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                return;
            };
        }
    }
}
