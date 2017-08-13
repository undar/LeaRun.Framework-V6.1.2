using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单管理
    /// </summary>
    public class OrderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string CustomerName { get; set; }
        /// <summary>
        /// 销售人员Id
        /// </summary>
        /// <returns></returns>
        public string SellerId { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        public string SellerName { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        public string OrderCode { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <returns></returns>
        public decimal? DiscountSum { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        public decimal? ReceivedAmount { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        public DateTime? PaymentDate { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        public string PaymentMode { get; set; }
        /// <summary>
        /// 收款状态（1-未收款2-部分收款3-全部收款）
        /// </summary>
        /// <returns></returns>
        public int? PaymentState { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        /// <returns></returns>
        public decimal? SaleCost { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        /// <returns></returns>
        public string AbstractInfo { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        /// <returns></returns>
        public string ContractCode { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        /// <returns></returns>
        public string ContractFile { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ReceivedAmount = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}