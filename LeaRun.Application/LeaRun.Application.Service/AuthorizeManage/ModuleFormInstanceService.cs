using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Service.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单实例
    /// </summary>
    public class ModuleFormInstanceService : RepositoryFactory,IModuleFormInstanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取一个实体类
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ModuleFormInstanceEntity GetEntityByObjectId(string objectId)
        {
            try
            {
                var expression = LinqExtensions.True<ModuleFormInstanceEntity>();
                expression = expression.And(t => t.ObjectId.Equals(objectId));
                return this.BaseRepository().FindEntity<ModuleFormInstanceEntity>(expression);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, ModuleFormInstanceEntity entity)
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
        #endregion
    }
}
