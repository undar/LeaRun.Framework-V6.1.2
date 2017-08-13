using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request.SendMessage
{
    class SendImage : MessageSend
    {
        public override string msgtype
        {
            get { return "image"; }
        }

        [IsNotNull]
        public SendImage.SendItem image { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
