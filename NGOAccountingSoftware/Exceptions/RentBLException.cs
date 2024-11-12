using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class RentBLException : Exception
    {
        public RentBLException(string message) : base(message)
        {
        }
    }
}
