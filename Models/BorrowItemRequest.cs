using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookl_Manager_System.Models
{
    public class BorrowItemRequest
    {
        public int BookId { get; set; }

        public int Quantity { get; set; }
    }
}
