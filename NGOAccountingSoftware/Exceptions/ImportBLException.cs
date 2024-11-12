using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class ImportBLException : Exception
    {
        public ImportBLException(string message) : base(message)
        {
        }
    }
}
