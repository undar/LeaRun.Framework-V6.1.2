using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.BaseManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤IP
    /// </summary>
    public class FilterIPService : RepositoryFactory<FilterIPEntity>, IFilterIPService
    {
        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterIPEntity> GetList(string objectId, string visitType)
        {
            var expression = LinqExtensions.True<FilterIPEntity>();
            expression = expression.And(t => t.ObjectId == objectId);
            if (!string.IsNullOrEmpty(visitType))
            {
                int _visittype = visitType.ToInt();
                expression = expression.And(t => t.VisitType == _visittype);
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id，用逗号分隔</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterIPEntity> GetAllList(string objectId, int visitType)
        {
            var expression = LinqExtensions.True<FilterIPEntity>();
            expression = expression.And(t => objectId.Contains(t.ObjectId));
            expression = expression.And(t => t.VisitType == visitType);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterIPEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterIPEntity.Modify(keyValue);
                this.BaseRepository().Update(filterIPEntity);
            }
            else
            {
                filterIPEntity.Create();
                this.BaseRepository().Insert(filterIPEntity);
            }
        }
        #endregion
    }
}
