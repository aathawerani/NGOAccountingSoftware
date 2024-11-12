using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class TrustHandlerException : Exception
    {
        public TrustHandlerException(string message) : base(message)
        {
        }
    }
}
