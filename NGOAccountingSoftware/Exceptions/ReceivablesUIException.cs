using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ReceivablesUIException : Exception
    {
        public ReceivablesUIException(string message) : base(message)
        {
        }
    }
}
