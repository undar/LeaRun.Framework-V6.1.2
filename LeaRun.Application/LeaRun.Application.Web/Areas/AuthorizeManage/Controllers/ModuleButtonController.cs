using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonController : MvcControllerBase
    {
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();

        #region 视图功能
        /// <summary>
        /// 按钮表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 选择系统功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OptionModule()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 按钮列表 
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string moduleId)
        {
            var data = moduleButtonBLL.GetList(moduleId);
            return Content(data.ToJson());
        }
        public ActionResult GetButtonListJson(string moduleId)
        {
            var data = moduleButtonBLL.GetList(moduleId);
            var JsonData = new
            {
                rows = data,
            };
            return Content(JsonData.ToJson());
        }
        
        /// <summary>
        /// 按钮列表 
        /// </summary>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string moduleId)
        {
            var data = moduleButtonBLL.GetList(moduleId);
            if (data != null)
            {
                var TreeList = new List<TreeGridEntity>();
                foreach (ModuleButtonEntity item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.ModuleButtonId) == 0 ? false : true;
                    tree.id = item.ModuleButtonId;
                    tree.parentId = item.ParentId;
                    tree.expanded = true;
                    tree.hasChildren = hasChildren;
                    tree.entityJson = item.ToJson();
                    TreeList.Add(tree);
                }
                return Content(TreeList.TreeJson());
            }
            return null;
        }
        public ActionResult GetModuleButtonListJson(string moduleId)
        {
            var data = moduleButtonBLL.GetList(moduleId);
           
            if (data != null)
            {

                return Content(data.ToJson());
            }
            return null;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 按钮列表Json转换按钮树形Json 
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        public ActionResult ListToTreeJson(string moduleButtonJson)
        {
            var data = from items in moduleButtonJson.ToList<ModuleButtonEntity>() orderby items.SortCode select items;
            var treeList = new List<TreeEntity>();
            foreach (ModuleButtonEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ModuleButtonId) == 0 ? false : true;
                tree.id = item.ModuleButtonId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 按钮列表Json转换按钮树形Json 
        /// </summary>
        /// <param name="moduleButtonJson">按钮列表</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult ListToListTreeJson(string moduleButtonJson)
        {
            var data = from items in moduleButtonJson.ToList<ModuleButtonEntity>() orderby items.SortCode select items;
            var TreeList = new List<TreeGridEntity>();
            foreach (ModuleButtonEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ModuleButtonId) == 0 ? false : true;
                tree.id = item.ModuleButtonId;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }
        /// <summary>
        /// 复制按钮
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="moduleId">功能主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CopyForm(string keyValue, string moduleId)
        {
            moduleButtonBLL.CopyForm(keyValue, moduleId);
            return Content(new AjaxResult { type = ResultType.success, message = "复制成功。" }.ToJson());
        }
        #endregion
    }
}
