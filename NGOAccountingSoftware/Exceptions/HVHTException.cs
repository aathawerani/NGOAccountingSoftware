using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class HVHTException : Exception
    {
        public HVHTException(string message) : base(message)
        {
        }
    }
}
