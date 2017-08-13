using LeaRun.Application.Entity.MessageManage;
using LeaRun.Application.IService.MessageManage;
using LeaRun.Application.Service.MessageManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.Busines.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.30 15:16
    /// 描 述：即时通信群组管理
    /// </summary>
    public class IMContentBLL
    {
        private IMsgContentService server = new IMContentService();
        private IMsgGroupService groupServer = new IMGroupService();
        /// <summary>
        /// 获取消息列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<IMReadModel> GetListOneToOne(Pagination pagination, string userId,string sendId)
        {
            return server.GetList(pagination, userId, sendId);
        }
        /// <summary>
        /// 获取消息列表（群组）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<IMReadModel> GetListByGroupId(Pagination pagination, string queryJson)
        {
            return server.GetListByGroupId(pagination, queryJson);
        }
         /// <summary>
        /// 获取消息数量列表
        /// </summary>
        /// <param name="queryJson">readStatus,userId</param>
        /// <returns></returns>
        public IEnumerable<IMReadNumModel> GetReadList(string userId)
        {
            return server.GetReadList(userId);
        }
         /// <summary>
        /// 获取某用户某种消息的总数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public string GetReadAllNum(string userId, string status)
        {
            string num = "0";
            DataTable dt =  server.GetReadAllNum(userId, status);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["unReadNum"].ToString() != "")
                {
                    num = dt.Rows[0]["unReadNum"].ToString();
                }
            }
            return num;
        }

        #region 提交数据
        /// <summary>
        /// 增加一条一对一消息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendId"></param>
        /// <param name="createName"></param>
        /// <param name="message"></param>
        public void AddOneToOne(string userId,string sendId,string createName,string message)
        {
            IMContentEntity entity = new IMContentEntity();
            entity.SendId = sendId;
            entity.ToId = userId;
            entity.MsgContent = message;
            entity.IsGroup = 0;
            entity.CreateUserId = sendId;
            entity.CreateUserName = createName;

            server.Add(entity,null);
        }
        /// <summary>
        /// 增加群组消息
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="sendId"></param>
        /// <param name="createName"></param>
        /// <param name="message"></param>
        public void AddGroup(string groupId, string sendId, string createName, string message,out DataTable dtUserId)
        {
            IMContentEntity entity = new IMContentEntity();
            entity.SendId = sendId;
            entity.ToId = groupId;
            entity.MsgContent = message;
            entity.IsGroup = 0;
            entity.CreateUserId = sendId;
            entity.CreateUserName = createName;

            DataTable dt = groupServer.GetUserIdList(groupId);
            dtUserId = dt;
            server.Add(entity, dt);
        }
        /// <summary>
        /// 更新消息的阅读状态
        /// </summary>
        /// <param name="sendId"></param>
        /// <param name="isGroup"></param>
        public void UpDateSatus(string userId, string sendId,string status)
        {
            server.Update(userId, sendId, status);
        }

        #endregion

    }
}
