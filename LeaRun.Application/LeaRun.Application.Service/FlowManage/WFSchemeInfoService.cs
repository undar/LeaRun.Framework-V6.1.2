using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.BaseManage;
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

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.02.23 10:02
    /// 描 述：工作流模板信息表操作（支持：SqlServer）
    /// </summary>
    public class WFSchemeInfoService : RepositoryFactory, WFSchemeInfoIService
    {
        private IUserService userservice = new UserService();
        #region 获取数据
        /// <summary>
        /// 获取流程列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            w.Id,
	                            w.SchemeCode,
	                            w.SchemeName,
	                            w.SchemeType,
	                            t2.ItemName AS SchemeTypeName,
	                            w.SortCode,
	                            w.DeleteMark,
	                            w.EnabledMark,
	                            w.Description,
	                            w.CreateDate,
	                            w.CreateUserId,
	                            w.CreateUserName,
	                            w.ModifyDate,
	                            w.ModifyUserId,
	                            w.ModifyUserName,
                                w.AuthorizeType
                            FROM
	                            WF_SchemeInfo w
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                            WHERE w.DeleteMark = 0 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.SchemeCode LIKE @keyword 
                                        or w.SchemeName LIKE @keyword 
                                        or w.Description LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程列表数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            try
            {
                string dd =OperatorProvider.Provider.Current().ObjectId;
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            distinct w.Id,
	                            w.SchemeCode,
	                            w.SchemeName,
	                            w.SchemeType,
	                            t2.ItemName AS SchemeTypeName,
	                            w.SortCode,
	                            w.DeleteMark,
	                            w.EnabledMark,
	                            w.Description,
	                            w.CreateDate,
	                            w.CreateUserId,
	                            w.CreateUserName,
	                            w.ModifyDate,
	                            w.ModifyUserId,
	                            w.ModifyUserName
                            FROM
	                            WF_SchemeInfo w
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                            LEFT JOIN 
	                            WF_SchemeInfoAuthorize w2 ON w2.SchemeInfoId = w.Id
                            WHERE w.DeleteMark = 0 AND w.EnabledMark = 1
                            AND ( w.AuthorizeType = 0  ");
                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    if (OperatorProvider.Provider.Current().ObjectId != "")
                    {
                        strSql.Append(string.Format(" OR w2.ObjectId in ('{0}','{1}') )", OperatorProvider.Provider.Current().ObjectId.Replace(",", "','"), OperatorProvider.Provider.Current().UserId));
                    }
                    else
                    {
                        strSql.Append(" ) ");
                    }
                }
                else
                {
                    strSql.Append(" OR w.AuthorizeType = 1 ) ");
                }
               

                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.SchemeCode LIKE @keyword 
                                        or w.SchemeName LIKE @keyword 
                                        or w.Description LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(" order by CreateDate desc");
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程模板列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            w1.*,
	                            t2.ItemName,
                                t2.ItemDetailId as ItemId
                            FROM
                                WF_SchemeInfo w1
                            LEFT JOIN Base_DataItemDetail t2 ON t2.ItemDetailId = w1.SchemeType
                            ORDER BY t2.SortCode");
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 设置流程
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFSchemeInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFSchemeInfoEntity>(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        public IEnumerable<WFSchemeInfoAuthorizeEntity> GetAuthorizeEntityList(string schemeInfoId)
        {
            try
            {
                var expression = LinqExtensions.True<WFSchemeInfoAuthorizeEntity>();
                expression = expression.And(t => t.SchemeInfoId == schemeInfoId);
                return this.BaseRepository().FindList<WFSchemeInfoAuthorizeEntity>(expression);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 更新流程模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                WFSchemeInfoEntity entity = new WFSchemeInfoEntity();
                entity.Modify(keyValue);
                entity.EnabledMark = state;
                this.BaseRepository().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        
    }
}
