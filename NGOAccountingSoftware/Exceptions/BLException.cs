﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class BLException : Exception
    {
        public BLException(string message) : base(message)
        {
        }
    }
}
