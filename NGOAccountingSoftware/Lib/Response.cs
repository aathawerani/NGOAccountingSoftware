using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustApplication
{
    public enum ResponseType { Success = 0, Failure}
    class Response
    {
        public string Message { get; private set; }
        ResponseType RespType;
        public Response(ResponseType responsetype, string _Message)
        {
            RespType = responsetype;
            Message = _Message;
        }

        public bool IsSuccess()
        {
            if (RespType == ResponseType.Success)
                return true;
            return false;
        }

        public void AddMessage(string message)
        {
            Message += "\n" + message;
        }

    }
}
