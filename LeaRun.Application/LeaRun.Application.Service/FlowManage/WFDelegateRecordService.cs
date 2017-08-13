using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：工作流委托记录操作类（支持：SqlServer）
    /// </summary>
    public class WFDelegateRecordService : RepositoryFactory, WFDelegateRecordIService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据(type 1：委托记录，其他：被委托记录)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson,int type, string userId = null)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
	                                w.WFDelegateRuleId,
	                                w.FromUserId,
	                                w.FromUserName,
	                                w.ToUserId,
	                                w.ToUserName,
	                                w.CreateDate,
	                                w.ProcessId,
	                                w.ProcessCode,
	                                w.ProcessName
                                FROM
	                                WF_DelegateRecord w
                                Where 1=1 
                               ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!string.IsNullOrEmpty(userId))
                {
                    if (type == 1)
                    {
                        strSql.Append(@" AND ( w.FromUserId = @UserId )");
                    }
                    else
                    {
                        strSql.Append(@" AND ( w.ToUserId = @UserId )");
                    }
                    parameter.Add(DbParameters.CreateDbParameter("@UserId", userId));
                }
                if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.ToUserName LIKE @keyword  or w.ProcessName LIKE @keyword or w.ProcessCode  LIKE @keyword  )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实体数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, WFDelegateRecordEntity entity)
        {
            try
            {
                int num;
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    num = this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    num = this.BaseRepository().Update(entity);
                }
                return num;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
