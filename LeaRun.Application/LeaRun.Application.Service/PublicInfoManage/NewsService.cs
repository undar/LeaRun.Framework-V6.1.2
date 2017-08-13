using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.Service.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public class NewsService : RepositoryFactory<NewsEntity>, INewsService
    {
        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<NewsEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["FullHead"].IsEmpty())
            {
                string FullHead = queryParam["FullHead"].ToString();
                expression = expression.And(t => t.FullHead.Contains(FullHead));
            }
            if (!queryParam["Category"].IsEmpty())
            {
                string Category = queryParam["Category"].ToString();
                expression = expression.And(t => t.Category == Category);
            }
            expression = expression.And(t => t.TypeId == 1);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 新闻实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public NewsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存新闻表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, NewsEntity newsEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                newsEntity.Modify(keyValue);
                newsEntity.TypeId = 1;
                this.BaseRepository().Update(newsEntity);
            }
            else
            {
                newsEntity.Create();
                newsEntity.TypeId = 1;
                this.BaseRepository().Insert(newsEntity);
            }
        }
        #endregion
    }
}
