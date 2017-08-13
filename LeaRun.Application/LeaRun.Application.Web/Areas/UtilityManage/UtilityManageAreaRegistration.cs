using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.UtilityManage
{
    public class UtilityManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "UtilityManage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "UtilityManage_default",
                "UtilityManage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
