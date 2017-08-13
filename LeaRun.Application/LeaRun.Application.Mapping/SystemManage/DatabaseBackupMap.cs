using LeaRun.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.25 11:02
    /// 描 述：数据库备份
    /// </summary>
    public class DataBaseBackupMap : EntityTypeConfiguration<DataBaseBackupEntity>
    {
        public DataBaseBackupMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_DatabaseBackup");
            //主键
            this.HasKey(t => t.DatabaseBackupId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
