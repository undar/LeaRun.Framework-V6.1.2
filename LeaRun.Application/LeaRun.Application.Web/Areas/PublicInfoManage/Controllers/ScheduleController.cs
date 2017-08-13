using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Util.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.PublicInfoManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.4.21 16:01
    /// 描 述：日程管理
    /// </summary>
    public class ScheduleController : MvcControllerBase
    {
        private ScheduleBLL schedulebll = new ScheduleBLL();

        #region 视图功能
        /// <summary>
        /// 日程管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 添加日程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        public ActionResult GetList()
        {
            List<Hashtable> data = new List<Hashtable>();
            foreach (ScheduleEntity entity in schedulebll.GetList("").ToList())
            {
                Hashtable ht = new Hashtable();
                ht["id"] = entity.ScheduleId;
                ht["title"] = entity.ScheduleContent;
                ht["end"] = (entity.EndDate.ToDate().ToString("yyyy-MM-dd") + " " + entity.EndTime.Substring(0, 2) + ":" + entity.EndTime.Substring(2, 2)).ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                ht["start"] = (entity.StartDate.ToDate().ToString("yyyy-MM-dd") + " " + entity.StartTime.Substring(0, 2) + ":" + entity.StartTime.Substring(2, 2)).ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                ht["allDay"] = false;
                data.Add(ht);
            }
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = schedulebll.GetEntity(keyValue);
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
            schedulebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ScheduleEntity entity)
        {
            schedulebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
