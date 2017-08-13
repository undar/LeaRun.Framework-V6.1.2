using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典分类
    /// </summary>
    public class DataItemService : RepositoryFactory<DataItemEntity>, IDataItemService
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据分类编号获取实体对象
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <returns></returns>
        public DataItemEntity GetEntityByCode(string ItemCode)
        {
            var expression = LinqExtensions.True<DataItemEntity>();
            if (!string.IsNullOrEmpty(ItemCode))
            {
                expression = expression.And(t => t.ItemCode == ItemCode);
            }
            return this.BaseRepository().FindEntity(expression);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类编号不能重复
        /// </summary>
        /// <param name="itemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistItemCode(string itemCode, string keyValue)
        {
            var expression = LinqExtensions.True<DataItemEntity>();
            expression = expression.And(t => t.ItemCode == itemCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="itemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue)
        {
            var expression = LinqExtensions.True<DataItemEntity>();
            expression = expression.And(t => t.ItemName == itemName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataItemEntity dataItemEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataItemEntity.Modify(keyValue);
                this.BaseRepository().Update(dataItemEntity);
            }
            else
            {
                dataItemEntity.Create();
                this.BaseRepository().Insert(dataItemEntity);
            }
        }
        #endregion
    }
}
