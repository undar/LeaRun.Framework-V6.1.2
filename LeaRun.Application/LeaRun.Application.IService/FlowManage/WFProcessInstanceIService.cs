using LeaRun.Application.Entity.FlowManage;
using LeaRun.Util.WebControl;
using LeaRun.Util.FlowWork;
using System.Data;
using System.Collections.Generic;
namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例表操作接口（支持：SqlServer）
    /// </summary>
    public interface WFProcessInstanceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取流程监控数据（用于流程监控）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取流程实例分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="type">3草稿</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson, string type);
         /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetToMeBeforePageList(Pagination pagination, string queryJson);
         /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetToMeAfterPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实例进程信息实体
        /// </summary>
        /// <param name="keyVlaue"></param>
        /// <returns></returns>
        WFProcessInstanceEntity GetEntity(string keyVlaue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 存储工作流实例进程（编辑草稿用）
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="wfOperationHistoryEntity"></param>
        /// <returns></returns>
        int SaveProcess(string processId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity wfOperationHistoryEntity = null);
        /// <summary>
        /// 存储工作流实例进程(创建实例进程)
        /// </summary>
        /// <param name="wfRuntimeModel"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntity"></param>
        /// <returns></returns>
        int SaveProcess(WF_RuntimeModel wfRuntimeModel, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, WFProcessTransitionHistoryEntity processTransitionHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList);
         /// <summary>
        /// 存储工作流实例进程（审核驳回重新提交）
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        int SaveProcess(WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null);
        /// <summary>
        ///  更新流程实例 审核节点用
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbbaseId"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntityList"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        int SaveProcess(string sql, string dbbaseId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null);
       
        /// <summary>
        /// 保存工作流进程实例
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <returns></returns>
        int SaveProcess(WFProcessInstanceEntity processInstanceEntity);
        /// <summary>
        /// 删除工作流实例进程
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        int DeleteProcess(string keyValue);
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        int OperateVirtualProcess(string keyValue, int state);
                /// <summary>
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        void DesignateProcess(string processId, string makeLists);
        #endregion
    }
}
