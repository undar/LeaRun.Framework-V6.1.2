using LeaRun.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-31 20:07
    /// 描 述：干部拟调整
    /// </summary>
    public class Cadre_PlanAdjustMap : EntityTypeConfiguration<Cadre_PlanAdjustEntity>
    {
        public Cadre_PlanAdjustMap()
        {
            #region 表、主键
            //表
            this.ToTable("Cadre_PlanAdjust");
            //主键
            this.HasKey(t => t.id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
