using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-26 21:15
    /// 描 述：干部基础
    /// </summary>
    public class Cadre_FamilyMap : EntityTypeConfiguration<Cadre_FamilyEntity>
    {
        public Cadre_FamilyMap()
        {
            #region 表、主键
            //表
            this.ToTable("Cadre_Family");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
