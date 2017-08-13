using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Linq;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.01.14 11:02
    /// 描 述：工作流委托规则表操作类（支持：SqlServer）
    /// </summary>
    public class WFDelegateRuleService : RepositoryFactory, WFDelegateRuleIService
    {


        #region 获取数据
        /// <summary>
        /// 获取委托规则分页数据（不写委托人获取全部）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="userId">委托人</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson,string userId=null)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w1.Id,
	                                w1.ToUserId,
	                                w1.ToUserName,
	                                w1.BeginDate,
	                                w1.EndDate,
	                                w1.CreateUserId,
	                                w1.CreateUserName,
	                                w1.CreateDate,
	                                w1.Description,
                                    w1.EnabledMark,
	                                COUNT(w2.Id) as shcemeNum
                                FROM
	                                WF_DelegateRule w1
                                LEFT JOIN WF_DelegateRuleSchemeInfo w2 ON w2.DelegateRuleId = w1.Id
                                Where 1=1 
                               ");
                var parameter = new List<DbParameter>();
                var queryParam = queryJson.ToJObject();
                if (!string.IsNullOrEmpty(userId))
                {
                    strSql.Append(@" AND ( w1.CreateUserId = @CreateUserId )");
                    parameter.Add(DbParameters.CreateDbParameter("@CreateUserId",userId));
                }
                if (!queryParam["Keyword"].IsEmpty())//关键字查询
                {
                    string keyord = queryParam["Keyword"].ToString();
                    strSql.Append(@" AND ( w1.ToUserName LIKE @keyword  )");

                    parameter.Add(DbParameters.CreateDbParameter("@keyword", '%' + keyord + '%'));
                }
                strSql.Append(@" GROUP BY
	                                w1.Id,
	                                w1.ToUserId,
	                                w1.ToUserName,
	                                w1.BeginDate,
	                                w1.EndDate,
	                                w1.CreateUserId,
	                                w1.CreateUserName,
	                                w1.CreateDate,
                                    w1.EnabledMark,
	                                w1.Description ");

                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray(), pagination);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取流程模板信息列表数据
        /// </summary>
        /// <param name="ruleId">委托规则Id</param>
        /// <returns></returns>
        public DataTable GetSchemeInfoList(string ruleId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                w.Id,
	                                w.SchemeCode,
	                                w.SchemeName,
	                                w.SchemeType,
	                                t2.ItemName AS SchemeTypeName,
	                                w.SortCode,
	                                w.DeleteMark,
	                                w.EnabledMark,
	                                w.Description,
	                                w.CreateDate,
	                                w.CreateUserId,
	                                w.CreateUserName,
	                                w.ModifyDate,
	                                w.ModifyUserId,
	                                w.ModifyUserName,
	                                CASE
                                WHEN t3.Id IS NOT NULL THEN
	                                '1'
                                ELSE
	                                '0'
                                END AS ischecked
                                FROM
	                                WF_SchemeInfo w
                                LEFT JOIN Base_DataItemDetail t2 ON t2.ItemDetailId = w.SchemeType
                                LEFT JOIN WF_DelegateRuleSchemeInfo t3 ON t3.SchemeInfoId = w.Id and t3.DelegateRuleId = @ruleId
                                WHERE
	                                w.DeleteMark = 0
                                AND w.EnabledMark = 1
                                ORDER BY
	                                w.SchemeType,
	                                w.SchemeCode");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@ruleId", string.IsNullOrEmpty(ruleId) ? " " : ruleId));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据模板信息Id获取委托规则实体
        /// </summary>
        /// <param name="shcemeInfoId"></param>
        /// <returns></returns>
        public DataTable GetEntityBySchemeInfoId(string shcemeInfoId,string[] objectIdList)
        {
            try
            {
                IPermissionService service = new PermissionService();
                string userIdlist = "";
                foreach (string item in objectIdList)
                {
                    List<UserRelationEntity> list = service.GetMemberList(item).ToList();
                    foreach (var item1 in list)
                    {
                        if (userIdlist != "")
                        {
                            userIdlist += "','";
                        }
                        userIdlist += item1.UserId;
                    }
                    if (userIdlist != "")
                    {
                        userIdlist += "','";
                    }
                    userIdlist += item;
                }
                var strSql = new StringBuilder();
                strSql.Append(string.Format(@"SELECT
	                               	    w1.Id,
	                                    w1.ToUserId,
	                                    w1.ToUserName,
	                                    w1.BeginDate,
	                                    w1.EndDate,
	                                    w1.CreateDate,
	                                    w1.CreateUserId,
	                                    w1.CreateUserName,
	                                    w1.EnabledMark,
	                                    w1.Description
                                    FROM
	                                    WF_DelegateRule w1
                                    LEFT JOIN WF_DelegateRuleSchemeInfo w2 ON w2.DelegateRuleId = w1.Id
                                    WHERE
	                                    w1.EnabledMark = 1 AND w1.BeginDate <='{0}' AND w1.EndDate >='{0}' AND w1.CreateUserId in ('{1}')
                                   AND w2.SchemeInfoId = @SchemeInfoId
                                ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), userIdlist));
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@SchemeInfoId", shcemeInfoId));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取委托规则实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public WFDelegateRuleEntity GetEntity(string keyValue)
        {
            try
            {
               return this.BaseRepository().FindEntity<WFDelegateRuleEntity>(keyValue);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存委托规则
        /// </summary>
        /// <returns></returns>
        public int SaveDelegateRule(string keyValue,WFDelegateRuleEntity ruleEntity,string[] shcemeInfoIdlist)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    ruleEntity.Create();
                    db.Insert(ruleEntity);
                }
                else
                {
                    ruleEntity.Modify(keyValue);
                    db.Update(ruleEntity);
                }
                db.Delete<WFDelegateRuleSchemeInfoEntity>(ruleEntity.Id, "DelegateRuleId");
                foreach (string item in shcemeInfoIdlist)
                {
                    WFDelegateRuleSchemeInfoEntity entity = new WFDelegateRuleSchemeInfoEntity();
                    entity.Create();
                    entity.DelegateRuleId = ruleEntity.Id;
                    entity.SchemeInfoId = item;
                    db.Insert(entity);
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
        /// 删除委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int DeleteRule(string keyValue)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<WFDelegateRuleEntity>(keyValue);
                db.Delete<WFDelegateRuleSchemeInfoEntity>(keyValue, "DelegateRuleId");
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
        /// 使能委托规则
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="enableMark"></param>
        /// <returns></returns>
        public int UpdateRuleEnable(string keyValue, int enableMark)
        {
            try
            {
                WFDelegateRuleEntity entity = new WFDelegateRuleEntity();
                entity.Modify(keyValue);
                entity.EnabledMark = enableMark;
                this.BaseRepository().Update(entity);

                return 1;
            }
            catch {
                throw;
            }
        }
        #endregion

    }
}
