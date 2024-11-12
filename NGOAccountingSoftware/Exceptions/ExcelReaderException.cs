using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ExcelReaderException : Exception
    {
        public ExcelReaderException(string message) : base(message)
        {
        }
    }
}
