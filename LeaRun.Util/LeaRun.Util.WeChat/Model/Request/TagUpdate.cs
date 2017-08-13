using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    public class TagUpdate : OperationRequestBase<OperationResultsBase,HttpPostRequest>
    {
        protected override string Url()
        {
            return "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token=ACCESS_TOKEN";
        }

        [IsNotNull]
        public string tagid { get; set; }

        [IsNotNull]
        [Length(1,64)]
        public string tagname { get; set; }
    }
}
