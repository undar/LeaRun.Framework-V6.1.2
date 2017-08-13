using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    /// <summary>
    /// 获取部门成员接口返回结果
    /// </summary>
    public class UserSimplelistResult : OperationResultsBase
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        /// <returns></returns>
        public List<UserSimplelistItem> userlist { get; set; }

        public class UserSimplelistItem
        {
            /// <summary>
            /// 员工UserID。对应管理端的帐号
            /// </summary>
            /// <returns></returns>
            public string userid { get; set; }

            /// <summary>
            /// 成员名称
            /// </summary>
            /// <returns></returns>
            public string name { get; set; }
        }
    }
}
