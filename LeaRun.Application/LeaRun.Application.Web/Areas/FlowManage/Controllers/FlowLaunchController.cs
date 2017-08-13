using LeaRun.Application.Busines.FlowManage;
using LeaRun.Application.Entity.FlowManage;
using System;
using System.Collections.Generic;
using LeaRun.Util;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LeaRun.Application.Code;

namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 14:27
    /// 描 述：流程发起
    /// </summary>
    public class FlowLaunchController : MvcControllerBase
    {
        private WFRuntimeBLL wfProcessBll = new WFRuntimeBLL();
        #region 视图功能
        //
        // GET: /FlowManage/FlowLaunch/
        /// <summary>
        /// 管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewIndex()
        {
            return View();
        }
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FlowProcessNewForm()
        {
            return View();
        }
        public ActionResult GetFlowJson(string keyValue)
        {
            FormModuleBLL formbll = new FormModuleBLL();
            WFSchemeInfoBLL bll = new WFSchemeInfoBLL();
            var entity = bll.GetEntity(keyValue);
            var data = new {
                formEntity=formbll.GetEntity(entity.FormList),
                schemeInfo=entity
            };
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="wfSchemeInfoId">流程模板信息Id</param>
        /// <param name="frmData">表单数据</param>
        /// <param name="type">0发起，3草稿</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        //[ValidateInput(false)]
        public ActionResult CreateProcess(string wfSchemeInfoId, string wfProcessInstanceJson, string frmData)
        {
            WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceJson.ToObject<WFProcessInstanceEntity>();
            wfProcessBll.CreateProcess(wfSchemeInfoId,wfProcessInstanceEntity, frmData);
            string text = "创建成功";
            if (wfProcessInstanceEntity.EnabledMark != 1)
            {
                text = "草稿保存成功";
            }
            return Success(text);
        } 
        #endregion
    }
}
