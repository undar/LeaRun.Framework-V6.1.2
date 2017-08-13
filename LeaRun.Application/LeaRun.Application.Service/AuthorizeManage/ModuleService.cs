using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class ModuleService : RepositoryFactory<ModuleEntity>, IModuleService
    {
        #region 获取数据
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public int GetSortCode()
        {
            int sortCode = this.BaseRepository().IQueryable().Max(t => t.SortCode).ToInt();
            if (!string.IsNullOrEmpty(sortCode.ToString()))
            {
                return sortCode + 1;
            }
            return 100001;
        }
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM bASE_Module Order By SortCode");
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 功能编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<ModuleEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ModuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<ModuleEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ModuleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                int count = db.IQueryable<ModuleEntity>(t => t.ParentId == keyValue).Count();
                if (count > 0)
                {
                    throw new Exception("当前所选数据有子节点数据！");
                }
                db.Delete<ModuleEntity>(keyValue);
                db.Delete<ModuleButtonEntity>(t => t.ModuleId.Equals(keyValue));
                db.Delete<ModuleColumnEntity>(t => t.ModuleId.Equals(keyValue));

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">功能实体</param>
        /// <param name="moduleButtonList">按钮实体列表</param>
        /// <param name="moduleColumnList">视图实体列表</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonList, List<ModuleColumnEntity> moduleColumnList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    moduleEntity.Modify(keyValue);
                    db.Update(moduleEntity);
                }
                else
                {
                    moduleEntity.Create();
                    db.Insert(moduleEntity);
                }
                db.Delete<ModuleButtonEntity>(t => t.ModuleId.Equals(keyValue));
                if (moduleButtonList != null)
                {
                    db.Insert(moduleButtonList);
                }
                db.Delete<ModuleColumnEntity>(t => t.ModuleId.Equals(keyValue));
                if (moduleColumnList != null)
                {
                    db.Insert(moduleColumnList);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
