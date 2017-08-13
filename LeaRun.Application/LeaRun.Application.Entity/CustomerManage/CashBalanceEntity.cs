using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-04-28 16:48
    /// 描 述：现金余额
    /// </summary>
    public class CashBalanceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 现金余额主键
        /// </summary>
        /// <returns></returns>
        public string CashBalanceId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        public string ObjectId { get; set; }
        /// <summary>
        /// ExecutionDate
        /// </summary>
        /// <returns></returns>
        public DateTime? ExecutionDate { get; set; }
        /// <summary>
        /// CashAccount
        /// </summary>
        /// <returns></returns>
        public string CashAccount { get; set; }
        /// <summary>
        /// Receivable
        /// </summary>
        /// <returns></returns>
        public decimal? Receivable { get; set; }
        /// <summary>
        /// Expenses
        /// </summary>
        /// <returns></returns>
        public decimal? Expenses { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        /// <returns></returns>
        public decimal? Balance { get; set; }
        /// <summary>
        /// Abstract
        /// </summary>
        /// <returns></returns>
        public string Abstract { get; set; }
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
            this.CashBalanceId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CashBalanceId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}