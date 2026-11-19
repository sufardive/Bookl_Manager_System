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
                var sid = await context.Students.AnyAsync(s => s.Id == studentid);
                var bid = await context.Books.AnyAsync(book => book.Id == bookid);
                if (!sid || !bid)
                {
                    MessageBox.Show("学生或者书籍不存在");
                    return;
                }
                var book = await context.Books.FirstOrDefaultAsync(b => b.Id == bookid);
                int availbook = book.AvailableStock;
                if (number > availbook || number <= 0)
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
                if (borrowOrder.ReturnTime != null)
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
                var historyList = await context.BorrowOrderItems
                    .Where(b => b.BookId == bookId)
                    .Join(
                    context.BorrowOrders,
                    b => b.BorrowOrderId,
                    order => order.Id,
                    (item, order) => new { Item = item, Order = order }
                    )
                    .Join(
                    context.Students,
                    borrow => borrow.Order.StudentId,
                    student => student.Id,
                    (borrow, student) => new BookBorrowHistoryDto
                    {
                        StudentName = student.Name,
                        BorrowTime = borrow.Order.BorrowTime,
                        DueTime = borrow.Order.DueTime,
                        ReturnTime = borrow.Order.ReturnTime,
                        Quantity = borrow.Item.Quantity
                    })
                    .OrderBy(h => h.BorrowTime)
                    .ToListAsync();
                return historyList;
            }
            ;
        }
        public async Task<List<StudentBorrowHistoryDto>> GetStudentBorrowingHistory(int studentId)
        {
            using (var context = new AppDbContext(_dbOptions))
            {
                var StuBorrowHis = await context.Students
                    .Where(b => b.Id == studentId)
                    .Join(
                    context.BorrowOrders,
                    student => student.Id,
                    order => order.StudentId,
                    (student, order) => new
                    {
                        Student = student,
                        Order = order
                    })
                    .Join(
                    context.BorrowOrderItems,
                    order => order.Order.Id,
                    borrow => borrow.BorrowOrderId,
                    (Stu, borrow) => new
                    {
                        Student = Stu.Student,
                        Order = Stu.Order,
                        Borrow = borrow
                    })
                    .Join(
                    context.Books,
                    item => item.Borrow.BookId,
                    book => book.Id,
                    (Borrow, book) => new StudentBorrowHistoryDto
                    {
                        StudentId = Borrow.Student.Id,
                        StudentName = Borrow.Student.Name,
                        BookTitle = book.Title,
                        Quantity = Borrow.Borrow.Quantity,
                        BorrowTime = Borrow.Order.BorrowTime,
                        DueTime = Borrow.Order.DueTime,
                        ReturnTime = Borrow.Order.ReturnTime,
                    })
                    .OrderBy(h => h.BorrowTime)
                    .ToListAsync();
                return StuBorrowHis;
            }
        }
    }
};
