using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookl_Manager_System.Models
{
    public class StudentBorrowHistoryDto
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public string BookTitle { get; set; }

        public int Quantity { get; set; }

        public DateTime BorrowTime { get; set; }

        public DateTime DueTime { get; set; }

        public DateTime? ReturnTime { get; set; }
}
}
