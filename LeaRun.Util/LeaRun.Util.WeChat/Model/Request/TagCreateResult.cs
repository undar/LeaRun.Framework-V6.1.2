using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    class TagCreateResult : OperationResultsBase
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        /// <returns></returns>
        public string tagid { get; set; }
    }
}
