using LeaRun.Application.Entity.FlowManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleMap : EntityTypeConfiguration<FormModuleEntity>
    {
        public FormModuleMap()
        {
            #region 表、主键
            //表
            this.ToTable("Form_Module");
            //主键
            this.HasKey(t => t.FrmId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
