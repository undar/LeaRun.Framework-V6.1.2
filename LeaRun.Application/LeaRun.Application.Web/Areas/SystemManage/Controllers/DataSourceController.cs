using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-28 16:58
    /// 描 述：数据源表
    /// </summary>
    public class DataSourceController : MvcControllerBase
    {
        private DataSourceBLL datasourcebll = new DataSourceBLL();

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
        /// <summary>
        /// 表单页面
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
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var data = datasourcebll.GetPageList(pagination, queryJson);
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
        public ActionResult GetListJson(string queryJson)
        {
            var data = datasourcebll.GetList(queryJson);
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
            var data = datasourcebll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        [HttpGet]
        public ActionResult GetTableData(string dbLinkId, string tableName)
        {
            var data = datasourcebll.GetTableDataList(dbLinkId, tableName);
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
            datasourcebll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult TestData(string strEntity, string queryJson)
        {
            DataBaseLinkBLL linkbll = new DataBaseLinkBLL();
            DataSourceEntity entity = strEntity.ToObject<DataSourceEntity>();
            DataBaseLinkEntity link = linkbll.GetEntity(entity.DbId);
            string data=datasourcebll.TestData(link.DbType,link.DbConnection,entity.Strsql);
            return Content(data);
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
        public ActionResult SaveForm(string keyValue, DataSourceEntity entity)
        {
            datasourcebll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}
