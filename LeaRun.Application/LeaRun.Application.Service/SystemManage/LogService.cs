using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.1.8 9:56
    /// 描 述：系统日志
    /// </summary>
    public class LogService : RepositoryFactory<LogEntity>, ILogService
    {
        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<LogEntity>();
            var queryParam = queryJson.ToJObject();
            //日志分类
            if (!queryParam["Category"].IsEmpty())
            {
                int categoryId = queryParam["CategoryId"].ToInt();
                expression = expression.And(t => t.CategoryId == categoryId);
            }
            //操作时间
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.OperateTime >= startTime && t.OperateTime <= endTime);
            }
            //操作用户Id
            if (!queryParam["OperateUserId"].IsEmpty())
            {
                string OperateUserId = queryParam["OperateUserId"].ToString();
                expression = expression.And(t => t.OperateUserId == OperateUserId);
            }
            //操作用户账户
            if (!queryParam["OperateAccount"].IsEmpty())
            {
                string OperateAccount = queryParam["OperateAccount"].ToString();
                expression = expression.And(t => t.OperateAccount.Contains(OperateAccount));
            }
            //操作类型
            if (!queryParam["OperateType"].IsEmpty())
            {
                string operateType = queryParam["OperateType"].ToString();
                expression = expression.And(t => t.OperateType == operateType);
            }
            //功能模块
            if (!queryParam["Module"].IsEmpty())
            {
                string module = queryParam["Module"].ToString();
                expression = expression.And(t => t.Module.Contains(module));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        public void RemoveLog(int categoryId, string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")//保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")//保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")//保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            var expression = LinqExtensions.True<LogEntity>();
            expression = expression.And(t => t.OperateTime <= operateTime);
            expression = expression.And(t => t.CategoryId == categoryId);
            this.BaseRepository().Delete(expression);
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        public void WriteLog(LogEntity logEntity)
        {
            logEntity.LogId = Guid.NewGuid().ToString();
            logEntity.OperateTime = DateTime.Now;
            logEntity.DeleteMark = 0;
            logEntity.EnabledMark = 1;
            logEntity.IPAddress = Net.Ip;
            logEntity.Host = Net.Host;
            logEntity.Browser = Net.Browser;
            this.BaseRepository().Insert(logEntity);
        }
        #endregion
    }
}
