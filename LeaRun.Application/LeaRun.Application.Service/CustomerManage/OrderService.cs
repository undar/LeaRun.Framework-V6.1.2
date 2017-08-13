using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Application.IService.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using LeaRun.Util;
using System.Collections.Generic;
using System.Linq;
using System;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Code;

namespace LeaRun.Application.Service.CustomerManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderService : RepositoryFactory<OrderEntity>, IOrderService
    {
        private ICodeRuleService coderuleService = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<OrderEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.OrderDate >= startTime && t.OrderDate <= endTime);
            }
            //单据编号
            if (!queryParam["OrderCode"].IsEmpty())
            {
                string OrderCode = queryParam["OrderCode"].ToString();
                expression = expression.And(t => t.OrderCode.Contains(OrderCode));
            }
            //客户名称
            if (!queryParam["CustomerName"].IsEmpty())
            {
                string CustomerName = queryParam["CustomerName"].ToString();
                expression = expression.And(t => t.CustomerName.Contains(CustomerName));
            }
            //销售人员
            if (!queryParam["SellerName"].IsEmpty())
            {
                string SellerName = queryParam["SellerName"].ToString();
                expression = expression.And(t => t.SellerName.Contains(SellerName));
            }
            //收款状态
            if (!queryParam["PaymentState"].IsEmpty())
            {
                int PaymentState = queryParam["PaymentState"].ToInt();
                expression = expression.And(t => t.PaymentState == PaymentState);
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取前单、后单 数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="type">类型（1-前单；2-后单）</param>
        /// <returns>返回实体</returns>
        public OrderEntity GetPrevOrNextEntity(string keyValue, int type)
        {
            OrderEntity entity = this.GetEntity(keyValue);
            if (type == 1)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate >entity.CreateDate).OrderBy(t => t.CreateDate).FirstOrDefault();
            }
            else if (type == 2)
            {
                entity = this.BaseRepository().IQueryable().Where(t => t.CreateDate < entity.CreateDate).OrderByDescending(t => t.CreateDate).FirstOrDefault();
            }
            return entity;
        }
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
                db.Delete<OrderEntity>(keyValue);
                db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
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
        /// <param name="orderEntity">实体对象</param>
        /// <param name="orderEntryList">明细实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrderEntity orderEntity, List<OrderEntryEntity> orderEntryList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    orderEntity.Modify(keyValue);
                    db.Update(orderEntity);
                    //明细
                    db.Delete<OrderEntryEntity>(t => t.OrderId.Equals(keyValue));
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
                    }
                }
                else
                {
                    //主表
                    orderEntity.Create();
                    db.Insert(orderEntity);
                    coderuleService.UseRuleSeed(orderEntity.CreateUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString(), db);//占用单据号
                    //明细
                    foreach (OrderEntryEntity orderEntryEntity in orderEntryList)
                    {
                        orderEntryEntity.Create();
                        orderEntryEntity.OrderId = orderEntity.OrderId;
                        db.Insert(orderEntryEntity);
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