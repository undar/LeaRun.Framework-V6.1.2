using LeaRun.Application.Busines.WeChatManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.WeChatManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.WeChatManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class AppController : MvcControllerBase
    {
        private WeChatAppBLL weChatAppBLL = new WeChatAppBLL();

        #region 视图功能
        /// <summary>
        /// 应用管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 应用表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 消息型应用表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormNews()
        {
            return View();
        }
        /// <summary>
        /// 消息型应用表单-添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormMenu()
        {
            return View();
        }
        /// <summary>
        /// 主页型应用表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FormHome()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 应用列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson()
        {
            var data = weChatAppBLL.GetList();
            return ToJsonResult(data);
        }
        /// <summary>
        /// 应用实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = weChatAppBLL.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除应用
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            var Entity = weChatAppBLL.GetEntity(keyValue);
            if (Entity !=null)
            {
                weChatAppBLL.RemoveForm(keyValue);
                DirFileHelper.DeleteFile(Entity.AppLogo);
            }
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存应用表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="weChatAppEntity">应用实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, WeChatAppEntity weChatAppEntity)
        {
            weChatAppBLL.SaveForm(keyValue, weChatAppEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="AppId">应用主键</param>
        /// <returns></returns>
        public ActionResult UploadFile(string AppId)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            string FileEextension = Path.GetExtension(files[0].FileName);
            string virtualPath = string.Format("/Resource/WeChatFile/{0}{1}", AppId, FileEextension);
            string fullFileName = Server.MapPath("~" + virtualPath);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);
            return Success(virtualPath);
        }
        #endregion

        #region 处理数据
        /// <summary>
        /// 自定义菜单Json转换树形Json 
        /// </summary>
        /// <param name="menuJson">菜单Json</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult MenuListToTreeJson(string menuJson)
        {
            var data = from items in menuJson.ToList<WeChatAppMenuEntity>() orderby items.SortCode select items;
            var treeList = new List<TreeEntity>();
            foreach (WeChatAppMenuEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.MenuId) == 0 ? false : true;
                tree.id = item.MenuId;
                tree.text = item.MenuName;
                tree.value = item.MenuId;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 自定义菜单Json转换树形Json 
        /// </summary>
        /// <param name="menuJson">菜单Json</param>
        /// <returns>返回树形列表Json</returns>
        [HttpPost]
        public ActionResult MenuListToListTreeJson(string menuJson)
        {
            var data = from items in menuJson.ToList<WeChatAppMenuEntity>() orderby items.SortCode select items;
            var TreeList = new List<TreeGridEntity>();
            foreach (WeChatAppMenuEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.MenuId) == 0 ? false : true;
                tree.id = item.MenuId;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }
        #endregion
    }
}
