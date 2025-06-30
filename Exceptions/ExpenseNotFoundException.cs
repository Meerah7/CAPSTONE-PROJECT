using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.Exceptions
{ 
        public class ExpenseNotFoundException : Exception
        {
            public ExpenseNotFoundException(string message) : base(message) { }
        }
    }


