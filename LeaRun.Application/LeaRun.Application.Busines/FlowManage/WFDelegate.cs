using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using System.Data;

namespace LeaRun.Application.Busines.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 13:57
    /// 描 述：工作流工作委托操作（支持：SqlServer）
    /// </summary>
    public class WFDelegate
    {
        private WFDelegateRuleIService wfDelegateRuleService = new WFDelegateRuleService();
        private WFDelegateRecordIService wfDelegateRecordService = new WFDelegateRecordService();
        #region 获取数据
        /// <summary>
        /// 获取委托规则分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetRulePageList(Pagination pagination, string queryJson,string userId=null)
        {
            return wfDelegateRuleService.GetPageList(pagination, queryJson, userId);
        }
        /// <summary>
        /// 获取流程模板信息列表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSchemeInfoList(string ruleId)
        {
            return wfDelegateRuleService.GetSchemeInfoList(ruleId);
        }
         /// <summary>
        /// 获取委托规则实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public WFDelegateRuleEntity GetRuleEntity(string keyValue)
        {
            return wfDelegateRuleService.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取委托记录分页数据(type 1：委托记录，其他：被委托记录)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="type"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetRecordPageList(Pagination pagination, string queryJson,int type, string userId = null)
        {
            return wfDelegateRecordService.GetPageList(pagination, queryJson, type, userId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托规则
        /// </summary>
        /// <returns></returns>
        public int SaveDelegateRule(string keyValue, WFDelegateRuleEntity ruleEntity, string[] shcemeInfoIdlist)
        {
            return wfDelegateRuleService.SaveDelegateRule(keyValue, ruleEntity, shcemeInfoIdlist);
        }
        /// <summary>
        /// 删除委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteRule(string keyValue)
        {
            return wfDelegateRuleService.DeleteRule(keyValue);
        }
        /// <summary>
        /// 使能委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="enableMark"></param>
        /// <returns></returns>
        public int UpdateRuleEnable(string keyValue, int enableMark)
        {
            return wfDelegateRuleService.UpdateRuleEnable(keyValue, enableMark);
        }
        #endregion
    }
}
