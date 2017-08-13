using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.DemoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-12-06 17:29
    /// 描 述：OfficeRk
    /// </summary>
    public class OfficeRkEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [Column("ORDERID")]
        public string OrderId { get; set; }
        /// <summary>
        /// 供应商主键
        /// </summary>
        /// <returns></returns>
        [Column("GYSID")]
        public string GysId { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [Column("GYSNAME")]
        public string GysName { get; set; }
        /// <summary>
        /// 销售人员Id
        /// </summary>
        /// <returns></returns>
        [Column("SELLERID")]
        public string SellerId { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        /// <returns></returns>
        [Column("SELLERNAME")]
        public string SellerName { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        [Column("RKDATE")]
        public DateTime? RkDate { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        [Column("RKCODE")]
        public string RkCode { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        [Column("ACCOUNTS")]
        public decimal? Accounts { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        /// <returns></returns>
        [Column("ABSTRACTINFO")]
        public string AbstractInfo { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
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