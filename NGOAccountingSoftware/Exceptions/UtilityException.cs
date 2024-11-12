using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class UtilityException : Exception
    {
        public UtilityException(string message) : base(message)
        {
        }
    }
}
