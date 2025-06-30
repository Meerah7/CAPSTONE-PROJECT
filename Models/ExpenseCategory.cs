using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using finance;
 namespace finance.Models
    {
        public class ExpenseCategory
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; } = string.Empty;

            public override bool Equals(object? obj)
            {
                return obj is ExpenseCategory category &&
                       this.CategoryId == category.CategoryId &&
                       this.CategoryName == category.CategoryName;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(CategoryId, CategoryName);
            }
        }
    }
