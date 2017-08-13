using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Application.IService.WeChatManage;
using LeaRun.Application.Service.WeChatManage;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class WeChatAppBLL
    {
        private IWeChatAppService service = new WeChatAppService();

        #region 获取数据
        /// <summary>
        /// 应用列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WeChatAppEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 应用实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WeChatAppEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除应用
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
        /// 应用（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="weChatAppEntity">应用实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WeChatAppEntity weChatAppEntity)
        {
            try
            {
                service.SaveForm(keyValue, weChatAppEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
