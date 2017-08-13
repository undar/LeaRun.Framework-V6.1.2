using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Util.WebControl;
using System.Data;

namespace LeaRun.Application.IService.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单
    /// </summary>
    public interface IModuleFormService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取一个实体类
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        ModuleFormEntity GetEntity(string keyValue);
        /// <summary>
        /// 通过模块Id获取系统表单
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        ModuleFormEntity GetEntityByModuleId(string moduleId);
         /// <summary>
        /// 判断模块是否已经有系统表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        bool IsExistModuleId(string keyValue, string moduleId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SaveEntity(string keyValue, ModuleFormEntity entity);
        /// <summary>
        /// 虚拟删除一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int VirtualDelete(string keyValue);
        #endregion
    }
}
