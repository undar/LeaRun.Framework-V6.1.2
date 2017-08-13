using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.ReportManage;
using LeaRun.Application.IService.ReportManage;
using LeaRun.Application.Service.ReportManage;
using System;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.Busines.ReportManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板管理
    /// </summary>
    public class RptTempBLL
    {
        private IRptTempService service = new RptTempService();

        #region 获取数据
        /// <summary>
        /// 报表模板列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RptTempEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 报表模板实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RptTempEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获得报表数据
        /// </summary>
        /// <param name="reportId">主键</param>
        /// <returns></returns>
        public string GetReportData(string reportId)
        {
            return service.GetReportData(reportId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除报表模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存报表模板表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="rptTempEntity">报表实体</param>
        /// <param name="moduleEntity">模块实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RptTempEntity rptTempEntity, ModuleEntity moduleEntity)
        {
            try
            {
                service.SaveForm(keyValue, rptTempEntity, moduleEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
