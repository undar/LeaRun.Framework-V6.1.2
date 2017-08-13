using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
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

namespace LeaRun.Application.Service.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单
    /// </summary>
    public class ModuleFormService : RepositoryFactory, IModuleFormService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据(管理页面调用)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                m.FormId,
	                                m.ModuleId,
	                                m1.FullName as ModuleName,
	                                m.EnCode,
	                                m.FullName,
	                                m.SortCode,
	                                m.DeleteMark,
	                                m.EnabledMark,
	                                m.Description,
	                                m.CreateDate,
	                                m.CreateUserId,
	                                m.CreateUserName,
	                                m.ModifyDate,
	                                m.ModifyUserId,
	                                m.ModifyUserName
                                FROM
	                                Base_ModuleForm m
                                LEFT JOIN Base_Module m1 ON m1.ModuleId = m.ModuleId
                                WHERE m.DeleteMark = 0");

                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( m1.FullName LIKE @keyword 
                                        or m.FullName LIKE @keyword 
                                        or m.CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取一个实体类
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ModuleFormEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<ModuleFormEntity>(keyValue);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 通过模块Id获取系统表单
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ModuleFormEntity GetEntityByModuleId(string moduleId)
        {
            try
            {
                var expression = LinqExtensions.True<ModuleFormEntity>();
                expression = expression.And(t => t.ModuleId.Equals(moduleId));
                return this.BaseRepository().FindEntity<ModuleFormEntity>(expression);
            }
            catch
            {
                throw;
            }
          
        }
        /// <summary>
        /// 判断模块是否已经有系统表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public bool IsExistModuleId(string keyValue,string moduleId)
        {
            var expression = LinqExtensions.True<ModuleFormEntity>();
            if(string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ModuleId.Equals(moduleId));
            }
            else
            {
                expression = expression.And(t => t.ModuleId.Equals(moduleId) && t.FormId != keyValue);
            }
            ModuleFormEntity entity = this.BaseRepository().FindEntity<ModuleFormEntity>(expression);
            return entity == null ? false : true;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, ModuleFormEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    return this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    return this.BaseRepository().Update(entity);
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 虚拟删除一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int VirtualDelete(string keyValue)
        {
            try
            {
                ModuleFormEntity entity = this.BaseRepository().FindEntity<ModuleFormEntity>(keyValue);
                if (entity != null)
                {
                    entity.DeleteMark = 1;
                    return this.BaseRepository().Update(entity);
                }
                else
                {
                    throw (new Exception("没有该记录无法删除"));
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
