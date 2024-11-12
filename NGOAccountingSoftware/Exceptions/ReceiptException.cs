using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ReceiptException : Exception
    {
        public ReceiptException(string message) : base(message)
        {
        }
    }
}
