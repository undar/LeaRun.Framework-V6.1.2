
using LeaRun.Application.Entity.AuthorizeManage;
namespace LeaRun.Application.IService.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单实例
    /// </summary>
    public interface IModuleFormInstanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取一个实体类
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        ModuleFormInstanceEntity GetEntityByObjectId(string objectId);
        #endregion

        #region 提交数据
         /// <summary>
        /// 保存一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SaveEntity(string keyValue, ModuleFormInstanceEntity entity);
        #endregion
    }
}
