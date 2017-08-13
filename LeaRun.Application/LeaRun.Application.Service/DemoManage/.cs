using LeaRun.Application.Entity.DemoManage;
using LeaRun.Application.IService.DemoManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.DemoManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-05-04 13:25
    /// 描 述：学生表
    /// </summary>
    public class studentService : RepositoryFactory<studentEntity>, studentIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<studentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository("BaseDb","SqlServer").FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<studentEntity> GetList(string queryJson)
        {
            return this.BaseRepository("BaseDb","SqlServer").IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public studentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository("BaseDb","SqlServer").FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository("BaseDb","SqlServer").Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, studentEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository("BaseDb","SqlServer").Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository("BaseDb","SqlServer").Insert(entity);
            }
        }
        #endregion
    }
}
