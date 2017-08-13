using LeaRun.Application.Busines.CustomerManage;
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
    /// 创 建：undar
    /// 日 期：2016-04-28 15:20
    /// 描 述：现金余额报表
    /// </summary>
    public class CashBalanceReportController : MvcControllerBase
    {
        private CashBalanceBLL cashbalancebll = new CashBalanceBLL(); 

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
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = cashbalancebll.GetList(queryJson);
            return ToJsonResult(data);
        }
        #endregion
    }
}
