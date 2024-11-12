using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class AccountingSheetsExceptions : Exception
    {
        public AccountingSheetsExceptions(string message) : base(message)
        {
        }
    }
}
