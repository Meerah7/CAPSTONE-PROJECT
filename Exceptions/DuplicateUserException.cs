﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException() : base("User already exists.") { }

        public DuplicateUserException(string message) : base(message) { }

        public DuplicateUserException(string message, Exception innerException) : base(message, innerException) { }
    }
}
