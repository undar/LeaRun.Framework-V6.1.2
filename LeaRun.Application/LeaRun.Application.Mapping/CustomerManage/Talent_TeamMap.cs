using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2017-07-25 00:10
    /// 描 述：人才团队
    /// </summary>
    public class Talent_TeamMap : EntityTypeConfiguration<Talent_TeamEntity>
    {
        public Talent_TeamMap()
        {
            #region 表、主键
            //表
            this.ToTable("Talent_Team");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
