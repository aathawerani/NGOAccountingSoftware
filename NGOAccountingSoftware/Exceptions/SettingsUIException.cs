using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication.Exceptions
{
    class SettingsUIException : Exception
    {
        public SettingsUIException(string message) : base(message)
        {
        }
    }
}
