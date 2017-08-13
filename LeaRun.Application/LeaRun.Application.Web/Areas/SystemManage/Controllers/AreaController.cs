using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.12 16:40
    /// 描 述：区域管理
    /// </summary>
    public class AreaController : MvcControllerBase
    {
        private AreaBLL areaBLL = new AreaBLL();

        #region 视图功能
        /// <summary>
        /// 区域管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 区域表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 区域详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 区域列表 
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson(string value)
        {
            string parentId = value == null ? "0" : value;
            var filterdata = areaBLL.GetList(parentId).ToList();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (filterdata.Count > 0)
            {
                foreach (AreaEntity item in filterdata)
                {
                    bool hasChildren = areaBLL.GetList(item.AreaId).ToList().Count == 0 ? false : true;
                    sb.Append("{");
                    sb.Append("\"id\":\"" + item.AreaId + "\",");
                    sb.Append("\"text\":\"" + item.AreaName + "\",");
                    sb.Append("\"value\":\"" + item.AreaId + "\",");
                    sb.Append("\"isexpand\":false,");
                    sb.Append("\"complete\":false,");
                    sb.Append("\"hasChildren\":" + hasChildren.ToString().ToLower() + "");
                    sb.Append("},");
                }
                sb = sb.Remove(sb.Length - 1, 1);
            }
            sb.Append("]");
            return Content(sb.ToString());
        }
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="value">当前主键</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string value, string keyword)
        {
            string parentId = value == null ? "0" : value;
            var data = areaBLL.GetList(parentId, keyword).ToList();
            return Content(data.ToJson());
        }
        /// <summary>
        /// 区域列表（主要是绑定下拉框）
        /// </summary>
        /// <param name="parentId">节点Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetAreaListJson(string parentId)
        {
            var data = areaBLL.GetAreaList(parentId == null ? "0" : parentId);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 区域实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = areaBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除区域
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            areaBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, AreaEntity areaEntity)
        {
            areaBLL.SaveForm(keyValue, areaEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
