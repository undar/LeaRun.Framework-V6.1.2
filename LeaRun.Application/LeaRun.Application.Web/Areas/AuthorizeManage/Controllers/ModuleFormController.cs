using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单
    /// </summary>
    public class ModuleFormController : MvcControllerBase
    {
        private ModuleFormBLL moduleFormBll = new ModuleFormBLL();
        private ModuleFormInstanceBLL moduleFormInstanceBll = new ModuleFormInstanceBLL();

        #region 视图功能
        /// <summary>
        /// 功能管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormPreview()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 流程列表(分页)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = moduleFormBll.GetPageList(pagination, queryJson);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 功能实体 返回对象Json
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = moduleFormBll.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取系统表单实体通过模块Id
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEntityJsonByModuleId(string keyValue, string objectId)
        {
            var data = moduleFormBll.GetEntityByModuleId(keyValue);
            var dataInstance = moduleFormInstanceBll.GetEntityByObjectId(objectId);

            var jsonData = new {
                form = data,
                instance = dataInstance
            };

            return Content(jsonData.ToJson());
        }
        /// <summary>
        /// 判断模块是否已经注册了系统表单
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public ActionResult IsExistModuleId(string keyValue, string moduleId)
        {
            var data = moduleFormBll.IsExistModuleId(keyValue, moduleId);
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除表单模板
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            moduleFormBll.VirtualDelete(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleFormEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ModuleFormEntity moduleFormEntity)
        {
            moduleFormBll.SaveEntity(keyValue, moduleFormEntity);
            return Success("操作成功。");
        }
        #endregion

        #region 系统表单实例操作
        /// <summary>
        /// 保存系统表单实例（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleFormInstanceEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveFormInstance(string keyValue, ModuleFormInstanceEntity moduleFormInstanceEntity)
        {
            moduleFormInstanceBll.SaveEntity(keyValue, moduleFormInstanceEntity);
            return Success("操作成功。");
        }
        #endregion

    }
}
