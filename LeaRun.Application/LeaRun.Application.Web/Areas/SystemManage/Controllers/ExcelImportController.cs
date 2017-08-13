using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-03-31 15:17
    /// 描 述：Excel导入模板表
    /// </summary>
    public class ExcelImportController : MvcControllerBase
    {
        private ExcelImportBLL excelimportbll = new ExcelImportBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SetFieldForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = excelimportbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = excelimportbll.GetEntity(keyValue);
            var childData = excelimportbll.GetDetails(keyValue);
            var jsonData = new
            {
                templateInfo = data,
                filedsInfo = childData
            };
            return ToJsonResult(jsonData);
        }
        public ActionResult GetFormJsonByModuleId()
        {
            string moduleId = WebHelper.GetCookie("currentmoduleId");
            ExcelImportEntity model = excelimportbll.GetEntityByModuleId(moduleId);
            var data = excelimportbll.GetEntity(model.F_Id);
            var childData = excelimportbll.GetDetails(model.F_Id);
            var jsonData = new
            {
                templateInfo = data,
                filedsInfo = childData
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取子表详细信息 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetDetailsJson(string keyValue)
        {
            var data = excelimportbll.GetDetails(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            excelimportbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateState(string keyValue, int F_EnabledMark)
        {
            excelimportbll.UpdateState(keyValue, F_EnabledMark);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="templateInfo">实体对象</param>
        /// <param name="filedsInfo">子表对象集</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string templateInfo, string filedsInfo)
        {
            var entity = templateInfo.ToObject<ExcelImportEntity>();
            List<ExcelImportFiledEntity> childEntitys = filedsInfo.ToList<ExcelImportFiledEntity>();
            excelimportbll.SaveForm(keyValue, entity, childEntitys);
            return Success("操作成功。");
        }
        #endregion
    }
}
