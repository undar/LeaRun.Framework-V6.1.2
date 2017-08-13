using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.BaseManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.FlowWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.12 15:54
    /// 描 述：工作流运行实例处理方法类
    /// </summary>
    public class WFRuntimeService :RepositoryFactory, WFRuntimeIService
    {
        private WFSchemeInfoIService wfSchemeInfoService = new WFSchemeInfoService();
        private WFSchemeContentIService wfSchemeContentService = new WFSchemeContentService();

        private WFProcessInstanceIService wfProcessInstanceService = new WFProcessInstanceService();
        private WFProcessSchemeIService wfProcessSchemeService = new WFProcessSchemeService();
        private WFProcessOperationHistoryIService wfProcessOperationHistoryService = new WFProcessOperationHistoryService();
        private WFProcessTransitionHistoryIService wfProcessTransitionHistoryService = new WFProcessTransitionHistoryService();
        private WFDelegateRuleIService wfDelegateRuleService = new WFDelegateRuleService();

        private IUserService userService = new UserService();//用户数据库操作类
        private IDepartmentService departmentService = new DepartmentService();
        private IOrganizeService organizeService = new OrganizeService();

        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();

        private string delegateUserList = "";

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
        public bool CreateInstance(Guid processId, string schemeInfoId, WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null)
        {
            
            try
            {
                WFSchemeInfoEntity wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(schemeInfoId);
                //WFSchemeContentEntity wfSchemeContentEntity = wfSchemeContentService.GetEntity(schemeInfoId, wfSchemeInfoEntity.SchemeVersion);
               
                WF_RuntimeInitModel wfRuntimeInitModel = new WF_RuntimeInitModel()
                {
                    schemeContent = wfSchemeInfoEntity.SchemeContent,
                    currentNodeId = "",
                    frmData = frmData,
                    processId = processId.ToString()
                };
                IWF_Runtime wfruntime = null;

                //if (wfSchemeInfoEntity.FrmType == 0)
                //{
                //    if(frmData == null)
                //    {
                //         throw new Exception("自定义表单需要提交表单数据");
                //    }
                //    else
                //    {
                //        wfruntime = new WF_Runtime(wfRuntimeInitModel);
                //    }
                //}
                //else
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel, GetFrmData);
                //}
                wfruntime = new WF_Runtime(wfRuntimeInitModel);   

                #region 实例信息
                wfProcessInstanceEntity.ActivityId = wfruntime.runtimeModel.nextNodeId;
                wfProcessInstanceEntity.ActivityType = wfruntime.GetStatus();//-1无法运行,0会签开始,1会签结束,2一般节点,4流程运行结束
                wfProcessInstanceEntity.ActivityName = wfruntime.runtimeModel.nextNode.name;
                wfProcessInstanceEntity.PreviousId = wfruntime.runtimeModel.currentNodeId;
                wfProcessInstanceEntity.SchemeType = wfSchemeInfoEntity.SchemeType;
  
                wfProcessInstanceEntity.EnabledMark = 1;//正式运行
                wfProcessInstanceEntity.MakerList =(wfruntime.GetStatus() != 4 ? GetMakerList(wfruntime) : "");//当前节点可执行的人信息
                wfProcessInstanceEntity.isFinish = (wfruntime.GetStatus() == 4 ? 1 : 0);
                #endregion

                #region 实例模板
                var data = new
                {
                    SchemeContent = wfSchemeInfoEntity.SchemeContent,
                    frmData = wfSchemeInfoEntity.FrmType != 2 ? frmData : null
                };
                WFProcessSchemeEntity wfProcessSchemeEntity = new WFProcessSchemeEntity { 
                    SchemeInfoId = schemeInfoId,
                    SchemeVersion = wfSchemeInfoEntity.SchemeVersion,
                    ModuleId=wfSchemeInfoEntity.ModuleId,
                    ProcessType = 1,//1正式，0草稿
                    SchemeContent = data.ToJson().ToString()
                };
                #endregion

                #region 流程操作记录
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.Content = "【创建】" + OperatorProvider.Provider.Current().UserName + "创建了一个流程进程【" + wfProcessInstanceEntity.Code + "/" + wfProcessInstanceEntity.CustomName + "】";
                #endregion

                #region 流转记录
                WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                processTransitionHistoryEntity.fromNodeId = wfruntime.runtimeModel.currentNodeId;
                processTransitionHistoryEntity.fromNodeName = wfruntime.runtimeModel.currentNode.name.Value;
                processTransitionHistoryEntity.fromNodeType = wfruntime.runtimeModel.currentNodeType;
                processTransitionHistoryEntity.toNodeId = wfruntime.runtimeModel.nextNodeId;
                processTransitionHistoryEntity.toNodeName = wfruntime.runtimeModel.nextNode.name.Value;
                processTransitionHistoryEntity.toNodeType = wfruntime.runtimeModel.nextNodeType;
                processTransitionHistoryEntity.TransitionSate =0;
                processTransitionHistoryEntity.isFinish = (processTransitionHistoryEntity.toNodeType == 4 ? 1 : 0);
                #endregion

                #region 委托记录
                List<WFDelegateRecordEntity> delegateRecordEntitylist = GetDelegateRecordList(schemeInfoId, wfProcessInstanceEntity.Code, wfProcessInstanceEntity.CustomName, wfProcessInstanceEntity.MakerList);
                wfProcessInstanceEntity.MakerList += delegateUserList;
                #endregion

                wfProcessInstanceService.SaveProcess(wfruntime.runtimeModel, wfProcessInstanceEntity, wfProcessSchemeEntity, processOperationHistoryEntity, processTransitionHistoryEntity, delegateRecordEntitylist);

                return true;
            }
            catch
            {
                throw;
            }
            
        }
        /// <summary>
        /// 创建一个实例(草稿创建)
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="code"></param>
        /// <param name="customName"></param>
        /// <param name="description"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        public bool CreateInstance(WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null)
        {
            try
            {
                WFProcessInstanceEntity _wfProcessInstanceEntity = wfProcessInstanceService.GetEntity(wfProcessInstanceEntity.Id);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.ProcessSchemeId);
                dynamic schemeContentJson = wfProcessSchemeEntity.SchemeContent.ToJson();//获取工作流模板内容的json对象;
                WF_RuntimeInitModel wfRuntimeInitModel = new WF_RuntimeInitModel()
                {
                    schemeContent = schemeContentJson.SchemeContent.Value,
                    currentNodeId = "",
                    frmData = frmData,
                    processId = wfProcessSchemeEntity.Id
                };
                IWF_Runtime wfruntime = null;
                //if (_wfProcessInstanceEntity.FrmType == 0)
                //{
                //    if (frmData == null)
                //    {
                //        throw new Exception("自定义表单需要提交表单数据");
                //    }
                //    else
                //    {
                //        wfruntime = new WF_Runtime(wfRuntimeInitModel);
                //    }
                    
                //}
                //else 
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel, GetFrmData);
                //}

                #region 实例信息
                wfProcessInstanceEntity.ActivityId = wfruntime.runtimeModel.nextNodeId;
                wfProcessInstanceEntity.ActivityType = wfruntime.GetStatus();//-1无法运行,0会签开始,1会签结束,2一般节点,4流程运行结束
                wfProcessInstanceEntity.ActivityName = wfruntime.runtimeModel.nextNode.name;
                wfProcessInstanceEntity.PreviousId = wfruntime.runtimeModel.currentNodeId;
                wfProcessInstanceEntity.EnabledMark = 1;//正式运行
                wfProcessInstanceEntity.MakerList = (wfruntime.GetStatus() != 4 ? GetMakerList(wfruntime) : "");//当前节点可执行的人信息
                wfProcessInstanceEntity.isFinish = (wfruntime.GetStatus() == 4 ? 1 : 0);
                #endregion

                #region 实例模板
                var data = new
                {
                    SchemeContent = schemeContentJson.SchemeContent.Value,
                    frmData = frmData
                };
                wfProcessSchemeEntity.ProcessType = 1;//1正式，0草稿
                wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();
                #endregion

                #region 流程操作记录
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.Content = "【创建】" + OperatorProvider.Provider.Current().UserName + "创建了一个流程进程【" + wfProcessInstanceEntity.Code + "/" + wfProcessInstanceEntity.CustomName + "】";
                #endregion

                #region 流转记录
                WFProcessTransitionHistoryEntity processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                processTransitionHistoryEntity.fromNodeId = wfruntime.runtimeModel.currentNodeId;
                processTransitionHistoryEntity.fromNodeName = wfruntime.runtimeModel.currentNode.name.Value;
                processTransitionHistoryEntity.fromNodeType = wfruntime.runtimeModel.currentNodeType;
                processTransitionHistoryEntity.toNodeId = wfruntime.runtimeModel.nextNodeId;
                processTransitionHistoryEntity.toNodeName = wfruntime.runtimeModel.nextNode.name.Value;
                processTransitionHistoryEntity.toNodeType = wfruntime.runtimeModel.nextNodeType;
                processTransitionHistoryEntity.TransitionSate = 0;
                processTransitionHistoryEntity.isFinish = (processTransitionHistoryEntity.toNodeType == 4 ? 1 : 0);
                #endregion

                #region 委托记录
                List<WFDelegateRecordEntity> delegateRecordEntitylist = GetDelegateRecordList(wfProcessSchemeEntity.SchemeInfoId, wfProcessInstanceEntity.Code, wfProcessInstanceEntity.CustomName, wfProcessInstanceEntity.MakerList);
                wfProcessInstanceEntity.MakerList += delegateUserList;
                #endregion

                wfProcessInstanceService.SaveProcess(wfruntime.runtimeModel, wfProcessInstanceEntity, wfProcessSchemeEntity, processOperationHistoryEntity, processTransitionHistoryEntity, delegateRecordEntitylist);
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 编辑表单再次提交(驳回后处理)
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="description"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        public bool EditionInstance(string processId, string description, string frmData = null)
        {
            try
            {
                WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceService.GetEntity(processId);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.ProcessSchemeId);
                WFSchemeInfoEntity wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(wfProcessSchemeEntity.SchemeInfoId);
                dynamic schemeContentJson = wfProcessSchemeEntity.SchemeContent.ToJson();//获取工作流模板内容的json对象;
                var data = new
                {
                    SchemeContent = schemeContentJson.SchemeContent.Value,
                    frmData = wfSchemeInfoEntity.FrmType != 2 ? frmData : null
                };
                wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();

                wfProcessInstanceEntity.isFinish = 0;
                if (string.IsNullOrEmpty(description))
                {
                    wfProcessInstanceEntity.Description = description;
                }
                wfProcessInstanceEntity.CreateDate = DateTime.Now;

                #region 流程操作记录
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.Content = "【创建】" + OperatorProvider.Provider.Current().UserName + "创建了一个流程进程【" + wfProcessInstanceEntity.Code + "/" + wfProcessInstanceEntity.CustomName + "】";
                #endregion

                #region 委托记录
                List<WFDelegateRecordEntity> delegateRecordEntitylist = GetDelegateRecordList(wfProcessSchemeEntity.SchemeInfoId, wfProcessInstanceEntity.Code, wfProcessInstanceEntity.CustomName, wfProcessInstanceEntity.MakerList);
                wfProcessInstanceEntity.MakerList += delegateUserList;
                #endregion

                wfProcessInstanceService.SaveProcess(wfProcessInstanceEntity, wfProcessSchemeEntity, processOperationHistoryEntity, delegateRecordEntitylist);

                return true;
            }
            catch
            {
                throw;
            }
        }
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
        public bool CreateRoughdraft(Guid processId, string schemeInfoId, WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null)
        {
            try
            {
                WFSchemeInfoEntity wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(schemeInfoId);
                WFSchemeContentEntity wfSchemeContentEntity = wfSchemeContentService.GetEntity(schemeInfoId, wfSchemeInfoEntity.SchemeVersion);
                
                wfProcessInstanceEntity.ActivityId = "";
                wfProcessInstanceEntity.ActivityName = "";
                wfProcessInstanceEntity.ActivityType = 0;//开始节点
                wfProcessInstanceEntity.isFinish = 0;
                wfProcessInstanceEntity.SchemeType = wfSchemeInfoEntity.SchemeType;
                wfProcessInstanceEntity.EnabledMark = 3;//草稿
                wfProcessInstanceEntity.CreateDate = DateTime.Now;
                //wfProcessInstanceEntity.FrmType = wfSchemeInfoEntity.FrmType;

                WFProcessSchemeEntity wfProcessSchemeEntity = new WFProcessSchemeEntity();
                wfProcessSchemeEntity.SchemeInfoId = schemeInfoId;
                wfProcessSchemeEntity.SchemeVersion = wfSchemeInfoEntity.SchemeVersion;
                wfProcessSchemeEntity.ProcessType = wfProcessInstanceEntity.EnabledMark;
                var data = new
                {
                    SchemeContent = wfSchemeContentEntity.SchemeContent,
                    frmData = frmData
                };
                wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();

                wfProcessInstanceService.SaveProcess(processId.ToString(),wfProcessInstanceEntity, wfProcessSchemeEntity);

                return true;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个草稿
        /// </summary>
        /// <param name="wfProcessInstanceEntity"></param>
        /// <param name="frmData"></param>
        /// <returns></returns>
        public bool EditionRoughdraft(WFProcessInstanceEntity wfProcessInstanceEntity, string frmData = null)
        {
            try
            {
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.ProcessSchemeId);
                dynamic schemeContentJson = wfProcessSchemeEntity.SchemeContent.ToJson();//获取工作流模板内容的json对象;
                var data = new
                {
                    SchemeContent = schemeContentJson.SchemeContent.Value,
                    frmData = frmData
                };
                wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();
                wfProcessInstanceEntity.isFinish = 0;
                wfProcessInstanceEntity.CreateDate = DateTime.Now;
                wfProcessInstanceService.SaveProcess(wfProcessInstanceEntity.Id,wfProcessInstanceEntity, wfProcessSchemeEntity);
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 节点审核
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public bool NodeVerification(string processId, bool flag, string description = "")
        {
            bool _res = false;
            try
            {
                string _sqlstr="", _dbbaseId="";
                WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceService.GetEntity(processId);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.ProcessSchemeId);
                var wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(wfProcessSchemeEntity.SchemeInfoId);
                WFProcessOperationHistoryEntity wfProcessOperationHistoryEntity = new WFProcessOperationHistoryEntity();//操作记录
                WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null;//流转记录
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();//委托记录

                dynamic schemeContentJson = wfProcessSchemeEntity.SchemeContent.ToJson();//获取工作流模板内容的json对象;
                WF_RuntimeInitModel wfRuntimeInitModel = new WF_RuntimeInitModel()
                {
                    schemeContent = schemeContentJson.SchemeContent.Value,
                    currentNodeId = wfProcessInstanceEntity.ActivityId,
                    frmData = schemeContentJson.frmData.Value,
                    previousId = wfProcessInstanceEntity.PreviousId,
                    processId = processId
                };
                IWF_Runtime wfruntime = null;
                //if (wfProcessInstanceEntity.FrmType == 0)//自定义表单
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel);
                //}
                //else
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel, GetFrmData);
                //}
                wfruntime = new WF_Runtime(wfRuntimeInitModel);
                #region 会签
                if (wfProcessInstanceEntity.ActivityType == 0)//会签
                {
                    wfruntime.MakeTagNode(wfruntime.runtimeModel.currentNodeId, 1,"");//标记当前节点通过
                    ///寻找需要审核的节点Id
                    string _VerificationNodeId = "";
                    List<string> _nodelist = wfruntime.GetCountersigningNodeIdList(wfruntime.runtimeModel.currentNodeId);
                    string _makerList = "";
                    foreach (string item in _nodelist)
                    {
                        _makerList = GetMakerList(wfruntime.runtimeModel.nodeDictionary[item], wfruntime.runtimeModel.processId);
                        if (_makerList != "-1")
                        {
                            foreach (string one in _makerList.Split(','))
                            {
                                if (OperatorProvider.Provider.Current().UserId == one || OperatorProvider.Provider.Current().ObjectId.IndexOf(one) != -1)
                                {
                                    _VerificationNodeId = item;
                                    break;
                                }
                            }
                        }
                    }

                    if (_VerificationNodeId != "")
                    {
                        if (flag)
                        {
                            wfProcessOperationHistoryEntity.Content = "【" + OperatorProvider.Provider.Current().UserName + "】【" + wfruntime.runtimeModel.nodeDictionary[_VerificationNodeId].name + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】同意,备注：" + description;
                        }
                        else
                        {
                            wfProcessOperationHistoryEntity.Content = "【" + OperatorProvider.Provider.Current().UserName + "】【" + wfruntime.runtimeModel.nodeDictionary[_VerificationNodeId].name + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】不同意,备注：" + description;
                        }

                        string _Confluenceres = wfruntime.NodeConfluence(_VerificationNodeId, flag, OperatorProvider.Provider.Current().UserId, description);
                        var _data = new {
                            SchemeContent = wfruntime.runtimeModel.schemeContentJson.ToString(),
                            //frmData = (wfProcessInstanceEntity.FrmType == 0?wfruntime.runtimeModel.frmData:null)
                            frmData = wfSchemeInfoEntity.FrmType != 2 ? wfruntime.runtimeModel.frmData : null
                        };
                        wfProcessSchemeEntity.SchemeContent = _data.ToJson().ToString();
                        switch (_Confluenceres)
                        {
                            case "-1"://不通过
                                wfProcessInstanceEntity.isFinish = 3;
                                break;
                            case "1"://等待
                                break;
                            default://通过
                                wfProcessInstanceEntity.PreviousId = wfProcessInstanceEntity.ActivityId;
                                wfProcessInstanceEntity.ActivityId = wfruntime.runtimeModel.nextNodeId;
                                wfProcessInstanceEntity.ActivityType = wfruntime.runtimeModel.nextNodeType;//-1无法运行,0会签开始,1会签结束,2一般节点,4流程运行结束
                                wfProcessInstanceEntity.ActivityName = wfruntime.runtimeModel.nextNode.name;
                                wfProcessInstanceEntity.isFinish = (wfruntime.runtimeModel.nextNodeType == 4 ? 1 : 0);
                                wfProcessInstanceEntity.MakerList = (wfruntime.runtimeModel.nextNodeType == 4 ? GetMakerList(wfruntime) : "");//当前节点可执行的人信息
                               
                                #region 流转记录
                                processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                                processTransitionHistoryEntity.fromNodeId = wfruntime.runtimeModel.currentNodeId;
                                processTransitionHistoryEntity.fromNodeName = wfruntime.runtimeModel.currentNode.name.Value;
                                processTransitionHistoryEntity.fromNodeType = wfruntime.runtimeModel.currentNodeType;
                                processTransitionHistoryEntity.toNodeId = wfruntime.runtimeModel.nextNodeId;
                                processTransitionHistoryEntity.toNodeName = wfruntime.runtimeModel.nextNode.name.Value;
                                processTransitionHistoryEntity.toNodeType = wfruntime.runtimeModel.nextNodeType;
                                processTransitionHistoryEntity.TransitionSate = 0;
                                processTransitionHistoryEntity.isFinish = (processTransitionHistoryEntity.toNodeType == 4 ? 1 : 0);
                                #endregion

                                #region 委托记录
                                WFDelegateRecordEntity delegateRecordEntity = null;
                                DataTable dt = wfDelegateRuleService.GetEntityBySchemeInfoId(wfProcessSchemeEntity.SchemeInfoId, wfProcessInstanceEntity.MakerList.Split(','));
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (!string.IsNullOrEmpty(dr["Id"].ToString()))
                                    {
                                        delegateRecordEntity = new WFDelegateRecordEntity();
                                        delegateRecordEntity.WFDelegateRuleId = dr["Id"].ToString();
                                        delegateRecordEntity.FromUserId = dr["CreateUserId"].ToString();
                                        delegateRecordEntity.FromUserName = dr["CreateUserName"].ToString();
                                        delegateRecordEntity.ToUserId = dr["ToUserId"].ToString();
                                        delegateRecordEntity.ToUserName = dr["ToUserName"].ToString();

                                        delegateRecordEntity.ProcessCode = wfProcessInstanceEntity.Code;
                                        delegateRecordEntity.ProcessName = wfProcessInstanceEntity.CustomName;

                                        delegateRecordEntitylist.Add(delegateRecordEntity);

                                        wfProcessInstanceEntity.MakerList += "," + dr["ToUserId"].ToString();
                                    }
                                }
                                #endregion

                                if (wfruntime.runtimeModel.currentNode.setInfo != null && wfruntime.runtimeModel.currentNode.setInfo.NodeSQL != null)
                                {
                                    _sqlstr = wfruntime.runtimeModel.currentNode.setInfo.NodeSQL.Value;
                                    _dbbaseId = wfruntime.runtimeModel.currentNode.setInfo.NodeDataBaseToSQL.Value;
                                }
                                break;
                        }
                    }
                    else
                    {
                        throw(new Exception("审核异常,找不到审核节点"));
                    }
                }
                #endregion

                #region 一般审核
                else//一般审核
                {
                    if (flag)
                    {
                        wfruntime.MakeTagNode(wfruntime.runtimeModel.currentNodeId, 1, OperatorProvider.Provider.Current().UserId, description);
                        wfProcessInstanceEntity.PreviousId = wfProcessInstanceEntity.ActivityId;
                        wfProcessInstanceEntity.ActivityId = wfruntime.runtimeModel.nextNodeId;
                        wfProcessInstanceEntity.ActivityType = wfruntime.runtimeModel.nextNodeType;//-1无法运行,0会签开始,1会签结束,2一般节点,4流程运行结束
                        wfProcessInstanceEntity.ActivityName = wfruntime.runtimeModel.nextNode.name;
                        wfProcessInstanceEntity.MakerList = (wfruntime.runtimeModel.nextNodeType != 4 ? GetMakerList(wfruntime) : "");//当前节点可执行的人信息
                        wfProcessInstanceEntity.isFinish = (wfruntime.runtimeModel.nextNodeType == 4 ? 1 : 0);
                        #region 流转记录
                        processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                        processTransitionHistoryEntity.fromNodeId = wfruntime.runtimeModel.currentNodeId;
                        processTransitionHistoryEntity.fromNodeName = wfruntime.runtimeModel.currentNode.name.Value;
                        processTransitionHistoryEntity.fromNodeType = wfruntime.runtimeModel.currentNodeType;
                        processTransitionHistoryEntity.toNodeId = wfruntime.runtimeModel.nextNodeId;
                        processTransitionHistoryEntity.toNodeName = wfruntime.runtimeModel.nextNode.name.Value;
                        processTransitionHistoryEntity.toNodeType = wfruntime.runtimeModel.nextNodeType;
                        processTransitionHistoryEntity.TransitionSate = 0;
                        processTransitionHistoryEntity.isFinish = (processTransitionHistoryEntity.toNodeType == 4 ? 1 : 0);
                        #endregion

                        #region 委托记录
                        WFDelegateRecordEntity delegateRecordEntity = null;
                       
                        DataTable dt = wfDelegateRuleService.GetEntityBySchemeInfoId(wfProcessSchemeEntity.SchemeInfoId, wfProcessInstanceEntity.MakerList.Split(','));
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (!string.IsNullOrEmpty(dr["Id"].ToString()))
                            {
                                delegateRecordEntity = new WFDelegateRecordEntity();
                                delegateRecordEntity.WFDelegateRuleId = dr["Id"].ToString();
                                delegateRecordEntity.FromUserId = dr["CreateUserId"].ToString();
                                delegateRecordEntity.FromUserName = dr["CreateUserName"].ToString();
                                delegateRecordEntity.ToUserId = dr["ToUserId"].ToString();
                                delegateRecordEntity.ToUserName = dr["ToUserName"].ToString();

                                delegateRecordEntity.ProcessCode = wfProcessInstanceEntity.Code;
                                delegateRecordEntity.ProcessName = wfProcessInstanceEntity.CustomName;

                                delegateRecordEntitylist.Add(delegateRecordEntity);

                                wfProcessInstanceEntity.MakerList += "," + dr["ToUserId"].ToString();
                            }
                        }
                        #endregion

                        if (wfruntime.runtimeModel.currentNode.setInfo != null && wfruntime.runtimeModel.currentNode.setInfo.NodeSQL != null)
                        {
                            _sqlstr = wfruntime.runtimeModel.currentNode.setInfo.NodeSQL.Value;
                            _dbbaseId = wfruntime.runtimeModel.currentNode.setInfo.NodeDataBaseToSQL.Value;
                        }
                      
                        wfProcessOperationHistoryEntity.Content = "【" + OperatorProvider.Provider.Current().UserName + "】【" + wfruntime.runtimeModel.currentNode.name + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】同意,备注：" + description;
                    }
                    else
                    {
                        wfProcessInstanceEntity.isFinish = 3; //表示该节点不同意
                        wfruntime.MakeTagNode(wfruntime.runtimeModel.currentNodeId, -1, OperatorProvider.Provider.Current().UserId, description);

                        wfProcessOperationHistoryEntity.Content = "【" + OperatorProvider.Provider.Current().UserName + "】【" + wfruntime.runtimeModel.currentNode.name + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】不同意,备注：" + description;
                    }
                    var data = new
                    {
                        SchemeContent = wfruntime.runtimeModel.schemeContentJson.ToString(),
                        //frmData = (wfProcessInstanceEntity.FrmType == 0 ? wfruntime.runtimeModel.frmData : null)
                        frmData = wfSchemeInfoEntity.FrmType != 2 ? wfruntime.runtimeModel.frmData : null
                    };
                    wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();
                }
                #endregion 

                _res = true;
                wfProcessInstanceService.SaveProcess(_sqlstr, _dbbaseId,wfProcessInstanceEntity, wfProcessSchemeEntity, wfProcessOperationHistoryEntity, delegateRecordEntitylist, processTransitionHistoryEntity);
                return _res;
            }
            catch {
                throw;
            }
        }
        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="nodeId"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool NodeReject(string processId,string nodeId, string description = "")
        {
            try
            {
                WFProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceService.GetEntity(processId);
                WFProcessSchemeEntity wfProcessSchemeEntity = wfProcessSchemeService.GetEntity(wfProcessInstanceEntity.ProcessSchemeId);
                var wfSchemeInfoEntity = wfSchemeInfoService.GetEntity(wfProcessSchemeEntity.SchemeInfoId);
                WFProcessOperationHistoryEntity wfProcessOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null;
                dynamic schemeContentJson = wfProcessSchemeEntity.SchemeContent.ToJson();//获取工作流模板内容的json对象;
                WF_RuntimeInitModel wfRuntimeInitModel = new WF_RuntimeInitModel()
                {
                    schemeContent = schemeContentJson.SchemeContent.Value,
                    currentNodeId = wfProcessInstanceEntity.ActivityId,
                    frmData = schemeContentJson.frmData.Value,
                    previousId = wfProcessInstanceEntity.PreviousId,
                    processId = processId
                };
                IWF_Runtime wfruntime = null;
                //if (wfProcessInstanceEntity.FrmType == 0)//自定义表单
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel);
                //}
                //else
                //{
                //    wfruntime = new WF_Runtime(wfRuntimeInitModel, GetFrmData);
                //}
                wfruntime = new WF_Runtime(wfRuntimeInitModel);
                string resnode = "";
                if (nodeId == "")
                {
                    resnode = wfruntime.RejectNode();
                }
                else
                {
                    resnode = nodeId;
                }
                wfruntime.MakeTagNode(wfruntime.runtimeModel.currentNodeId, 0, OperatorProvider.Provider.Current().UserId, description);
                wfProcessInstanceEntity.isFinish = 4;//4表示驳回（需要申请者重新提交表单）
                if (resnode != "")
                {
                    wfProcessInstanceEntity.PreviousId = wfProcessInstanceEntity.ActivityId;
                    wfProcessInstanceEntity.ActivityId = resnode;
                    wfProcessInstanceEntity.ActivityType = wfruntime.GetNodeStatus(resnode);//-1无法运行,0会签开始,1会签结束,2一般节点,4流程运行结束
                    wfProcessInstanceEntity.ActivityName = wfruntime.runtimeModel.nodeDictionary[resnode].name;
                    wfProcessInstanceEntity.MakerList = GetMakerList(wfruntime.runtimeModel.nodeDictionary[resnode], wfProcessInstanceEntity.PreviousId);//当前节点可执行的人信息
                    #region 流转记录
                    processTransitionHistoryEntity = new WFProcessTransitionHistoryEntity();
                    processTransitionHistoryEntity.fromNodeId = wfruntime.runtimeModel.currentNodeId;
                    processTransitionHistoryEntity.fromNodeName = wfruntime.runtimeModel.currentNode.name.Value;
                    processTransitionHistoryEntity.fromNodeType = wfruntime.runtimeModel.currentNodeType;
                    processTransitionHistoryEntity.toNodeId = wfruntime.runtimeModel.nextNodeId;
                    processTransitionHistoryEntity.toNodeName = wfruntime.runtimeModel.nextNode.name.Value;
                    processTransitionHistoryEntity.toNodeType = wfruntime.runtimeModel.nextNodeType;
                    processTransitionHistoryEntity.TransitionSate = 1;//
                    processTransitionHistoryEntity.isFinish = (processTransitionHistoryEntity.toNodeType == 4 ? 1 : 0);
                    #endregion
                }
                var data = new
                {
                    SchemeContent = wfruntime.runtimeModel.schemeContentJson.ToString(),
                    //frmData = (wfProcessInstanceEntity.FrmType == 0 ? wfruntime.runtimeModel.frmData : null)
                    frmData = wfSchemeInfoEntity.FrmType != 2 ? wfruntime.runtimeModel.frmData : null
                };
                wfProcessSchemeEntity.SchemeContent = data.ToJson().ToString();
                wfProcessOperationHistoryEntity.Content = "【" + OperatorProvider.Provider.Current().UserName + "】【" + wfruntime.runtimeModel.currentNode.name + "】【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】驳回,备注：" + description;

                wfProcessInstanceService.SaveProcess(wfProcessInstanceEntity, wfProcessSchemeEntity, wfProcessOperationHistoryEntity,null, processTransitionHistoryEntity);
                return true;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 召回流程进程
        /// </summary>
        /// <param name="processId"></param>
        public void CallingBackProcess(string processId)
        {
            try
            {
                wfProcessInstanceService.OperateVirtualProcess(processId,2);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 终止一个实例(彻底删除)
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public void KillProcess(string processId)
        {
            try
            {
                wfProcessInstanceService.DeleteProcess(processId);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取某个节点（审核人所能看到的提交表单的权限）
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetProcessSchemeContentByNodeId(string data,string formid,string nodeId)
        {
            try
            {
                //List<dynamic> list = new List<dynamic>();
                //dynamic schemeContentJson = data.ToJson();//获取工作流模板内容的json对象;
                //string schemeContent1 = schemeContentJson.SchemeContent.Value;
                //dynamic schemeContentJson1 = schemeContent1.ToJson();

                //dynamic formJson = form.ToJson();
                ////string FrmContent = formJson.Value;
                ////dynamic FrmContentJson = FrmContent.ToJson();
                //dynamic FrmContentJson = formJson;
                //List<dynamic> list0 = new List<dynamic>();
                //foreach (var item in formJson.data)
                //{
                //    var myfield = item.fields;
                //    foreach (var item1 in myfield)
                //    {
                //        list0.Add(item1);
                //    }
                //}
                //FrmContentJson = list0;
                //foreach (var item in schemeContentJson1.Flow.nodes)
                //{
                //    if (item.id.Value == nodeId)
                //    {
                //        foreach (var item1 in item.setInfo.frmPermissionInfo)
                //        {
                //            foreach (var item2 in FrmContentJson)
                //            {
                //                if (item2.field.Value == item1.fieldid.Value)
                //                {
                //                    if (item1.look.Value == true)
                //                    {
                //                        if (item1.down != null)
                //                        {
                //                            item2.down = item1.down.Value;
                //                        }
                //                        list.Add(item2);
                //                    } 
                //                    break;
                //                }
                //            }
                //        }
                //        break;
                //    }
                //}
                //string myData = list.ToJson().ToString();

                //return myData;
                List<dynamic> list = new List<dynamic>();
                dynamic schemeContentJson = data.ToJson();//获取工作流模板内容的json对象;
                string schemeContent1 = schemeContentJson.SchemeContent.Value;
                dynamic schemeContentJson1 = schemeContent1.ToJson();
               
              

                foreach (var item in schemeContentJson1.Flow.nodes)
                {
                    if (item.id.Value == nodeId)
                    {
                        foreach (var item1 in item.setInfo.frmPermissionInfo)
                        {
                            item1.formId = formid;
                            list.Add(item1);
                        }
                        break;
                    }
                }

                return list.ToJson().ToString();
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取某个节点（审核人所能看到的提交表单的权限）
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetProcessSchemeContentByUserId(string data, string userId)
        {
            try
            {
                List<dynamic> list = new List<dynamic>();
                dynamic schemeContentJson = data.ToJson();//获取工作流模板内容的json对象;
                string schemeContent1 = schemeContentJson.SchemeContent.Value;
                dynamic schemeContentJson1 = schemeContent1.ToJson();
                string FrmContent = schemeContentJson1.Frm.FrmContent.Value;
                dynamic FrmContentJson = FrmContent.ToJson();

                foreach (var item in schemeContentJson1.Flow.nodes)
                {
                    if (item.setInfo != null && item.setInfo.UserId != null && item.setInfo.UserId.Value == userId)
                    {
                        foreach (var item1 in item.setInfo.frmPermissionInfo)
                        {
                            foreach (var item2 in FrmContentJson)
                            {
                                if (item2.control_field.Value == item1.fieldid.Value)
                                {
                                    if (item1.look.Value == true)
                                    {
                                        if (item1.down != null)
                                        {
                                            item2.down = item1.down.Value;
                                        }
                                        list.Add(item2);
                                    }
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
                schemeContentJson1.Frm.FrmContent = list.ToJson().ToString();
                schemeContentJson.SchemeContent = schemeContentJson1.ToString();
                return schemeContentJson.ToString();
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 工作流流程处理方法(公用)
        /// <summary>
        /// 寻找该节点执行人
        /// </summary>
        /// <param name="wfruntime"></param>
        /// <returns></returns>
        private string GetMakerList(IWF_Runtime wfruntime)
        {
            try
            {
                string makerList = "";
                if (wfruntime.runtimeModel.nextNodeId == "-1")
                {
                    throw (new Exception("无法寻找到下一个节点"));
                }
                if (wfruntime.runtimeModel.nextNodeType == 0)//如果是会签节点
                {
                    List<string> _nodelist = wfruntime.GetCountersigningNodeIdList(wfruntime.runtimeModel.nextNodeId);
                    string _makerList = "";
                    foreach (string item in _nodelist)
                    {
                        _makerList = GetMakerList(wfruntime.runtimeModel.nodeDictionary[item], wfruntime.runtimeModel.processId);
                        if (_makerList == "-1")
                        {
                            throw (new Exception("无法寻找到会签节点的审核者,请查看流程设计是否有问题!"));
                        }
                        else if (_makerList == "1")
                        {
                            throw (new Exception("会签节点的审核者不能为所有人,请查看流程设计是否有问题!"));
                        }
                        else
                        {
                            if (makerList != "")
                            {
                                makerList += ",";
                            }
                            makerList += _makerList;
                        }
                    }
                }
                else
                {
                    makerList = GetMakerList(wfruntime.runtimeModel.nextNode, wfruntime.runtimeModel.processId);
                    if (makerList == "-1")
                    {
                        throw (new Exception("无法寻找到节点的审核者,请查看流程设计是否有问题!"));
                    }
                }

                return makerList;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 寻找该节点执行人
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string GetMakerList(dynamic node, string processId)
        {
            try
            {
                string makerlsit = "";

                if (node.setInfo == null)
                {
                    makerlsit = "-1";
                }
                else
                {
                    if (node.setInfo.NodeDesignate.Value == "NodeDesignateType1")//所有成员
                    {
                        makerlsit = "1";
                    }
                    else if (node.setInfo.NodeDesignate.Value == "NodeDesignateType2")//指定成员
                    {
                        makerlsit = ArrwyToString(node.setInfo.NodeDesignateData.role, makerlsit);
                        makerlsit = ArrwyToString(node.setInfo.NodeDesignateData.post, makerlsit);
                        makerlsit = ArrwyToString(node.setInfo.NodeDesignateData.usergroup, makerlsit);
                        makerlsit = ArrwyToString(node.setInfo.NodeDesignateData.user, makerlsit);

                        if (makerlsit == "")
                        {
                            makerlsit = "-1";
                        }
                    }
                    else if (node.setInfo.NodeDesignate.Value == "NodeDesignateType3")//发起者领导
                    {
                        UserEntity userEntity = userService.GetEntity(OperatorProvider.Provider.Current().UserId);
                        if (string.IsNullOrEmpty(userEntity.ManagerId))
                        {
                            makerlsit = "-1";
                        }
                        else
                        {
                            makerlsit = userEntity.ManagerId;
                        }
                    }
                    else if (node.setInfo.NodeDesignate.Value == "NodeDesignateType4")//前一步骤领导
                    {
                        WFProcessTransitionHistoryEntity transitionHistoryEntity = wfProcessTransitionHistoryService.GetEntity(processId, node.id.Value);
                        UserEntity userEntity = userService.GetEntity(transitionHistoryEntity.CreateUserId);
                        if (string.IsNullOrEmpty(userEntity.ManagerId))
                        {
                            makerlsit = "-1";
                        }
                        else
                        {
                            makerlsit = userEntity.ManagerId;
                        }
                    }
                    else if (node.setInfo.NodeDesignate.Value == "NodeDesignateType5")//发起者部门领导
                    {
                        UserEntity userEntity = userService.GetEntity(OperatorProvider.Provider.Current().UserId);
                        DepartmentEntity departmentEntity = departmentService.GetEntity(userEntity.DepartmentId);

                        if (string.IsNullOrEmpty(departmentEntity.ManagerId))
                        {
                            makerlsit = "-1";
                        }
                        else
                        {
                            makerlsit = departmentEntity.ManagerId;
                        }
                    }
                    else if (node.setInfo.NodeDesignate.Value == "NodeDesignateType6")//发起者公司领导
                    {
                        UserEntity userEntity = userService.GetEntity(OperatorProvider.Provider.Current().UserId);
                        OrganizeEntity organizeEntity = organizeService.GetEntity(userEntity.OrganizeId);

                        if (string.IsNullOrEmpty(organizeEntity.ManagerId))
                        {
                            makerlsit = "-1";
                        }
                        else
                        {
                            makerlsit = organizeEntity.ManagerId;
                        }
                    }
                }
                return makerlsit;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 将数组转化成逗号相隔的字串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        private string ArrwyToString(dynamic data, string Str)
        {
            string resStr = Str;
            foreach (var item in data)
            {
                if (resStr != "")
                {
                    resStr += ",";
                }
                resStr += item.Value;
            }
            return resStr;
        }
        /// <summary>
        /// 获取系统表单信息
        /// </summary>
        /// <param name="DataBaseId"></param>
        /// <param name="tableName"></param>
        /// <param name="tableFiled"></param>
        /// <param name="processId"></param>
        /// <returns></returns>
        private string GetFrmData(string DataBaseId, string tableName, string tableFiled, string processId)
        {
            
            string res = "";
            try
            {
                if (DataBaseId == "" || DataBaseId == "" || DataBaseId == "")
                {
                    return "";
                }

                DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(DataBaseId);//获取

                string sqlstr = string.Format("Select * from {0} where {1} = '{2}' ", tableName, tableFiled, processId);
                DataTable dt = this.BaseRepository(dataBaseLinkEntity.DbConnection).FindTable(sqlstr);

                res = dt.ToJson();
                return res;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取委托记录列表
        /// </summary>
        /// <param name="wfSchemeInfoId"></param>
        /// <param name="code"></param>
        /// <param name="customName"></param>
        /// <param name="makerList"></param>
        /// <returns></returns>
        private List<WFDelegateRecordEntity> GetDelegateRecordList(string wfSchemeInfoId, string code, string customName, string makerList)
        {
            try
            {
                delegateUserList = "";
                WFDelegateRecordEntity delegateRecordEntity = null;
                List<WFDelegateRecordEntity> delegateRecordEntitylist = new List<WFDelegateRecordEntity>();
                DataTable dt = wfDelegateRuleService.GetEntityBySchemeInfoId(wfSchemeInfoId, makerList.Split(','));
                foreach (DataRow dr in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["Id"].ToString()))
                    {
                        delegateRecordEntity = new WFDelegateRecordEntity();
                        delegateRecordEntity.WFDelegateRuleId = dr["Id"].ToString();
                        delegateRecordEntity.FromUserId = dr["CreateUserId"].ToString();
                        delegateRecordEntity.FromUserName = dr["CreateUserName"].ToString();
                        delegateRecordEntity.ToUserId = dr["ToUserId"].ToString();
                        delegateRecordEntity.ToUserName = dr["ToUserName"].ToString();

                        delegateRecordEntity.ProcessCode = code;
                        delegateRecordEntity.ProcessName = customName;

                        delegateRecordEntitylist.Add(delegateRecordEntity);

                        delegateUserList += "," + dr["ToUserId"].ToString();
                    }
                }

                return delegateRecordEntitylist;
            }
            catch {
                throw;
            }
        }
        #endregion
    }
}
