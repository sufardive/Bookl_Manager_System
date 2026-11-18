using System;

namespace Bookl_Manager_System.Models
{
    public class BookBorrowHistoryDto
    {
        public string StudentName { get; set; }

        public DateTime BorrowTime { get; set; }

        public DateTime DueTime { get; set; }

        public DateTime? ReturnTime { get; set; }

        public int Quantity { get; set; }
    }
}