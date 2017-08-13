using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-21 16:10
    /// 描 述：商机跟进记录
    /// </summary>
    public class TrailRecordMap : EntityTypeConfiguration<TrailRecordEntity>
    {
        public TrailRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_TrailRecord");
            //主键
            this.HasKey(t => t.TrailId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}