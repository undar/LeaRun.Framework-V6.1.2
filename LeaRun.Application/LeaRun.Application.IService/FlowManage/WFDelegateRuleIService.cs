using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util.WebControl;
using System.Data;

namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：工作流委托规则表操作接口（支持：SqlServer）
    /// </summary>
    public interface WFDelegateRuleIService
    {
        #region 获取数据
        /// <summary>
        /// 获取委托规则分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson, string userId = null);
        /// <summary>
        /// 获取流程模板信息列表数据
        /// </summary>
        /// <returns></returns>
        DataTable GetSchemeInfoList(string ruleId);
        /// <summary>
        /// 获取委托规则实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        WFDelegateRuleEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据模板信息Id获取委托规则实体
        /// </summary>
        /// <param name="shcemeInfoId"></param>
        /// <returns></returns>
        DataTable GetEntityBySchemeInfoId(string shcemeInfoId, string[] objectIdList);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托规则
        /// </summary>
        /// <returns></returns>
        int SaveDelegateRule(string keyValue, WFDelegateRuleEntity ruleEntity, string[] shcemeInfoIdlist);
        /// <summary>
        /// 删除委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int DeleteRule(string keyValue);
        /// <summary>
        /// 使能委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="enableMark"></param>
        /// <returns></returns>
        int UpdateRuleEnable(string keyValue, int enableMark);
        #endregion
    }
}
