using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Data;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleContentService : RepositoryFactory, FormModuleContentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="frmId">工作流表单模板表主键</param>
        /// <param name="version">模板版本号</param>
        /// <returns></returns>
        public FormModuleContentEntity GetEntity(string frmId, string version)
        {
            try
            {
                var expression = LinqExtensions.True<FormModuleContentEntity>();
                expression = expression.And(t => t.FrmId == frmId).And(t => t.FrmVersion == version);
                return this.BaseRepository().FindEntity<FormModuleContentEntity>(expression);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleContentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<FormModuleContentEntity>(keyValue);
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="frmId">工作流模板信息表Id</param>
        /// <returns></returns>
        public IEnumerable<FormModuleContentEntity> GetEntityList(string frmId)
        {
            var expression = LinqExtensions.True<FormModuleContentEntity>();
            expression = expression.And(t => t.FrmId == frmId);
            return this.BaseRepository().FindList<FormModuleContentEntity>(expression);
        }
        /// <summary>
        /// 获取对象列表（不包括模板内容）
        /// </summary>
        /// <param name="frmId">工作流模板Id</param>
        /// <returns></returns>
        public DataTable GetTableList(string frmId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT Id,FrmId,FrmVersion FROM Form_ModuleContent where FrmId = '" + frmId + "' Order by  FrmVersion DESC ");
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
