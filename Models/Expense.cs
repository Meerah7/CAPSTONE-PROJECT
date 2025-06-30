using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using finance;
 namespace finance.Models
    {
        public class Expense
        {
            public int ExpenseId { get; set; }

            public int UserId { get; set; }
            public decimal Amount { get; set; }
            public DateTime Date { get; set; }
            public string? Description { get; set; }
            public int CategoryId { get; set; }
        }
    }

