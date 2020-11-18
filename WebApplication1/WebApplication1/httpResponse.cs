using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class httpResponse<T>
    {
        private T data;
        private int code;
        private string msg;

        public httpResponse(string msg,int code,T data)
        {
            this.Data = data;
            this.Code = code;
            this.Msg = msg;
        }

        public T Data { get => data; set => data = value; }
        public int Code { get => code; set => code = value; }
        public string Msg { get => msg; set => msg = value; }
    }
}
