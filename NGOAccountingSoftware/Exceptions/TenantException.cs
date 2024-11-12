using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class TenantException : Exception
    {
        public TenantException(string message) : base(message)
        {
        }
    }
}
