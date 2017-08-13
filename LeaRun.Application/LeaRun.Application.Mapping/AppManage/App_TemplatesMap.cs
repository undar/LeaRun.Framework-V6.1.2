namespace LeaRun.Application.Mapping.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using System;
    using System.Data.Entity.ModelConfiguration;

    public class App_TemplatesMap : EntityTypeConfiguration<App_TemplatesEntity>
    {
        public App_TemplatesMap()
        {
            base.ToTable("App_Templates");
            base.HasKey<string>(t => t.F_Id);
        }
    }
}

