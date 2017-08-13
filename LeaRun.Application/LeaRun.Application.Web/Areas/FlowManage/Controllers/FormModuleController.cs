using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Busines.FlowManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using System.Collections.Generic;
using LeaRun.Application.Busines.SystemManage;

using LeaRun.Application.Cache;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using System.Linq;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Busines.AuthorizeManage;
using System.Data;
namespace LeaRun.Application.Web.Areas.FlowManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:46
    /// 描 述：表单关联表
    /// </summary>
    public class FormModuleController : MvcControllerBase
    {

        private FormModuleRelationBLL formmodulerelationbll = new FormModuleRelationBLL();
        FormModuleBLL formmodulebll = new FormModuleBLL();
        FormModuleContentBLL formcontentbll = new FormModuleContentBLL();
        FormModuleInstanceBLL instancebll = new FormModuleInstanceBLL();
        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ManageForm()
        {
            return View();
        }
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
        public ActionResult FormReleaseer()
        {
            return View();
        }
        [HttpGet]
        public ActionResult FormPreview()
        {
            return View();
        }
        public ActionResult CustmerFormIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CustmerFormForm()
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
            var data = formmodulebll.GetPageList(pagination, queryJson);
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
        /// 表单树 
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetFormTreeJson()
        {
            var data = formmodulebll.GetAllList();
            var treeList = new List<TreeEntity>();
            string FrmType = "";
            foreach (DataRow item in data.Rows)
            {
                TreeEntity tree = new TreeEntity();
                if (FrmType != item["FrmCategory"].ToString())
                {
                    TreeEntity tree1 = new TreeEntity();
                    FrmType = item["FrmCategory"].ToString();
                    tree1.id = FrmType;
                    tree1.text = item["FrmTypeName"].ToString();
                    tree1.value = FrmType;
                    tree1.isexpand = true;
                    tree1.complete = true;
                    tree1.hasChildren = true;
                    tree1.parentId = "0";
                    tree1.img = "fa fa-list-alt";
                    tree1.Attribute = "Sort";
                    tree1.AttributeValue = "FrmType";
                    treeList.Add(tree1);
                }
                tree.id = item["FrmId"].ToString();
                tree.text = item["FrmName"].ToString();
                tree.value = item["FrmId"].ToString();
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = false;
                tree.parentId = FrmType;
                tree.Attribute = "Sort";
                tree.AttributeValue = "Frm";
                treeList.Add(tree);
            }
            return Content(treeList.TreeToJson());
        }
        public ActionResult GetTreeJson(string queryJson)
        {
            DataItemCache dataItemCache = new DataItemCache();
            var formModuleData = formmodulebll.GetList();
            var data = dataItemCache.GetDataItemList("FormSort");
            var treeList = new List<TreeEntity>();
            foreach (DataItemModel item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren =true;//此处做下判断
                tree.id = item.ItemDetailId;
                tree.text = item.ItemName;
                tree.value = item.ItemValue;
                tree.parentId = item.ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "formCategory";
                treeList.Add(tree);
            }
            foreach (FormModuleEntity item in formModuleData)
            {
                #region 部门
                TreeEntity tree = new TreeEntity();
                bool hasChildren = false;
                tree.id = item.FrmId;
                tree.text = item.FrmName;
                tree.value = item.FrmId;
                
                tree.parentId = item.FrmCategory;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                tree.Attribute = "Sort";
                tree.AttributeValue = "form";
                tree.AttributeA = "version";
                tree.AttributeValueA = item.Version;
                treeList.Add(tree);
                #endregion
            }
            //if (!string.IsNullOrEmpty(queryJson))
            //{
            //    treeList = treeList.TreeWhere(t => t.text.Contains(keyword), "id", "parentId");
            //}
            return Content(treeList.TreeToJson());
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetEntityJson(string keyValue)
        {
            var data = formmodulebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetInstanceEntityJson(string keyValue)
        {
            var data = instancebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
          /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetRelationPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = formmodulerelationbll.GetPageList(pagination, queryJson);
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
        [HttpGet]
        public ActionResult GetInstancePageList(Pagination pagination, string relationFormId)
        {
            var watch = CommonHelper.TimerStart();
            var data =instancebll.GetPageList(pagination, relationFormId).ToJson();
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetFormContentJson(string keyValue)
        {
            FormModuleRelationEntity entity = formmodulerelationbll.GetEntity(keyValue);
            var data = formcontentbll.GetEntity(entity.ModuleContentId);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetRelationListJson(string queryJson)
        {
            var data = formmodulerelationbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetRelationEntityJson(string keyValue)
        {
            var data = formmodulerelationbll.GetEntity(keyValue);
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
            formmodulebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveInstanceForm(string keyValue)
        {
            instancebll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, FormModuleEntity u)
        {
            FormModuleEntity  entity = u;
            formmodulebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveCustmerFormInstance(string keyValue, FormModuleInstanceEntity entity)
        {
            instancebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// （启用、禁用）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="State">状态：1-启动；0-禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SubmitUpdateState(string keyValue, int State)
        {
            formmodulebll.UpdateState(keyValue, State);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RelationRemove(string keyValue)
        {
            formmodulerelationbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="baseInfo">实体对象</param>
        /// <param name="moduleInfo">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRelationForm(string keyValue, string baseInfo, string moduleInfo)
        {
            FormModuleRelationEntity entity = baseInfo.ToObject<FormModuleRelationEntity>();
            ModuleEntity module = null;
            if (entity.FrmKind==0)
            {
                module=moduleInfo.ToObject<ModuleEntity>();
                entity.ObjectName = module.FullName;
            }
            
            formmodulerelationbll.SaveForm(keyValue, entity, module);
            return Success("操作成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult UpdateForm(string keyValue)
        {
            formmodulerelationbll.UpdateForm(keyValue);
            return Success("操作成功。");
        }
        #endregion
    }
}
