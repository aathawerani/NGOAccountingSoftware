using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class PrintDocumentException : Exception
    {
        public PrintDocumentException(string message) : base(message)
        {
        }
    }
}
