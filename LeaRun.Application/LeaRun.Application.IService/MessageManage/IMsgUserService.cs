using LeaRun.Application.Entity.MessageManage;
using System.Collections.Generic;

namespace LeaRun.Application.IService.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.26 11:14
    /// 描 述：即时通信用户管理
    /// </summary>
    public interface IMsgUserService
    {
        /// <summary>
        /// 获取联系人列表（即时通信）
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMUserModel> GetList(string OrganizeId);
    }
}
