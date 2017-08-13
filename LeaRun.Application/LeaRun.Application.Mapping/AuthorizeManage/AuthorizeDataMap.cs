using LeaRun.Application.Entity.AuthorizeManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.27
    /// 描 述：授权数据范围
    /// </summary>
    public class AuthorizeDataMap : EntityTypeConfiguration<AuthorizeDataEntity>
    {
        public AuthorizeDataMap()
        {
            #region 表、主键
            //表
            this.ToTable("Base_AuthorizeData");
            //主键
            this.HasKey(t => t.AuthorizeDataId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
