using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.AuthorizeManage.Controllers
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.8 22:35
    /// 描 述：岗位权限
    /// </summary>
    public class PermissionPostController : MvcControllerBase
    {
        private OrganizeBLL organizeBLL = new OrganizeBLL();
        private DepartmentBLL departmentBLL = new DepartmentBLL();
        private DepartmentCache departmentCache = new DepartmentCache();
        private RoleBLL roleBLL = new RoleBLL();
        private UserBLL userBLL = new UserBLL();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();
        private PermissionBLL permissionBLL = new PermissionBLL();
        private AuthorizeBLL authorizeBll = new AuthorizeBLL();

        #region 视图功能
        /// <summary>
        /// 岗位权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotAuthorize()
        {
            return View();
        }
        /// <summary>
        /// 岗位成员
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult AllotMember()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 部门列表 
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetDepartmentTreeJson(string postId)
        {
            var roleEntity = roleBLL.GetEntity(postId);
            var organizeEntity = organizeBLL.GetEntity(roleEntity.OrganizeId);
            var data = departmentCache.GetList(roleEntity.OrganizeId);

            var treeList = new List<TreeEntity>();
            TreeEntity tree = new TreeEntity();
            tree.id = organizeEntity.OrganizeId;
            tree.text = organizeEntity.FullName;
            tree.value = organizeEntity.OrganizeId;
            tree.isexpand = true;
            tree.complete = true;
            tree.hasChildren = true;
            tree.parentId = "0";
            treeList.Add(tree);
            foreach (DepartmentEntity item in data)
            {
                tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.DepartmentId) == 0 ? false : true;
                tree.id = item.DepartmentId;
                tree.text = item.FullName;
                tree.value = item.DepartmentId;
                if (item.ParentId == "0")
                {
                    tree.parentId = roleEntity.OrganizeId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserListJson(string postId)
        {
            var existMember = permissionBLL.GetMemberList(postId);
            var userdata = userBLL.GetTable();
            userdata.Columns.Add("isdefault", Type.GetType("System.Int32"));
            userdata.Columns.Add("ischeck", Type.GetType("System.Int32"));
            foreach (DataRow item in userdata.Rows)
            {
                string UserId = item["userid"].ToString();
                int ischeck = existMember.Count(t => t.UserId == UserId);
                item["ischeck"] = ischeck;
                if (ischeck > 0)
                {
                    item["isdefault"] = existMember.First(t => t.UserId == UserId).IsDefault;
                }
                else
                {
                    item["isdefault"] = 0;
                }
            }
            userdata = DataHelper.DataFilter(userdata, "", "ischeck desc");
            return Content(userdata.ToJson());
        }
        /// <summary>
        /// 系统功能列表
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleTreeJson(string postId)
        {
            var existModule = permissionBLL.GetModuleList(postId);
            //var data = moduleBLL.GetList();
            var data = authorizeBll.GetModuleList(SystemInfo.CurrentUserId);
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ModuleId) == 0 ? false : true;
                tree.id = item.ModuleId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                tree.title = "";
                tree.checkstate = existModule.Count(t => t.ItemId == item.ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.parentId = item.ParentId;
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 系统按钮列表
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleButtonTreeJson(string postId)
        {
            var existModuleButton = permissionBLL.GetModuleButtonList(postId);
            var moduleData = moduleBLL.GetList();
            var moduleButtonData = moduleButtonBLL.GetList();
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in moduleData)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.ModuleId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                tree.checkstate = existModuleButton.Count(t => t.ItemId == item.ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.parentId = item.ParentId;
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            foreach (ModuleButtonEntity item in moduleButtonData)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = moduleButtonData.Count(t => t.ParentId == item.ModuleButtonId) == 0 ? false : true;
                tree.id = item.ModuleButtonId;
                tree.text = item.FullName;
                tree.value = item.ModuleButtonId;
                if (item.ParentId == "0")
                {
                    tree.parentId = item.ModuleId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.checkstate = existModuleButton.Count(t => t.ItemId == item.ModuleButtonId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-wrench " + item.ModuleId;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 系统视图列表
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ModuleColumnTreeJson(string postId)
        {
            var existModuleColumn = permissionBLL.GetModuleColumnList(postId);
            var moduleData = moduleBLL.GetList();
            var moduleColumnData = moduleColumnBLL.GetList();
            var treeList = new List<TreeEntity>();
            foreach (ModuleEntity item in moduleData)
            {
                TreeEntity tree = new TreeEntity();
                tree.id = item.ModuleId;
                tree.text = item.FullName;
                tree.value = item.ModuleId;
                tree.checkstate = existModuleColumn.Count(t => t.ItemId == item.ModuleId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = true;
                tree.parentId = item.ParentId;
                tree.img = item.Icon;
                treeList.Add(tree);
            }
            foreach (ModuleColumnEntity item in moduleColumnData)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = moduleColumnData.Count(t => t.ParentId == item.ModuleColumnId) == 0 ? false : true;
                tree.id = item.ModuleColumnId;
                tree.text = item.FullName;
                tree.value = item.ModuleColumnId;
                if (item.ParentId == "0")
                {
                    tree.parentId = item.ModuleId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.checkstate = existModuleColumn.Count(t => t.ItemId == item.ModuleColumnId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-filter " + item.ModuleId;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 数据权限列表
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OrganizeTreeJson(string postId)
        {
            var existAuthorizeData = permissionBLL.GetAuthorizeDataList(postId);
            var organizedata = organizeBLL.GetList();
            var departmentdata = departmentBLL.GetList();
            var treeList = new List<TreeEntity>();
            foreach (OrganizeEntity item in organizedata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = organizedata.Count(t => t.ParentId == item.OrganizeId) == 0 ? false : true;
                if (hasChildren == false)
                {
                    hasChildren = departmentdata.Count(t => t.OrganizeId == item.OrganizeId) == 0 ? false : true;
                    if (hasChildren == false)
                    {
                        continue;
                    }
                }
                tree.id = item.OrganizeId;
                tree.text = item.FullName;
                tree.value = item.OrganizeId;
                tree.parentId = item.ParentId;
                if (item.ParentId == "0")
                {
                    tree.img = "fa fa-sitemap";
                }
                else
                {
                    tree.img = "fa fa-home";
                }
                tree.checkstate = existAuthorizeData.Count(t => t.ResourceId == item.OrganizeId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            foreach (DepartmentEntity item in departmentdata)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = departmentdata.Count(t => t.ParentId == item.DepartmentId) == 0 ? false : true;
                tree.id = item.DepartmentId;
                tree.text = item.FullName;
                tree.value = item.DepartmentId;
                if (item.ParentId == "0")
                {
                    tree.parentId = item.OrganizeId;
                }
                else
                {
                    tree.parentId = item.ParentId;
                }
                tree.checkstate = existAuthorizeData.Count(t => t.ResourceId == item.DepartmentId);
                tree.showcheck = true;
                tree.isexpand = true;
                tree.complete = true;
                tree.img = "fa fa-umbrella";
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            int authorizeType = -1;
            if (existAuthorizeData.ToList().Count > 0)
            {
                authorizeType = existAuthorizeData.ToList()[0].AuthorizeType.ToInt();
            }
            var JsonData = new
            {
                authorizeType = authorizeType,
                authorizeData = existAuthorizeData,
                treeJson = treeList.TreeToJson(),
            };
            return Content(JsonData.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存岗位成员
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <param name="userIds">成员Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveMember(string postId, string userIds)
        {
            permissionBLL.SaveMember(AuthorizeTypeEnum.Post, postId, userIds);
            return Success("保存成功。");
        }
        /// <summary>
        /// 保存岗位授权
        /// </summary>
        /// <param name="postId">岗位Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataJson">数据权限</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveAuthorize(string postId, string moduleIds, string moduleButtonIds, string moduleColumnIds, string authorizeDataJson)
        {
            permissionBLL.SaveAuthorize(AuthorizeTypeEnum.Post, postId, moduleIds, moduleButtonIds, moduleColumnIds, authorizeDataJson);
            return Success("保存成功。");
        }
        #endregion
    }
}
