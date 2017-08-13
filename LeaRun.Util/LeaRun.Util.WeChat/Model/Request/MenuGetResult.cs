using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.WeChat.Model.Request
{
    public class MenuGetResult : OperationResultsBase
    {
        public Menu menu { get; set; }

        public class Menu
        {

            /// <summary>
            /// 一级菜单数组，个数应为1~3个
            /// </summary>
            /// <returns></returns>
            public List<MenuItem> button { get; set; }
        }
    }
}
