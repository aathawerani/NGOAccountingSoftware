using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class DBConnectionException : Exception
    {
        public DBConnectionException(string message) : base(message)
        {
        }
    }
}
