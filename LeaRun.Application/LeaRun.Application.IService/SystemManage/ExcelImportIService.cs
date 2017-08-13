using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-03-31 15:17
    /// 描 述：Excel导入模板表
    /// </summary>
    public interface ExcelImportIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<ExcelImportEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ExcelImportEntity GetEntity(string keyValue);
        ExcelImportEntity GetEntityByModuleId(string keyValue);
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        IEnumerable<ExcelImportFiledEntity> GetDetails(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ExcelImportEntity entity,List<ExcelImportFiledEntity> entryList);
        void ImportExcel(string fid, DataTable dt, out DataTable Result);
        void UpdateState(string keyValue, int State);
        #endregion
    }
}
