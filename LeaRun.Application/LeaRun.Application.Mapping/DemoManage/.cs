using LeaRun.Application.Entity.DemoManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.DemoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-05-04 13:25
    /// 描 述：学生表
    /// </summary>
    public class studentMap : EntityTypeConfiguration<studentEntity>
    {
        public studentMap()
        {
            #region 表、主键
            //表
            this.ToTable("student");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
