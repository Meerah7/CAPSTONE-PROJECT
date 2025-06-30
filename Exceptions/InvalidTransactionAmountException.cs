using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.Exceptions
{
    public class InvalidTransactionAmountException : Exception
    {
        public InvalidTransactionAmountException() : base("Transaction amount is invalid.") { }

        public InvalidTransactionAmountException(string message) : base(message) { }

        public InvalidTransactionAmountException(string message, Exception innerException) : base(message, innerException) { }
    }
}
