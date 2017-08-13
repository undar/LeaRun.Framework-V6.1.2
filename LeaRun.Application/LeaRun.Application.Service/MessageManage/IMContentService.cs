using LeaRun.Application.Entity.MessageManage;
using LeaRun.Application.IService.MessageManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
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
    public class IMContentService : RepositoryFactory, IMsgContentService
    {
        /// <summary>
        /// 获取消息列表（单对单）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<IMReadModel> GetList(Pagination pagination, string userId, string sendId,string flag = "1")
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  * from(
                            SELECT  t1.ReadId ,
                                    t1.ContentId ,
                                    t1.CreateDate ,
                                    t1.UserId ,
                                    t2.SendId ,
                                    t2.MsgContent as Content
                            FROM    IM_Read t1
                                    INNER JOIN IM_Content t2 ON t1.ContentId = t2.ContentId and t2.IsGroup = 0)T 
                            WHERE   1=1 ");
            var parameter = new List<DbParameter>();
            //信息接收者
            if (!userId.IsEmpty())
            {
                if (flag == "1" && !sendId.IsEmpty())//获取一个聊天窗口的消息记录
                {
                    strSql.Append(" AND ( T.UserId = @UserId or T.UserId = @UserId2 )");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", userId.ToString()));
                    parameter.Add(DbParameters.CreateDbParameter("@UserId2", sendId.ToString()));
                }
                else
                {
                    strSql.Append(" AND T.UserId = @UserId");
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", userId.ToString()));
                }
            }
            //信息发送者
            if (!sendId.IsEmpty())
            {
                if (flag == "1" && !userId.IsEmpty())//获取一个聊天窗口的消息记录
                {
                    strSql.Append(" AND ( T.SendId = @SendId or T.SendId = @SendId2 )");
                    parameter.Add(DbParameters.CreateDbParameter("@SendId", sendId.ToString()));
                    parameter.Add(DbParameters.CreateDbParameter("@SendId2", userId.ToString()));
                }
                else
                {
                    strSql.Append(" AND T.SendId = @SendId");
                    parameter.Add(DbParameters.CreateDbParameter("@SendId", sendId.ToString()));
                }
            }
            return this.BaseRepository().FindList<IMReadModel>(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取消息列表（群组）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<IMReadModel> GetListByGroupId(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  t.ContentId ,
                                    t.SendId ,
                                    t.ToId ,
                                    t.MsgContent ,
                                    t.CreateDate
                            FROM    IM_Content t
                            WHERE   t.IsGroup = 1");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            //群组Id
            if (!queryParam["groupId"].IsEmpty())
            {
                strSql.Append(" AND t.ToId = @ToId");
                parameter.Add(DbParameters.CreateDbParameter("@ToId", queryParam["groupId"].ToString()));
            }

            return this.BaseRepository().FindList<IMReadModel>(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 获取消息数量列表
        /// </summary>
        /// <param name="queryJson">readStatus,userId</param>
        /// <returns></returns>
        public IEnumerable<IMReadNumModel> GetReadList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT
	                            SUM( CASE WHEN t1.ReadStatus = 0 THEN 1 ELSE 0 END )AS unReadNum,
	                            t1.SendId,
	                            t1.UserId
                            FROM
	                            IM_Read t1
                            WHERE
	                            1 = 1
                            ");
            var parameter = new List<DbParameter>();
            //接收者Id
            if (!userId.IsEmpty())
            {
                strSql.Append(" AND ( t1.UserId = @UserId OR t1.SendId = @UserId )");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId.ToString()));
            }
            strSql.Append(@" GROUP BY t1.SendId, t1.UserId
                             ORDER BY MAX (t1.CreateDate) DESC");
            return this.BaseRepository().FindList<IMReadNumModel>(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 获取某用户某种消息的总数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable GetReadAllNum(string userId, string status)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  COUNT(ReadId) AS unReadNum
                            FROM    IM_Read 
                            WHERE   1 = 1 ");
            var parameter = new List<DbParameter>();

            //消息状态
            if (!status.IsEmpty())
            {
                strSql.Append(" AND ReadStatus = @ReadStatus");
                parameter.Add(DbParameters.CreateDbParameter("@ReadStatus", status));
            }
            //接收者Id
            if (!userId.IsEmpty())
            {
                strSql.Append(" AND UserId = @UserId");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId.ToString()));
            }
            return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
        }
        #region 提交数据
        /// <summary>
        /// 增加一条消息内容
        /// </summary>
        /// <param name="entity"></param>
        public void Add(IMContentEntity entity, DataTable dtGroupUserId)
        {
            IDatabase db = DbFactory.Base().BeginTrans();
            try
            {
                //增加一条消息内容
                entity.Create();
                db.Insert<IMContentEntity>(entity);
                if (entity.IsGroup == 1)
                {
                    foreach (DataRow item in dtGroupUserId.Rows)
                    {
                        IMReadEntity msgreadentity = new IMReadEntity();
                        msgreadentity.Create();
                        msgreadentity.ContentId = entity.ContentId;
                        msgreadentity.UserId = item["userId"].ToString();
                        msgreadentity.SendId = entity.ToId;//群组消息发送者为群组Id
                        msgreadentity.CreateUserId = entity.CreateUserId;
                        msgreadentity.CreateUserName = entity.CreateUserName;
                        msgreadentity.ReadStatus = 0;
                        db.Insert<IMReadEntity>(msgreadentity);
                    }
                }
                else
                {
                    IMReadEntity msgreadentity = new IMReadEntity();
                    msgreadentity.Create();
                    msgreadentity.ContentId = entity.ContentId;
                    msgreadentity.UserId = entity.ToId;
                    msgreadentity.SendId = entity.SendId;
                    msgreadentity.CreateUserId = entity.CreateUserId;
                    msgreadentity.CreateUserName = entity.CreateUserName;
                    msgreadentity.ReadStatus = 0;
                    db.Insert<IMReadEntity>(msgreadentity);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 更新消息状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int Update(string userId, string sendId, string status)
        {
            var strSql = new StringBuilder();
            strSql.Append("UPDATE IM_Read");
            var parameter = new List<DbParameter>();
            //更新状态
            if (!status.IsEmpty())
            {
                strSql.Append(" SET ReadStatus = @ReadStatus");
                parameter.Add(DbParameters.CreateDbParameter("@ReadStatus", status));
            }
            strSql.Append(" WHERE 1 = 1 ");
            //发送者
            if (!sendId.IsEmpty())
            {
                strSql.Append(" AND SendId = @SendId");
                parameter.Add(DbParameters.CreateDbParameter("@SendId", sendId));
            }
            //接收者
            if (!userId.IsEmpty())
            {
                strSql.Append(" AND UserId = @UserId");
                parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));
            }
            return this.BaseRepository().ExecuteBySql(strSql.ToString(), parameter.ToArray());
        }
        #endregion
    }
}
