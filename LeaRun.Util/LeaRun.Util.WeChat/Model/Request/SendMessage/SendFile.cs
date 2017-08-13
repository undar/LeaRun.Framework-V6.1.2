using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request.SendMessage
{
    class SendFile : MessageSend
    {
        public override string msgtype
        {
            get { return "file"; }
        }

        [IsNotNull]
        public SendFile.SendItem file { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
