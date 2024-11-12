using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ReportUIException : Exception
    {
        public ReportUIException(string message) : base(message)
        {
        }
    }
}
