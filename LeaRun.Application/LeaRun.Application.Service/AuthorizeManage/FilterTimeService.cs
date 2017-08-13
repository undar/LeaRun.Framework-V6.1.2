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
    /// 描 述：过滤时段
    /// </summary>
    public class FilterTimeService : RepositoryFactory<FilterTimeEntity>, IFilterTimeService
    {
        #region 获取数据
        /// <summary>
        /// 过滤时段列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterTimeEntity> GetList(string objectId, string visitType)
        {
            var expression = LinqExtensions.True<FilterTimeEntity>();
            expression = expression.And(t => t.ObjectId == objectId);
            if (!string.IsNullOrEmpty(visitType))
            {
                int _visittype = visitType.ToInt();
                expression = expression.And(t => t.VisitType == _visittype);
            }
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 过滤时段列表
        /// </summary>
        /// <param name="objectId">对象Id,用逗号隔开</param>
        /// <returns></returns>
        public IEnumerable<FilterTimeEntity> GetList(string objectId)
        {
            var expression = LinqExtensions.True<FilterTimeEntity>();
            expression = expression.And(t => objectId.Contains(t.ObjectId));
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 过滤时段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterTimeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤时段
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterTimeEntity">过滤时段实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FilterTimeEntity filterTimeEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                filterTimeEntity.Modify(keyValue);
                this.BaseRepository().Update(filterTimeEntity);
            }
            else
            {
                filterTimeEntity.Create();
                filterTimeEntity.FilterTimeId = filterTimeEntity.ObjectId;
                this.BaseRepository().Insert(filterTimeEntity);
            }
        }
        #endregion
    }
}
