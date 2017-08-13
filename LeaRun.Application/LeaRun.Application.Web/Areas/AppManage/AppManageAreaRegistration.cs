namespace LeaRun.Application.Web.Areas.AppManage
{
    using System;
    using System.Web.Mvc;

    public class AppManageAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("AppManage_default", "AppManage/{controller}/{action}/{id}", new { action = "Index", id = UrlParameter.Optional });
        }

        public override string AreaName
        {
            get
            {
                return "AppManage";
            }
        }
    }
}

