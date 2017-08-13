using LeaRun.Application.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.PublicInfoManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.10.18
    /// 描 述：电子签章
    /// </summary>
    public class SignetController : MvcControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

