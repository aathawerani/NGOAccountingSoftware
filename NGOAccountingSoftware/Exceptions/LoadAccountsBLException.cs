using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class LoadAccountsBLException : Exception
    {
        public LoadAccountsBLException(string message) : base(message)
        {
        }
    }
}
