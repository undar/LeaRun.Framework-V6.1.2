using LeaRun.Application.Entity.MessageManage;
using LeaRun.Application.IService.MessageManage;
using LeaRun.Application.Service.MessageManage;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.27 10:54
    /// 描 述：即时通信群组管理
    /// </summary>
    public class IMGroupBLL
    {
        private IMsgGroupService service = new IMGroupService();
        /// <summary>
        /// 获取群组列表（即时通信）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMGroupModel> GetList(string userId)
        {
            return service.GetList(userId);
        }
        /// <summary>
        /// 创建一个群组
        /// </summary>
        /// <param name="groupName">群组名称</param>
        /// <param name="userId">创建者Id</param>
        /// <param name="uesrName"></param>
        /// <param name="userIdList"></param>
        public void Create(string groupName, string userId, string uesrName, List<string> userIdList)
        {
            IMGroupEntity entity = new IMGroupEntity();
            entity.FullName = groupName;
            entity.CreateUserId = userId;
            entity.CreateUserName = uesrName;
            service.Save(null, entity, userIdList);
        }
        /// <summary>
        /// 更新群名字
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="uesrName"></param>
        public void UpdateName(string keyValue, string uesrName)
        {
            IMGroupEntity entity = new IMGroupEntity();
            entity.CreateUserName = uesrName;
            service.Save(null, entity, null);
        }
        /// <summary>
        /// 增加一个组员到群组里面
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="userId"></param>
        public void AddUserId(string groupId, string userId,string createUserId, string createUesrName)
        {
            IMUserGroupEntity entity = new IMUserGroupEntity();
            entity.GroupId = groupId;
            entity.UserId = userId;
            entity.CreateUserId = createUserId;
            entity.CreateUserName = createUesrName;
            service.AddUserId(entity);
        }
        /// <summary>
        /// 删除群组里的一个联系人
        /// </summary>
        /// <param name="UserGroupId"></param>
        public void RemoveUserId(string UserGroupId)
        {
            service.RemoveUserId(UserGroupId);
        }
    }
}
