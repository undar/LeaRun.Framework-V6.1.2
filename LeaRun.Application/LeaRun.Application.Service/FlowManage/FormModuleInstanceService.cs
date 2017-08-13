using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    public class FieldEntity
    {
        public string type { get; set; }
        public string value { get; set; }
        public string field { get; set; }
        public string infoType { get; set; }
        public string realValue { get; set; }
    }
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-27 15:28
    /// 描 述：表单实例表
    /// </summary>
    public class FormModuleInstanceService : RepositoryFactory<FormModuleInstanceEntity>, FormModuleInstanceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public string GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<FormModuleInstanceEntity>();
            if (!queryJson.IsEmpty())
            {
                expression = expression.And(t => t.ObjectId.Equals(queryJson));
            }
            IEnumerable<FormModuleInstanceEntity> list = this.BaseRepository().FindList(expression, pagination);
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //foreach (var item in list)
            //{
            //    dic.Add("leaCustmerFormId", item.Id);
            //    List<FieldEntity> fieldlist = item.FrmInstanceJson.ToList<FieldEntity>();
                
            //    foreach (var item1 in fieldlist)
            //    {
            //        dic.Add(item1.field,item1.value);
            //    }
            //}
            //return dic;
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            var i = 0;
            foreach (var item in list)
            {
               
                sb.Append("{");
                sb.Append("\"leaCustmerFormId\":\""+item.Id+"\",");
               
                List<FieldEntity> fieldlist = item.FrmInstanceJson.ToList<FieldEntity>();
                var a = 0;
                foreach (var item1 in fieldlist)
                {
                    
                    sb.Append("\"" + item1.field + "\":\"" + item1.value + "\"");
                    if (a<fieldlist.Count-1)
                    {
                        sb.Append(",");
                    }
                    a++;
                }
                sb.Append("}");
                if (i<list.ToArray().Length-1)
                {
                    sb.Append(",");
                }
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<FormModuleInstanceEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleInstanceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FormModuleInstanceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
