using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.Offices;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.CustomerManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.3.11 14:22
    /// 描 述：客户订单
    /// </summary>
    public class OrderController : MvcControllerBase
    {
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private OrderBLL orderBLL = new OrderBLL();
        private OrderEntryBLL orderEntryBLL = new OrderEntryBLL();

        #region 视图功能
        /// <summary>
        /// 订单列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 订单表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Form()
        {

            if (Request["keyValue"] == null)
            {
                ViewBag.OrderCode = codeRuleBLL.GetBillCode(SystemInfo.CurrentUserId, "", ((int)CodeRuleEnum.Customer_OrderCode).ToString());
            }
            return View();
        }
        /// <summary>
        /// 订单详细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Detail()
        {
            return View();
        }
        /// <summary>
        /// 选择商品信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OptionProduct()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表（订单主表）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = orderBLL.GetPageList(pagination, queryJson);
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
        /// 获取列表（订单明细表）
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetOrderEntryListJson(string orderId)
        {
            var data = orderEntryBLL.GetList(orderId);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 （主表+明细）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var jsonData = new
            {
                order = orderBLL.GetEntity(keyValue),
                orderEntry = orderEntryBLL.GetList(keyValue)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取前单数据实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetPrevJson(string keyValue)
        {
            var data = orderBLL.GetPrevOrNextEntity(keyValue, 1);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取后单数据实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetNextJson(string keyValue)
        {
            var data = orderBLL.GetPrevOrNextEntity(keyValue, 2);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除订单数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveForm(string keyValue)
        {
            orderBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存订单表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="orderEntryJson">明细实体对象Json</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, OrderEntity entity, string orderEntryJson)
        {
            var orderEntryList = orderEntryJson.ToList<OrderEntryEntity>();
            orderBLL.SaveForm(keyValue, entity, orderEntryList);
            return Success("操作成功。");
        }
        #endregion

        #region 导出数据
        /// <summary>
        /// 导出订单明细（Excel模板导出）
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        public void ExportOrderEntry(string orderId)
        {
            var order = orderBLL.GetEntity(orderId);
            var orderEntry = orderEntryBLL.GetList(orderId);

            List<TemplateMode> list = new List<TemplateMode>();
            //设置主表信息
            list.Add(new TemplateMode() { row = 1, cell = 1, value = order.CustomerName });
            list.Add(new TemplateMode() { row = 1, cell = 5, value = order.SellerName });
            list.Add(new TemplateMode() { row = 1, cell = 8, value = order.OrderDate.ToDate().ToString("yyyy-MM-dd") });
            list.Add(new TemplateMode() { row = 1, cell = 11, value = order.OrderCode });
            list.Add(new TemplateMode() { row = 17, cell = 1, value = order.DiscountSum.ToString() });
            list.Add(new TemplateMode() { row = 17, cell = 5, value = order.Accounts.ToString() });
            list.Add(new TemplateMode() { row = 17, cell = 8, value = order.PaymentDate.ToDate().ToString("yyyy-MM-dd") });
            list.Add(new TemplateMode() { row = 17, cell = 11, value = new DataItemCache().ToItemName("Client_PaymentMode", order.PaymentMode) });
            list.Add(new TemplateMode() { row = 18, cell = 1, value = order.SaleCost.ToString() });
            list.Add(new TemplateMode() { row = 18, cell = 5, value = order.CreateUserName });
            list.Add(new TemplateMode() { row = 18, cell = 8, value = order.ContractCode });
            list.Add(new TemplateMode() { row = 18, cell = 11, value = order.ContractFile });
            list.Add(new TemplateMode() { row = 19, cell = 1, value = order.AbstractInfo });
            list.Add(new TemplateMode() { row = 20, cell = 1, value = order.Description });
            //设置明细信息
            int rowIndex = 4;
            foreach (OrderEntryEntity item in orderEntry)
            {
                list.Add(new TemplateMode() { row = rowIndex, cell = 0, value = item.ProductName });
                list.Add(new TemplateMode() { row = rowIndex, cell = 3, value = item.ProductCode });
                list.Add(new TemplateMode() { row = rowIndex, cell = 4, value = item.UnitId });
                list.Add(new TemplateMode() { row = rowIndex, cell = 5, value = item.Qty.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 6, value = item.Price.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 7, value = item.Amount.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 8, value = item.TaxRate.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 9, value = item.Taxprice.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 10, value = item.Tax.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 11, value = item.TaxAmount.ToString() });
                list.Add(new TemplateMode() { row = rowIndex, cell = 12, value = item.Description });
                rowIndex++;
            }
            //设置明细合计
            list.Add(new TemplateMode() { row = 16, cell = 5, value = orderEntry.Sum(t => t.Qty).ToString() });
            list.Add(new TemplateMode() { row = 16, cell = 6, value = orderEntry.Sum(t => t.Price).ToString() });
            list.Add(new TemplateMode() { row = 16, cell = 7, value = orderEntry.Sum(t => t.Amount).ToString() });
            list.Add(new TemplateMode() { row = 16, cell = 9, value = orderEntry.Sum(t => t.Taxprice).ToString() });
            list.Add(new TemplateMode() { row = 16, cell = 10, value = orderEntry.Sum(t => t.Tax).ToString() });
            list.Add(new TemplateMode() { row = 16, cell = 11, value = orderEntry.Sum(t => t.TaxAmount).ToString() });
            //执行导出
            ExcelHelper.ExcelDownload(list, "OrderEntry.xlsx", "订单明细-" + order.OrderCode + ".xlsx");
        }
        #endregion
    }
}
