using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Code;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.9 10:45
    /// 描 述：（权限认证+安全）拦截组件
    /// </summary>
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        private PermissionMode _customMode;
        /// <summary>默认构造</summary>
        /// <param name="Mode">认证模式</param>
        public HandlerAuthorizeAttribute(PermissionMode Mode)
        {
            _customMode = Mode;
        }
        /// <summary>
        /// 权限认证
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //是否超级管理员
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return;
            }
            //是否忽略
            if (_customMode == PermissionMode.Ignore)
            {
                return;
            }
            //IP过滤
            if (!this.FilterIP())
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>alert('很抱歉！您当前所在IP被系统拒绝访问！');top.Loading(false);</script>";
                filterContext.Result = Content;
                return;
            }
            //时段过滤
            if (!this.FilterTime())
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>alert('很抱歉！系统不允许您在当前时段访问！');top.Loading(false);</script>";
                filterContext.Result = Content;
                return;
            }
            //认证执行
            if (!this.ActionAuthorize(filterContext))
            {
                ContentResult Content = new ContentResult();
                Content.Content = "<script type='text/javascript'>alert('很抱歉！您的权限不足，访问被拒绝！');top.Loading(false);</script>";
                filterContext.Result = Content;
                return;
            }
        }
        /// <summary>
        /// IP过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterIP()
        {
            bool isFilterIP = Config.GetValue("FilterIP").ToBool();
            if (isFilterIP == true)
            {
                return new FilterIPBLL().FilterIP();
            }
            return true;
        }
        /// <summary>
        /// 时段过滤
        /// </summary>
        /// <returns></returns>
        private bool FilterTime()
        {
            bool isFilterIP = Config.GetValue("FilterTime").ToBool();
            if (isFilterIP == true)
            {
                return new FilterTimeBLL().FilterTime();
            }
            return true;
        }
        /// <summary>
        /// 执行权限认证
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private bool ActionAuthorize(ActionExecutingContext filterContext)
        {
            string currentUrl = HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            return new AuthorizeBLL().ActionAuthorize(SystemInfo.CurrentUserId, SystemInfo.CurrentModuleId, currentUrl);
        }
    }
}