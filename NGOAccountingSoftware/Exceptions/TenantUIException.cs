using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class TenantUIException : Exception
    {
        public TenantUIException(string message) : base(message)
        {
        }
    }
}
