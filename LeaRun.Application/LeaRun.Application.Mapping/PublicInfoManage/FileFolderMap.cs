using LeaRun.Application.Entity.PublicInfoManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.15 10:56
    /// 描 述：文件夹
    /// </summary>
    public class FileFolderMap : EntityTypeConfiguration<FileFolderEntity>
    {
        public FileFolderMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_FileFolder");
            //主键
            this.HasKey(t => t.FolderId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
