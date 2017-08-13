using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using LeaRun.Util;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-25 21:08
    /// 描 述：域外人才
    /// </summary>
    public class Talent_OutTeamService : RepositoryFactory, Talent_OutTeamIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Talent_OutTeamEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Talent_OutTeamEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "name":              //姓名
                        expression = expression.And(t => t.name.Contains(keyword));
                        break;
                    case "domicile":              //户籍所在地
                        expression = expression.And(t => t.domicile.Contains(keyword));
                        break;
                    case "politicsstatus":              //政治面貌
                        expression = expression.And(t => t.politicsstatus.Contains(keyword));
                        break;
                    case "highestedu":              //最高学历
                        expression = expression.And(t => t.highestedu.Contains(keyword));
                        break;
                    case "talenttype":              //人才类型
                        expression = expression.And(t => t.talenttype.Contains(keyword));
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Talent_OutTeamEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Talent_OutTeamEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Talent_outFamilyEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Talent_outFamilyEntity>("select * from Talent_outFamily where talentid='" + keyValue + "'");        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<Talent_OutTeamEntity>(keyValue);
                db.Delete<Talent_outFamilyEntity>(t => t.id.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Talent_OutTeamEntity entity,List<Talent_outFamilyEntity> entryList)
        {
        IRepository db = this.BaseRepository().BeginTrans();
        try
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //主表
                entity.Modify(keyValue);
                db.Update(entity);
                //明细
                db.Delete<Talent_outFamilyEntity>(t => t.talentid.Equals(keyValue));
                foreach (Talent_outFamilyEntity item in entryList)
                {
                    item.Create();
                    item.talentid = entity.id;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (Talent_outFamilyEntity item in entryList)
                {
                    item.Create();
                    item.talentid = entity.id;
                    db.Insert(item);
                }
            }
            db.Commit();
        }
        catch (Exception)
        {
            db.Rollback();
            throw;
        }
        }
        #endregion
    }
}
