using LeaRun.Application.Entity.MessageManage;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.27 10:50
    /// 描 述：即时通信群组管理
    /// </summary>
    public interface IMsgGroupService
    {
        /// <summary>
        /// 获取群组列表（即时通信）
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMGroupModel> GetList(string userId);
        /// <summary>
        /// 获取群组里面的用户Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        DataTable GetUserIdList(string groupId);
        /// <summary>
        /// 保存群组信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void Save(string keyValue, IMGroupEntity entity, List<string> userIdList);
        /// <summary>
        /// 删除群组里的一个联系人
        /// </summary>
        /// <param name="UserGroupId"></param>
        void RemoveUserId(string UserGroupId);
        /// <summary>
        /// 群里增加一个用户
        /// </summary>
        /// <param name="entity"></param>
        void AddUserId(IMUserGroupEntity entity);
    }
}
