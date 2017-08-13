using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤时段
    /// </summary>
    public class FilterTimeController : MvcControllerBase
    {
        private FilterTimeBLL filterTimeBLL = new FilterTimeBLL();

        #region 视图功能
        /// <summary>
        /// 过滤时段管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 过滤时段表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 过滤时段列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string objectId, string visitType)
        {
            var data = filterTimeBLL.GetList(objectId, visitType);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 过滤时段实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = filterTimeBLL.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤时段
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            filterTimeBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="databaseLinkEntity">过滤时段实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(FilterTimeEntity filterTimeEntity)
        {
            filterTimeBLL.SaveForm(filterTimeEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
