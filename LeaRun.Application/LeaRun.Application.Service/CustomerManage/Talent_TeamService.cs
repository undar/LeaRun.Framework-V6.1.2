using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2017-07-25 00:10
    /// 描 述：人才团队
    /// </summary>
    public class Talent_TeamService : RepositoryFactory<Talent_TeamEntity>, Talent_TeamIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Talent_TeamEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Talent_TeamEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "TeamName":              //团队名称
                        expression = expression.And(t => t.TeamName.Contains(keyword));
                        break;
                    case "name":              //姓名
                        expression = expression.And(t => t.name.Contains(keyword));
                        break;
                    case "domicile":              //户籍所在地
                        expression = expression.And(t => t.domicile.Contains(keyword));
                        break;
                    case "highestedu":              //最高学历
                        expression = expression.And(t => t.highestedu.Contains(keyword));
                        break;
                    case "talenttype":              //人才类型
                        expression = expression.And(t => t.talenttype.Contains(keyword));
                        break;
                    case "politicsstatus":              //政治面貌
                        expression = expression.And(t => t.politicsstatus.Contains(keyword));
                        break;
                    case "submissioncom":              //报送单位
                        expression = expression.And(t => t.submissioncom.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression,pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Talent_TeamEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Talent_TeamEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "TeamName":              //团队名称
                        expression = expression.And(t => t.TeamName.Contains(keyword));
                        break;
                    case "name":              //姓名
                        expression = expression.And(t => t.name.Contains(keyword));
                        break;
                    case "domicile":              //户籍所在地
                        expression = expression.And(t => t.domicile.Contains(keyword));
                        break;
                    case "highestedu":              //最高学历
                        expression = expression.And(t => t.highestedu.Contains(keyword));
                        break;
                    case "talenttype":              //人才类型
                        expression = expression.And(t => t.talenttype.Contains(keyword));
                        break;
                    case "politicsstatus":              //政治面貌
                        expression = expression.And(t => t.politicsstatus.Contains(keyword));
                        break;
                    case "submissioncom":              //报送单位
                        expression = expression.And(t => t.submissioncom.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Talent_TeamEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Talent_TeamEntity entity)
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
