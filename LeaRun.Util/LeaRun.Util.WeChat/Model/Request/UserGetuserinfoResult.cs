using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    public class UserGetuserinfoResult : OperationResultsBase
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        /// <returns></returns>
        public string UserId { get; set; }
    }
}
