using LeaRun.Application.Code;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.FlowWork;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例表操作
    /// </summary>
    public class WFProcessInstanceService : RepositoryFactory, WFProcessInstanceIService
    {
        private IDataBaseLinkService dataBaseLinkService = new DataBaseLinkService();

        #region 获取数据
        /// <summary>
        /// 获取流程监控数据（用于流程监控）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try{
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
                                    w.CustomName,
                                    w.wfLevel,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    w.Description,
                                    w.isFinish
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN
                                    WF_ProcessScheme w1 ON w1.Id = w.ProcessSchemeId
                                LEFT JOIN
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                WHERE w.EnabledMark != 3 ");//3表示草稿
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["WFSchemeInfoId"].IsEmpty())
                {
                    strSql.Append(" AND w1.SchemeInfoId = @WFSchemeInfoId ");
                    parameter.Add(DbParameters.CreateDbParameter("@WFSchemeInfoId", queryParam["WFSchemeInfoId"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch {
                throw;
            }
        }
        /// <summary>
        /// 获取流程实例分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="type">3草稿</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson,string type)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
                                    w.CustomName,
                                    w.wfLevel,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    w.Description,
                                    w.isFinish
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN 
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType");//3表示草稿
                if (type == "3")
                {
                    strSql.Append(@" WHERE w.EnabledMark = 3 AND w.isFinish != 2 ");
                }
                else
                {
                    strSql.Append(@" WHERE w.EnabledMark != 3 AND w.isFinish != 2 ");
                }

                if (!OperatorProvider.Provider.Current().IsSystem)
                {
                    strSql.Append(string.Format(" AND w.CreateUserId = '{0}' ", OperatorProvider.Provider.Current().UserId ));
                }

                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");
                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取登录者需要处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeBeforePageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
                                    w.Code,
									w.CustomName,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
                                    w.SchemeType,
                                    t2.ItemName AS SchemeTypeName,
                                    w.MakerList,
                                    w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
                                    w.isFinish,
                                    w.Description,
                                    w.wfLevel
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN 
	                                Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                WHERE w.EnabledMark = 1 AND w.isFinish = 0 ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(string.Format(@" AND ( w.MakerList LIKE '{0}' or  w.MakerList = '1' ", OperatorProvider.Provider.Current().UserId));
                foreach (var objectid in OperatorProvider.Provider.Current().ObjectId.Split(','))
                {
                    strSql.Append(string.Format(@" or w.MakerList LIKE '{0}' ", objectid ));
                }
                strSql.Append(")");
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取登录者已经处理的流程
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetToMeAfterPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(string.Format(@"SELECT
	                                w.Id,
	                                w.Code,
									w.CustomName,
	                                w.ActivityId,
	                                w.ActivityName,
	                                w.ActivityType,
	                                w.ProcessSchemeId,
	                                w.SchemeType,
	                                t2.ItemName AS SchemeTypeName,
	                                w.MakerList,
	                                w.EnabledMark,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
	                                w2.Content,
                                    w.isFinish,
                                    w.Description,
                                    w.wfLevel
                                FROM
	                                WF_ProcessInstance w
                                LEFT JOIN Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                LEFT JOIN WF_ProcessOperationHistory w2 ON w2.ProcessId = w.Id
                                WHERE
	                                w.EnabledMark = 1 AND w2.CreateUserId = '{0}' And w.CreateUserId != '{0}' ", OperatorProvider.Provider.Current().UserId));
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();

                if (!queryParam["SchemeType"].IsEmpty())
                {
                    strSql.Append(" AND w.SchemeType = @SchemeType ");
                    parameter.Add(DbParameters.CreateDbParameter("@SchemeType", queryParam["SchemeType"].ToString()));
                }
                else if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w.Code LIKE @keyword 
                                        or w.CustomName LIKE @keyword 
                                        or w.CreateUserName LIKE @keyword 
                    )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取实例进程信息实体
        /// </summary>
        /// <param name="keyVlaue"></param>
        /// <returns></returns>
        public WFProcessInstanceEntity GetEntity(string keyVlaue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyVlaue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 存储工作流实例进程(编辑草稿用)
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="wfOperationHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess(string processId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity wfOperationHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(processInstanceEntity.Id))
                {
                    processSchemeEntity.Create();
                    db.Insert(processSchemeEntity);

                    processInstanceEntity.Create();
                    processInstanceEntity.Id = processId;
                    processInstanceEntity.ProcessSchemeId = processSchemeEntity.Id;
                    db.Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processId);
                    db.Update(processInstanceEntity);

                    processSchemeEntity.Modify(processInstanceEntity.ProcessSchemeId);
                    db.Update(processSchemeEntity);
                }
                if (wfOperationHistoryEntity != null)
                {
                    wfOperationHistoryEntity.Create();
                    wfOperationHistoryEntity.ProcessId = processId;
                    db.Insert(wfOperationHistoryEntity);
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 存储工作流实例进程(创建实例进程)
        /// </summary>
        /// <param name="wfRuntimeModel"></param>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="delegateRecordEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WF_RuntimeModel wfRuntimeModel, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, WFProcessTransitionHistoryEntity processTransitionHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(processInstanceEntity.Id))
                {
                    processSchemeEntity.Create();
                    db.Insert(processSchemeEntity);

                    processInstanceEntity.Create();
                    processInstanceEntity.Id = wfRuntimeModel.processId;
                    processInstanceEntity.ProcessSchemeId = processSchemeEntity.Id;
                    db.Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processInstanceEntity.Id);
                    db.Update(processSchemeEntity);
                    db.Update(processInstanceEntity);
                }
                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                foreach (var item in delegateRecordEntityList)
                {
                    item.Create();
                    item.ProcessId = processInstanceEntity.Id;
                    db.Insert(item);
                }
                //if (processInstanceEntity.FrmType == 0)
                //{
                //    DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(wfRuntimeModel.schemeContentJson.Frm.FrmDbId.Value);//获取
                //    if (wfRuntimeModel.schemeContentJson.Frm.isSystemTable.Value != 0)
                //    {
                //        this.BaseRepository(dataBaseLinkEntity.DbConnection).ExecuteBySql(wfRuntimeModel.sqlFrm);
                //    }
                //}
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 存储工作流实例进程（审核驳回重新提交）
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <param name="processSchemeEntity"></param>
        /// <param name="processOperationHistoryEntity"></param>
        /// <param name="processTransitionHistoryEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                processInstanceEntity.Modify(processInstanceEntity.Id);
                db.Update(processSchemeEntity);
                db.Update(processInstanceEntity);

                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                if (delegateRecordEntityList != null)
                {
                    foreach (var item in delegateRecordEntityList)
                    {
                        item.Create();
                        item.ProcessId = processInstanceEntity.Id;
                        db.Insert(item);
                    }
                }
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
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
        public int SaveProcess(string sql,string dbbaseId, WFProcessInstanceEntity processInstanceEntity, WFProcessSchemeEntity processSchemeEntity, WFProcessOperationHistoryEntity processOperationHistoryEntity, List<WFDelegateRecordEntity> delegateRecordEntityList, WFProcessTransitionHistoryEntity processTransitionHistoryEntity = null)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                processInstanceEntity.Modify(processInstanceEntity.Id);
                db.Update(processSchemeEntity);
                db.Update(processInstanceEntity);

                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = processInstanceEntity.Id;
                db.Insert(processOperationHistoryEntity);

                if (processTransitionHistoryEntity != null)
                {
                    processTransitionHistoryEntity.Create();
                    processTransitionHistoryEntity.ProcessId = processInstanceEntity.Id;
                    db.Insert(processTransitionHistoryEntity);
                }
                if (delegateRecordEntityList != null)
                {
                    foreach (var item in delegateRecordEntityList)
                    {
                        item.Create();
                        item.ProcessId = processInstanceEntity.Id;
                        db.Insert(item);
                    }
                }
                //if (!string.IsNullOrEmpty(dbbaseId) && !string.IsNullOrEmpty(sql))//测试环境不允许执行sql语句
                //{
                //    DataBaseLinkEntity dataBaseLinkEntity = dataBaseLinkService.GetEntity(dbbaseId);//获取
                //    this.BaseRepository(dataBaseLinkEntity.DbConnection).ExecuteBySql(sql.Replace("{0}", processInstanceEntity.Id));
                //}
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存工作流进程实例
        /// </summary>
        /// <param name="processInstanceEntity"></param>
        /// <returns></returns>
        public int SaveProcess(WFProcessInstanceEntity processInstanceEntity)
        {
            try
            {
                int num;
                WFProcessInstanceEntity isExistEntity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(processInstanceEntity.Id);
                if (isExistEntity == null)
                {
                    processInstanceEntity.Create();
                    num = this.BaseRepository().Insert(processInstanceEntity);
                }
                else
                {
                    processInstanceEntity.Modify(processInstanceEntity.Id);
                    num = this.BaseRepository().Update(processInstanceEntity);
                }
                return num;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除工作流实例进程(删除草稿使用)
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int DeleteProcess(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                db.Delete<WFProcessSchemeEntity>(entity.ProcessSchemeId);
                db.Delete<WFProcessInstanceEntity>(keyValue);
                db.Commit();
                return 1;
            }
            catch {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 虚拟操作实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="state">0暂停,1启用,2取消（召回）</param>
        /// <returns></returns>
        public int OperateVirtualProcess(string keyValue,int state)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                WFProcessInstanceEntity entity = this.BaseRepository().FindEntity<WFProcessInstanceEntity>(keyValue);
                if (entity.isFinish == 1)
                {
                    throw new Exception("实例已经审核完成,操作失败");
                }
                else if (entity.isFinish == 2)
                {
                    throw new Exception("实例已经取消,操作失败");
                }
                /// 流程是否完成(0运行中,1运行结束,2被召回,3不同意,4表示被驳回)
                string content = "";
                switch (state)
                {
                    case 0:
                        if (entity.EnabledMark == 0)
                        {
                            return 1;
                        }
                        entity.EnabledMark = 0;
                        content = "【暂停】" + OperatorProvider.Provider.Current().UserName + "暂停了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                    case 1:
                        if (entity.EnabledMark == 1)
                        {
                            return 1;
                        }
                        entity.EnabledMark = 1;
                        content = "【启用】" + OperatorProvider.Provider.Current().UserName + "启用了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                    case 2:
                        entity.isFinish = 2;
                        content = "【召回】" + OperatorProvider.Provider.Current().UserName + "召回了一个流程进程【" + entity.Code + "/" + entity.CustomName + "】";
                        break;
                }
                db.Update<WFProcessInstanceEntity>(entity);
                WFProcessOperationHistoryEntity processOperationHistoryEntity = new WFProcessOperationHistoryEntity();
                processOperationHistoryEntity.Create();
                processOperationHistoryEntity.ProcessId = entity.Id;
                processOperationHistoryEntity.Content = content;
                db.Insert(processOperationHistoryEntity);
                db.Commit();
                return 1;
            }
            catch
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 流程指派
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="makeLists"></param>
        public void DesignateProcess(string processId, string makeLists)
        {
            try
            {
                WFProcessInstanceEntity entity = new WFProcessInstanceEntity();
                entity.Id = processId;
                entity.MakerList = makeLists;
                this.BaseRepository().Update(entity);
            }
            catch {
                throw;
            }
        }
        #endregion
    }
}
