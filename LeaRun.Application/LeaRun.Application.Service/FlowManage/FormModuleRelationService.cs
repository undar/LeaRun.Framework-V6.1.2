using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using LeaRun.Application.Entity.AuthorizeManage;
using System.Text;
using System.Data;
namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:46
    /// 描 述：表单关联表
    /// </summary>
    public class FormModuleRelationService : RepositoryFactory<FormModuleRelationEntity>, FormModuleRelationIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FormModuleRelationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select Id,ModuleContentId,a.FrmId,a.FrmName,FrmVersion,Version as NewVersion,FrmKind,ObjectId,ObjectButtonId,ObjectName,a.CreateDate,a.CreateUserId,a.CreateUserName from Form_ModuleRelation a LEFT JOIN Form_Module b ON a.FrmId = b.FrmId where 1=1");
            var queryParam = queryJson.ToJObject();
            //数据库Id查询
            if (!queryParam["FrmKind"].IsEmpty())
            {
                string FrmKind = queryParam["FrmKind"].ToString();
                strSql.Append(" and FrmKind="+FrmKind+"");
            }
            if (!queryParam["Keyword"].IsEmpty())
            {
                string keyword = queryParam["Keyword"].ToString();
                strSql.Append(" and ObjectName like '%" + keyword + "%'");
            }
            return this.BaseRepository().FindList(strSql.ToString(), pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FormModuleRelationEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleRelationEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
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
                db.Delete<FormModuleRelationEntity>(keyValue);
                FormModuleRelationEntity entity = db.FindEntity<FormModuleRelationEntity>(keyValue);
                db.Delete<ModuleEntity>(t => t.ModuleId.Equals(entity.ObjectId));
                db.Commit();
            }
           

            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }
        public void UpdateForm(string keyValue)
        {
            FormModuleRelationEntity entity = new FormModuleRelationEntity();
            FormModuleRelationEntity entity1 = this.BaseRepository().FindEntity(keyValue);

            DataTable dt = this.BaseRepository().FindTable("select top 1 Id,FrmVersion from Form_ModuleContent where FrmId='" + entity1.FrmId + "' order by FrmVersion desc");

            entity.ModuleContentId = dt.Rows[0][0].ToString();
            entity.FrmVersion = dt.Rows[0][1].ToString();
            entity.Modify(keyValue);
            this.BaseRepository().Update(entity);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FormModuleRelationEntity entity, ModuleEntity module)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
           

            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //关联
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //模块,先删除，再新增？
                    
                }
                else
                {
                    //取得ModuleCotentId
                    DataTable dt = this.BaseRepository().FindTable("select top 1 Id from Form_ModuleContent where FrmId='" + entity.FrmId + "' order by FrmVersion desc");
                    entity.ModuleContentId=dt.Rows[0][0].ToString();
                    //新增关联
                    entity.Create();
                    entity.ObjectId = System.Guid.NewGuid().ToString();
                    db.Insert(entity);
                    //模块
                    if (entity.FrmKind==0)
                    {
                        module.Create();
                        module.ModuleId = entity.ObjectId;
                        module.UrlAddress = module.UrlAddress+"?Id=" + entity.Id;
                        db.Insert(module);
                    }
                }
                db.Commit();
            }
            catch (System.Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
