using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class MiscUIException : Exception
    {
        public MiscUIException(string message) : base(message)
        {
        }
    }
}
