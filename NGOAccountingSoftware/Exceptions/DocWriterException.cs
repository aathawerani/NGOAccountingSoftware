using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class DocWriterException: Exception
    {
        public DocWriterException(string message) : base(message)
        {
        }
    }
}
