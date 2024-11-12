using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class TrustException : Exception
    {
        public TrustException(string message) : base(message)
        {
        }
    }
}
