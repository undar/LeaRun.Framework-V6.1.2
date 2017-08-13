using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单明细
    /// </summary>
    public class OrderEntryEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 订单明细主键
        /// </summary>
        /// <returns></returns>
        public string OrderEntryId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        public string OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        /// <returns></returns>
        public string ProductId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        /// <returns></returns>
        public string ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        public string ProductName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        public string UnitId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        public decimal? Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        public decimal? Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        public decimal? Amount { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        /// <returns></returns>
        public decimal? Taxprice { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        /// <returns></returns>
        public decimal? TaxRate { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        /// <returns></returns>
        public decimal? Tax { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        /// <returns></returns>
        public decimal? TaxAmount { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.OrderEntryId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.OrderEntryId = keyValue;
        }
        #endregion
    }
}