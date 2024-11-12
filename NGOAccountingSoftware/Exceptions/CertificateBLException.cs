using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class CertificateBLException : Exception
    {
        public CertificateBLException(string message) : base(message)
        {
        }
    }
}
