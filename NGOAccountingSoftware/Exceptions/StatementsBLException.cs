﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class StatementsBLException : Exception
    {
        public StatementsBLException(string message) : base(message)
        {
        }
    }
}