﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finance.Exceptions
{
    
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException(string message) : base(message) { }
        }
    }

