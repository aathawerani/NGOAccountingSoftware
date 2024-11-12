using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ExcelWriterException : Exception
    {
        public ExcelWriterException(string message) : base(message)
        {
        }
    }
}
