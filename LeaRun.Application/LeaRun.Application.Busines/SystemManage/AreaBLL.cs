using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.12 16:40
    /// 描 述：区域管理
    /// </summary>
    public class AreaBLL
    {
        private IAreaService service = new AreaService();
        /// <summary>
        /// 缓存key
        /// </summary>
        private string cacheKey = "areaCache";

        #region 获取数据
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<AreaEntity>>(cacheKey);
            if (cacheList == null)
            {
                var data = service.GetList();
                CacheFactory.Cache().WriteCache(data, cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList(string parentId, string keyword = "")
        {
            return service.GetList(parentId, keyword);
        }
        /// <summary>
        /// 区域列表（主要是给绑定数据源提供的）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetAreaList(string parentId)
        {
            return this.GetList().Where(t => t.EnabledMark == 1 && t.ParentId == parentId);
        }
        /// <summary>
        /// 区域实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AreaEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="{">区域实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AreaEntity areaEntity)
        {
            try
            {
                service.SaveForm(keyValue, areaEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
