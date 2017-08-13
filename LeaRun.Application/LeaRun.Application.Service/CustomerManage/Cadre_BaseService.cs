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
    /// 日 期：2017-07-26 21:15
    /// 描 述：干部基础
    /// </summary>
    public class Cadre_BaseService : RepositoryFactory, Cadre_BaseIService
    {
        #region 获取数据
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cadre_BaseEntity> GetList()
        {
            //查询条件           
            return this.BaseRepository().FindList<Cadre_BaseEntity>("select * from Cadre_Base");
        }
        #endregion
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Cadre_BaseEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Cadre_BaseEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["orgID"].IsEmpty())
            {
                string orgID = queryParam["orgID"].ToString();
                expression = expression.And(t => t.reportcom == orgID);
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        case "name":              //姓名
                            expression = expression.And(t => t.name.Contains(keyword));
                            break;
                        case "sex":              //性别
                            expression = expression.And(t => t.sex.Contains(keyword));
                            break;
                        case "currentduty":              //现任职务
                            expression = expression.And(t => t.currentduty.Contains(keyword));
                            break;
                        case "party":              //党派
                            expression = expression.And(t => t.party.Contains(keyword));
                            break;
                        default:
                            break;
                    }
                }
            }
                return this.BaseRepository().FindList(expression, pagination);
            }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Cadre_BaseEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<Cadre_BaseEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Cadre_FamilyEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<Cadre_FamilyEntity>("select * from Cadre_Family where PID='" + keyValue + "'");        }
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
                db.Delete<Cadre_BaseEntity>(keyValue);
                db.Delete<Cadre_FamilyEntity>(t => t.id.Equals(keyValue));
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
        public void SaveForm(string keyValue, Cadre_BaseEntity entity,List<Cadre_FamilyEntity> entryList)
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
                db.Delete<Cadre_FamilyEntity>(t => t.PID.Equals(keyValue));
                foreach (Cadre_FamilyEntity item in entryList)
                {
                    item.Create();
                    item.PID = entity.id;
                    db.Insert(item);
                }
            }
            else
            {
                //主表
                entity.Create();
                db.Insert(entity);
                //明细
                foreach (Cadre_FamilyEntity item in entryList)
                {
                    item.Create();
                    item.PID = entity.id;
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
       public   void Savephoto(string keyValue, Cadre_BaseEntity entity)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                   
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
    }
}
