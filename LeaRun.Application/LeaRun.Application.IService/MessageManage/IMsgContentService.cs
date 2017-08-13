using LeaRun.Application.Entity.MessageManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.30 15:16
    /// 描 述：即时通信群组管理
    /// </summary>
    public interface IMsgContentService
    {
        /// <summary>
        /// 获取消息列表（单对单）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<IMReadModel> GetList(Pagination pagination, string userId, string sendId,string flag = "1");
        /// <summary>
        /// 获取消息列表（群组）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<IMReadModel> GetListByGroupId(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取消息数量列表
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<IMReadNumModel> GetReadList(string userId);
         /// <summary>
        /// 获取某用户某种消息的总数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        DataTable GetReadAllNum(string userId, string status);
        
        #region 提交数据
        /// <summary>
        /// 增加一条消息内容
        /// </summary>
        /// <param name="entity"></param>
        void Add(IMContentEntity entity, DataTable dtGroupUserId);
        /// <summary>
        /// 更新消息状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="sendId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        int Update(string userId, string sendId, string status);
        #endregion
    }
}
