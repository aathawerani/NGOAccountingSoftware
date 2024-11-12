using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class DALException : Exception
    {
        public DALException(string message) : base(message)
        {
        }
    }
}
