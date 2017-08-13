using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using LeaRun.Util;

using LeaRun.Util.Extension;
using System;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-31 20:07
    /// 描 述：干部拟调整
    /// </summary>
    public class Cadre_PlanAdjustService : RepositoryFactory<Cadre_PlanAdjustEntity>, Cadre_PlanAdjustIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Cadre_PlanAdjustEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<Cadre_PlanAdjustEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "cadrename":              //干部姓名
                        expression = expression.And(t => t.cadrename.Contains(keyword));
                        break;
                    case "currentduty":              //现任职务
                        expression = expression.And(t => t.currentduty.Contains(keyword));
                        break;
                    case "currentrank":              //现任职级
                        expression = expression.And(t => t.currentrank.Contains(keyword));
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
        public Cadre_PlanAdjustEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Cadre_PlanAdjustEntity entity)
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
        /// <summary>
        /// 干部任免
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ToCustomer(string keyValue)
        {
            Cadre_PlanAdjustEntity chanceEntity = this.GetEntity(keyValue);
           // IEnumerable<TrailRecordEntity> trailRecordList = trailRecordService.GetList(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                chanceEntity.Modify(keyValue);
                chanceEntity.appointresultstatus = "1";
                db.Update<Cadre_PlanAdjustEntity>(chanceEntity);

                Cadre_BaseEntity Cadre_BaseEntity = new Cadre_BaseEntity();
                Cadre_BaseEntity.Modify(keyValue);
                Cadre_BaseEntity.id = chanceEntity.cadreid;
                Cadre_BaseEntity.currentduty = chanceEntity.aspiringduty;
                db.Update<Cadre_BaseEntity>(Cadre_BaseEntity);
 
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
