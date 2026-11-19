using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookl_Manager_System.Services
{
    public enum BorrowBookResult
    {
        Success,
        StudentNotFound,
        BookNotFound,
        InvalidQuantity,
        InsufficientStock,
        EmptyBorrowList
    }
}
