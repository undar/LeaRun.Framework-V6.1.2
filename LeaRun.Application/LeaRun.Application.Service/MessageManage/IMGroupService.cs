using LeaRun.Application.Entity.MessageManage;
using LeaRun.Application.IService.MessageManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.26 19:39
    /// 描 述：即时通信群组管理
    /// </summary>
    public class IMGroupService : RepositoryFactory, IMsgGroupService
    {
        /// <summary>
        /// 获取群组列表（即时通信）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMGroupModel> GetList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  g.GroupId ,
                                    g.FullName AS GroupName ,
                                    u.UserId ,
                                    u.UserGroupId
                            FROM    IM_UserGroup u
                                    LEFT JOIN IM_Group g ON u.GroupId = g.GroupId
                            WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            //公司主键
            if (!userId.IsEmpty())
            {
                strSql.Append(" AND u.UserId = @UserId");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));
            }
            return this.BaseRepository().FindList<IMGroupModel>(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取群组里面的用户Id
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public DataTable GetUserIdList(string groupId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  t.GroupId ,
                                    t.UserId
                            FROM    IM_UserGroup t
                            WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            //群组Id
            if (!groupId.IsEmpty())
            {
                strSql.Append(" AND u.GroupId = @GroupId");
                parameter.Add(DbParameters.CreateDbParameter("@GroupId", groupId));
            }
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }

        #region 提交数据
        /// <summary>
        /// 保存群组信息
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void Save(string keyValue, IMGroupEntity entity,List<string> userIdList)
        {
           
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update<IMGroupEntity>(entity);
            }
            else
            {
                IDatabase db = DbFactory.Base().BeginTrans();
                try
                {
                    entity.Create();
                    db.Insert<IMGroupEntity>(entity);

                    foreach (string userOne in userIdList)
                    {
                        IMUserGroupEntity msgusergroupentity = new IMUserGroupEntity();
                        msgusergroupentity.GroupId = entity.GroupId;
                        msgusergroupentity.UserId = userOne;
                        msgusergroupentity.CreateUserId = entity.CreateUserId;
                        msgusergroupentity.CreateUserName = entity.CreateUserName;
                        db.Insert<IMUserGroupEntity>(msgusergroupentity);
                    }
                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 删除群组里的一个联系人
        /// </summary>
        /// <param name="UserGroupId"></param>
        public void RemoveUserId(string UserGroupId)
        {
            string keyValue = UserGroupId;
            this.BaseRepository().Delete<IMUserGroupEntity>(keyValue);
        }
        /// <summary>
        /// 群里增加一个用户
        /// </summary>
        /// <param name="entity"></param>
        public void AddUserId(IMUserGroupEntity entity)
        {
            entity.Create();
            this.BaseRepository().Insert<IMUserGroupEntity>(entity);
        }
        #endregion
    }
}
