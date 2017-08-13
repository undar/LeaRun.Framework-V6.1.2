using LeaRun.Application.Entity.FlowManage;
using System;

namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.12 15:54
    /// 描 述：工作流实例处理接口
    /// </summary>
    public interface WFRuntimeIService
    {
        #region 流程处理API
        /// <summary>
        /// 创建一个实例
        /// </summary>
        /// <param name="processId">进程GUID</param>
        /// <param name="schemeInfoId">模板信息ID</param>
        /// <param name="wfLevel"></param>
        /// <param name="code">进程编号</param>
        /// <param name="customName">自定义名称</param>
        /// <param name="description">备注</param>
        /// <param name="frmData">表单数据信息</param>
        /// <returns></returns>
        bool CreateInstance(Guid processId, string schemeInfoId, WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null);
        /// <summary>
        /// 创建一个实例(草稿创建)
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="code"></param>
        /// <param name="customName"></param>
        /// <param name="description"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        bool CreateInstance(WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null);
        /// <summary>
        /// 编辑表单再次提交(驳回后处理)
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="description"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        bool EditionInstance(string processId, string description, string frmData = null);
        /// <summary>
        /// 创建一个草稿
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="schemeInfoId"></param>
        /// <param name="wfLevel"></param>
        /// <param name="code"></param>
        /// <param name="customName"></param>
        /// <param name="description"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        bool CreateRoughdraft(Guid processId, string schemeInfoId, WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null);
        /// <summary>
        /// 创建一个草稿
        /// </summary>
        /// <param name="wfProcessInstanceEntity"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        bool EditionRoughdraft(WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null);
        /// <summary>
        /// 节点审核(同意)
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        bool NodeVerification(string processId, bool flag, string description = "");
        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="nodeId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        bool NodeReject(string processId, string nodeId, string description = "");
        /// <summary>
        /// 召回流程进程
        /// </summary>
        /// <param name="processId"></param>
        void CallingBackProcess(string processId);
        /// <summary>
        /// 终止一个实例(彻底删除)
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        void KillProcess(string processId);
        /// <summary>
        /// 获取某个节点（审核人所能看到的提交表单的权限）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string GetProcessSchemeContentByNodeId(string data,string form, string nodeId);
        /// <summary>
        /// 获取某个节点（审核人所能看到的提交表单的权限）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        string GetProcessSchemeContentByUserId(string data, string userId);
        #endregion
    }
}
