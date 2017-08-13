using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailController : MvcControllerBase
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        private DataItemCache dataItemCache = new DataItemCache();

        #region 视图功能
        /// <summary>
        /// 明细管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 明细表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 明细详细
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
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        public ActionResult GetTreeListJson(string itemId, string condition, string keyword)
        {
            var data = dataItemDetailBLL.GetList(itemId).ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                #region 多条件查询
                switch (condition)
                {
                    case "ItemName":        //项目名
                        data = data.TreeWhere(t => t.ItemName.Contains(keyword), "ItemDetailId");
                        break;
                    case "ItemValue":      //项目值
                        data = data.TreeWhere(t => t.ItemValue.Contains(keyword), "ItemDetailId");
                        break;
                    case "SimpleSpelling": //拼音
                        data = data.TreeWhere(t => t.SimpleSpelling.Contains(keyword), "ItemDetailId");
                        break;
                    default:
                        break;
                }
                #endregion
            }
            var TreeList = new List<TreeGridEntity>();
            foreach (DataItemDetailEntity item in data)
            {
                TreeGridEntity tree = new TreeGridEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.ItemDetailId;
                tree.parentId = item.ParentId;
                tree.expanded = true;
                tree.hasChildren = hasChildren;
                tree.entityJson = item.ToJson();
                TreeList.Add(tree);
            }
            return Content(TreeList.TreeJson());
        }
        /// <summary>
        /// 明细实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = dataItemDetailBLL.GetEntity(keyValue);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表树Json</returns>
        [HttpGet]
        public ActionResult GetDataItemTreeJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            var treeList = new List<TreeEntity>();
            foreach (DataItemModel item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                tree.id = item.ItemDetailId;
                tree.text = item.ItemName;
                tree.value = item.ItemValue;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetDataItemListJson(string EnCode)
        {
            var data = dataItemCache.GetDataItemList(EnCode);
            return Content(data.ToJson());
        }
        /// <summary>
        /// 获取数据字典子列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <param name="ItemValue">项目值</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetSubDataItemListJson(string EnCode, string ItemValue)
        {
            var data = dataItemCache.GetSubDataItemList(EnCode, ItemValue);
            return Content(data.ToJson());
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="ItemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistItemValue(string ItemValue, string keyValue, string itemId)
        {
            bool IsOk = dataItemDetailBLL.ExistItemValue(ItemValue, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="ItemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ExistItemName(string ItemName, string keyValue, string itemId)
        {
            bool IsOk = dataItemDetailBLL.ExistItemName(ItemName, keyValue, itemId);
            return Content(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            dataItemDetailBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            dataItemDetailBLL.SaveForm(keyValue, dataItemDetailEntity);
            return Success("操作成功。");
        }
        #endregion
    }
}
