using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public interface ILogService
    {
        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        LogEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        void RemoveLog(int categoryId, string keepTime);
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        void WriteLog(LogEntity logEntity);
        #endregion
    }
}
