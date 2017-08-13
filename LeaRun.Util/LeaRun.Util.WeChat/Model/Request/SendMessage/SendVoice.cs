using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request.SendMessage
{
    class SendVoice : MessageSend
    {
        public override string msgtype
        {
            get { return "voice"; }
        }

        [IsNotNull]
        public SendVoice.SendItem voice { get; set; }

        public class SendItem
        {
            public string media_id { get; set; }
        }
    }
}
