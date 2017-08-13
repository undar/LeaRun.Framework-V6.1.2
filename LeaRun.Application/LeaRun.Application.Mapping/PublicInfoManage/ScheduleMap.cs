using LeaRun.Application.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-04-25 10:45
    /// 描 述：日程管理
    /// </summary>
    public class ScheduleMap : EntityTypeConfiguration<ScheduleEntity>
    {
        public ScheduleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_Schedule");
            //主键
            this.HasKey(t => t.ScheduleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
