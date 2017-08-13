using LeaRun.Application.Entity.MessageManage;
using System.Collections.Generic;

namespace LeaRun.SOA.IM
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.26 9:43
    /// 描 述：静态变量存储联系人信息
    /// </summary>
    public class UserStorage
    {
        private static bool _isSyncUserList = true;
        private static Dictionary<string, IMUserModel> _userAllList = new Dictionary<string,IMUserModel>();
        private static Dictionary<string, List<string>> _userOnlineList = new Dictionary<string, List<string>>();
        
        public static bool isSyncUserList
        {
            set { _isSyncUserList = value; }
            get { return _isSyncUserList; }
        }
        /// <summary>
        /// 全部用列表
        /// </summary>
        public static Dictionary<string, IMUserModel> userAllList
        {
            set { _userAllList = value; }
            get { return _userAllList; }
        }
        /// <summary>
        /// 在线用户列表
        /// </summary>
        public static Dictionary<string, List<string>> userOnlineList
        {
            set { _userOnlineList = value; }
            get { return _userOnlineList; }
        }
    }
}
