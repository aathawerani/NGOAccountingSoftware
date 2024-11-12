using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class LoggerException : Exception
    {
        public LoggerException(string message) : base(message)
        {
        }
    }
}
