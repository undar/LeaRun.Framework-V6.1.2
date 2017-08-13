using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2016-04-01 14:33
    /// 描 述：开票信息
    /// </summary>
    public class InvoiceMap : EntityTypeConfiguration<InvoiceEntity>
    {
        public InvoiceMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Invoice");
            //主键
            this.HasKey(t => t.InvoiceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
