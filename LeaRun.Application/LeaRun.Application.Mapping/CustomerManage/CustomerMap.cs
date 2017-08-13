using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerMap : EntityTypeConfiguration<CustomerEntity>
    {
        public CustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Customer");
            //主键
            this.HasKey(t => t.CustomerId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
