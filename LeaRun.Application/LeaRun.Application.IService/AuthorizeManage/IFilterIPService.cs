using LeaRun.Application.Entity.AuthorizeManage;
using System.Collections.Generic;

namespace LeaRun.Application.IService.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤IP
    /// </summary>
    public interface IFilterIPService
    {
        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        IEnumerable<FilterIPEntity> GetList(string objectId, string visitType);
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id,用逗号分隔</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        IEnumerable<FilterIPEntity> GetAllList(string objectId, int visitType);
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FilterIPEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, FilterIPEntity filterIPEntity);
        #endregion
    }
}
