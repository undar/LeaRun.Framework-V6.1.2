using LeaRun.Application.Entity.AuthorizeManage;
using System.Collections.Generic;

namespace LeaRun.Application.IService.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public interface IModuleService
    {
        #region 获取数据
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        int GetSortCode();
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetList();
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ModuleEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 功能编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue);
        /// <summary>
        /// 功能名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除功能
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">功能实体</param>
        /// <param name="moduleButtonList">按钮实体列表</param>
        /// <param name="moduleColumnList">视图实体列表</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonList, List<ModuleColumnEntity> moduleColumnList);
        #endregion
    }
}
