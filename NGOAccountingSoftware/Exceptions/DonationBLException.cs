using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class DonationBLException : Exception
    {
        public DonationBLException(string message) : base(message)
        {
        }
    }
}
