using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class FileManagerException : Exception
    {
        public FileManagerException(string message) : base(message)
        {
        }
    }
}
