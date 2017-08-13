using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request.SendMessage
{
    class SendText : MessageSend
    {
        public override string msgtype
        {
            get { return "text"; }
        }

        [IsNotNull]
        public SendText.SendItem text { get; set; }

        public class SendItem
        {
            /// <summary>
            /// 消息内容
            /// </summary>
            /// <returns></returns>
            [IsNotNull]
            public string content { get; set; }
        }
    } 
}
