using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Text;
using System.Data;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleService : RepositoryFactory, FormModuleIService
    {
        #region 获取数据
        public IEnumerable<FormModuleEntity> GetList()
        {
            return this.BaseRepository().FindList<FormModuleEntity>("select * from Form_Module order by Version desc");
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
	                            w.FrmId,
	                            w.FrmName,
	                            w.FrmCategory,
							    t2.ItemName AS FrmTypeName
                            FROM
	                            Form_Module w
						    LEFT JOIN 
                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.FrmCategory
                            WHERE w.DeleteMark = 0 and w.EnabledMark = 1 order by w.FrmType");
           
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FormModuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<FormModuleEntity>();
            var queryParam = queryJson.ToJObject();
            //数据库Id查询
            if (!queryParam["FrmCategory"].IsEmpty())
            {
                string frmCategory = queryParam["FrmCategory"].ToString();
                expression = expression.And(t => t.FrmCategory.Equals(frmCategory));
            }
            else if (!queryParam["Keyword"].IsEmpty())//关键字查询
            {
                string keyWord = queryParam["Keyword"].ToString();
                expression = expression.And(t => t.FrmName.Contains(keyWord));
                expression = expression.Or(t => t.FrmCode.Contains(keyWord));
                expression = expression.Or(t => t.Description.Contains(keyWord));
//                strSql.Append(@" AND ( w.FrmCode LIKE @keyword 
//                                        or w.FrmName LIKE @keyword 
//                                        or w.Description LIKE @keyword 
//                    )");

              
            }
            return this.BaseRepository().FindList<FormModuleEntity>(expression,pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<FormModuleEntity>(keyValue);
        }
       
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<FormModuleContentEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<FormModuleContentEntity>("select * from Form_ModuleContent where FrmId='" + keyValue + "'");        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<FormModuleEntity>(keyValue);
                db.Delete<FormModuleContentEntity>(t => t.FrmId.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
      
        /// <summary>
        /// 更新表单模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                FormModuleEntity entity = new FormModuleEntity();
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
