using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using LeaRun.Data.Repository;

namespace LeaRun.Application.Busines.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleContentBLL
    {
       
        private FormModuleContentIService contentservice = new FormModuleContentService();

        #region 获取数据
      
        /// <summary>
        /// 获取工作流模板对象列表
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        public IEnumerable<FormModuleContentEntity> GetEntityList(string wfSchemeInfoId)
        {
            return contentservice.GetEntityList(wfSchemeInfoId);
        }
        /// <summary>
        /// 获取对象列表（不包括模板内容）
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        public DataTable GetTableList(string frmId)
        {
            return contentservice.GetTableList(frmId);
        }
        /// <summary>
        /// 获取工作流模板对象
        /// </summary>
        /// <param name="contentid"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public FormModuleContentEntity GetContentEntity(string frmId, string version)
        {
            try
            {
                return contentservice.GetEntity(frmId, version);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public FormModuleContentEntity GetEntity(string frmId)
        {
            try
            {
                return contentservice.GetEntity(frmId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    
    }
}
