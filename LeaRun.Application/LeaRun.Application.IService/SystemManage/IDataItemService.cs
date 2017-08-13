using LeaRun.Application.Entity.SystemManage;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典分类
    /// </summary>
    public interface IDataItemService
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataItemEntity> GetList();
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DataItemEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据分类编号获取实体对象
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <returns></returns>
        DataItemEntity GetEntityByCode(string ItemCode);
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类编号不能重复
        /// </summary>
        /// <param name="itemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistItemCode(string itemCode, string keyValue);
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="itemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistItemName(string itemName, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, DataItemEntity dataItemEntity);
        #endregion
    }
}
