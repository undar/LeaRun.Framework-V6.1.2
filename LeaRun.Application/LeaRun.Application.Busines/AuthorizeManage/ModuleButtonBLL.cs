using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.BaseManage;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonBLL
    {
        private IModuleButtonService service = new ModuleButtonService();

        #region 获取数据
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 按钮列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetList(string moduleId)
        {
            return service.GetList(moduleId);
        }
        /// <summary>
        /// 按钮实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleButtonEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 复制按钮
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="moduleId">功能主键</param>
        /// <returns></returns>
        public void CopyForm(string keyValue, string moduleId)
        {
            try
            {
                ModuleButtonEntity moduleButtonEntity = this.GetEntity(keyValue);
                moduleButtonEntity.ModuleId = moduleId;
                service.AddEntity(moduleButtonEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
