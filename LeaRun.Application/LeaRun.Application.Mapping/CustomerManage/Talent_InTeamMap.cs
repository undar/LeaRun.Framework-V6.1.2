using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2017-07-25 00:22
    /// 描 述：域内人才
    /// </summary>
    public class Talent_InTeamMap : EntityTypeConfiguration<Talent_InTeamEntity>
    {
        public Talent_InTeamMap()
        {
            #region 表、主键
            //表
            this.ToTable("Talent_InTeam");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
