using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.7 16:40
    /// 描 述：新闻中心
    /// </summary>
    public interface INewsService
    {
        #region 获取数据
        /// <summary>
        /// 新闻列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<NewsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 新闻实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        NewsEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存新闻表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="newsEntity">新闻实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, NewsEntity newsEntity);
        #endregion
    }
}
