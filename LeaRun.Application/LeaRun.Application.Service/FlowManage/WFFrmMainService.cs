using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
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
    /// 日 期：2016.01.14 11:02
    /// 描 述：表单管理（支持：SqlServer）
    /// </summary>
    public class WFFrmMainService : RepositoryFactory, WFFrmMainIService
    {
        #region 获取数据
        /// <summary>
        /// 获取表单列表分页数据
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
	                            w.FrmMainId,
	                            w.FrmCode,
	                            w.FrmName,
	                            w.FrmType,
	                            t2.ItemName AS FrmTypeName,
	                            w.FrmDbId,
	                            t1.DBName,
	                            w.FrmTable,
                                w.FrmTableId,
                                w.isSystemTable,
	                            w.FrmContent,
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
	                            WF_FrmMain w
                            LEFT JOIN 
	                            Base_DatabaseLink t1 ON t1.DatabaseLinkId = w.FrmDbId
                            LEFT JOIN 
	                            Base_DataItemDetail t2 ON t2.ItemDetailId = w.FrmType
                            WHERE w.DeleteMark = 0 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                //数据库Id查询
                if (!queryParam["FrmType"].IsEmpty())
                {
                    strSql.Append(" AND w.FrmType = @FrmType ");
                    parameter.Add(DbParameters.CreateDbParameter("@FrmType", queryParam["FrmType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.FrmCode LIKE @keyword 
                                        or w.FrmName LIKE @keyword 
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
        /// 获取表单数据ALL(用于下拉框)
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                            w.FrmMainId,
	                            w.FrmName,
	                            w.FrmType,
							    t2.ItemName AS FrmTypeName
                            FROM
	                            WF_FrmMain w
						    LEFT JOIN 
                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.FrmType
                            WHERE w.DeleteMark = 0 and w.EnabledMark = 1 order by w.FrmType");
                var parameter = new List<DbParameter>();
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 设置表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFFrmMainEntity GetForm(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFFrmMainEntity>(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                WFFrmMainEntity entity = new WFFrmMainEntity();
                entity.Modify(keyValue);
                entity.DeleteMark = 1;
                this.BaseRepository().Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单
        /// </summary>
        /// <param name="entity">表单模板实体类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int SaveForm(string keyValue,WFFrmMainEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                return 1;
            }
            catch(Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 更新表单模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                WFFrmMainEntity entity = new WFFrmMainEntity();
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
