using LeaRun.Application.Entity;
using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Application.IService;
using LeaRun.Application.IService.WeChatManage;
using LeaRun.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class WeChatAppService : RepositoryFactory<WeChatAppEntity>, IWeChatAppService
    {
        #region 获取数据
        /// <summary>
        /// 应用列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatAppEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 应用实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatAppEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 应用（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="weChatAppEntity">应用实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WeChatAppEntity weChatAppEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                weChatAppEntity.Modify(keyValue);
                this.BaseRepository().Update(weChatAppEntity);
            }
            else
            {
                weChatAppEntity.Create();
                this.BaseRepository().Insert(weChatAppEntity);
            }
        }
        #endregion
    }
}
