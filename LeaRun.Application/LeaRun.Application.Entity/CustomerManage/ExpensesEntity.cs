using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-04-20 11:23
    /// 描 述：费用支出
    /// </summary>
    public class ExpensesEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 支出主键
        /// </summary>
        /// <returns></returns>
        public string ExpensesId { get; set; }
        /// <summary>
        /// 支出日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ExpensesDate { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        /// <returns></returns>
        public decimal? ExpensesPrice { get; set; }
        /// <summary>
        /// 支出账户
        /// </summary>
        /// <returns></returns>
        public string ExpensesAccount { get; set; }
        /// <summary>
        /// 支出种类
        /// </summary>
        /// <returns></returns>
        public string ExpensesType { get; set; }
        /// <summary>
        /// 支出对象（1-公司支付；2-个人垫付）
        /// </summary>
        /// <returns></returns>
        public int? ExpensesObject { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        /// <returns></returns>
        public string Managers { get; set; }
        /// <summary>
        /// 支出摘要
        /// </summary>
        /// <returns></returns>
        public string ExpensesAbstract { get; set; }
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
            this.ExpensesId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ExpensesObject = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ExpensesId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}