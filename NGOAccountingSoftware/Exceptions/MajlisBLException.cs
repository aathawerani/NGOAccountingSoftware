using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class MajlisBLException : Exception
    {
        public MajlisBLException(string message) : base(message)
        {
        }
    }
}
