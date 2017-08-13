using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-03-31 15:17
    /// 描 述：Excel导入模板表
    /// </summary>
    public class ExcelImportBLL
    {
        private ExcelImportIService service = new ExcelImportService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ExcelImportEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public ExcelImportEntity GetEntityByModuleId(string keyValue)
        {
            return service.GetEntityByModuleId(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<ExcelImportFiledEntity> GetDetails(string keyValue)
        {
            return service.GetDetails(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, ExcelImportEntity entity,List<ExcelImportFiledEntity> entryList)
        {
            try
            {
                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void ImportExcel(string fid, DataTable dt, out DataTable Result)
        {
            service.ImportExcel(fid,dt,out Result);
        } 
        public void UpdateState(string keyValue, int State)
        {
            try
            {
                service.UpdateState(keyValue, State);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
