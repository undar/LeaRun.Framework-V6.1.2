using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaRun.Util.WeChat.Helper;

namespace LeaRun.Util.WeChat.Model
{
    public class HttpPostRequest : IHttpSend
    {
        public string Send(string url, string data)
        {
            return new HttpHelper().Post(url, data, Encoding.UTF8, Encoding.UTF8);
        }
    }
}
