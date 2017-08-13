using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using LeaRun.Data.Repository;
using System;
using System.Data;
using LeaRun.Util.Extension;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.19 13:57
    /// 描 述：工作流流程模板操作（支持：SqlServer）
    /// </summary>
    public class WFSchemeInfoBLL
    {
        private WFSchemeInfoIService infoserver = new WFSchemeInfoService();
        private WFSchemeContentIService schemeserver = new WFSchemeContentService();

        #region 获取数据
        /// <summary>
        /// 获取流程列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            return infoserver.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取流程列表数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetList(string queryJson)
        {
            return infoserver.GetList(queryJson);
        }
        /// <summary>
        /// 获取所有表单数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            return infoserver.GetList();
        }
        /// <summary>
        /// 获取工作流模板对象列表
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        public IEnumerable<WFSchemeContentEntity> GetSchemeEntityList(string wfSchemeInfoId)
        {
            return schemeserver.GetEntityList(wfSchemeInfoId);
        }
          /// <summary>
        /// 获取对象列表（不包括模板内容）
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        public DataTable GetTableList(string wfSchemeInfoId)
        {
            return schemeserver.GetTableList(wfSchemeInfoId);
        }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WFSchemeInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return infoserver.GetEntity(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取工作流模板对象
        /// </summary>
        /// <param name="contentid"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public WFSchemeContentEntity GetSchemeEntity(string wfSchemeInfoId, string schemeVersion)
        {
            try
            {
                return schemeserver.GetEntity(wfSchemeInfoId, schemeVersion);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取权限列表数据
        /// </summary>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        public IEnumerable<WFSchemeInfoAuthorizeEntity> GetAuthorizeEntityList(string schemeInfoId)
        {
            return infoserver.GetAuthorizeEntityList(schemeInfoId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除流程
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                WFSchemeInfoEntity entity = db.FindEntity<WFSchemeInfoEntity>(keyValue);
                db.Delete<WFSchemeInfoEntity>(keyValue);
                var expression = LinqExtensions.True<WFSchemeContentEntity>();
                expression = expression.And(t => t.WFSchemeInfoId == entity.Id);

                db.Delete<WFSchemeContentEntity>(expression);
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存流程
        /// </summary>
        /// <param name="entity">表单模板实体类</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, WFSchemeInfoEntity entity, WFSchemeContentEntity modelentity, string[] shcemeAuthorizeData)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    entity.SchemeVersion = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    entity.SchemeContent = modelentity.SchemeContent;
                    db.Insert<WFSchemeInfoEntity>(entity);

                    modelentity.Create();
                    modelentity.WFSchemeInfoId = entity.Id;
                    modelentity.SchemeVersion = entity.SchemeVersion;
                    db.Insert<WFSchemeContentEntity>(modelentity);
                }
                else
                {
                    WFSchemeContentEntity modelentityold = schemeserver.GetEntity(keyValue, entity.SchemeVersion);
                    if (modelentityold.SchemeContent != modelentity.SchemeContent)
                    {
                        if (modelentity.SchemeVersion == "cg")
                        {
                            modelentityold.SchemeContent = modelentity.SchemeContent;
                            modelentityold.SchemeVersion = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            modelentity.SchemeVersion = modelentityold.SchemeVersion;
                            db.Update<WFSchemeContentEntity>(modelentityold);
                        }
                        else
                        {
                            modelentity.Create();
                            modelentity.WFSchemeInfoId = keyValue;
                            modelentity.SchemeVersion = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            db.Insert<WFSchemeContentEntity>(modelentity);
                        }
                    }
                    else
                    {
                        modelentity.SchemeVersion = modelentityold.SchemeVersion;
                    }
                    entity.Modify(keyValue);
                    entity.SchemeContent = modelentity.SchemeContent;
                    entity.SchemeVersion = modelentity.SchemeVersion;
                    db.Update<WFSchemeInfoEntity>(entity);
                }

                db.Delete<WFSchemeInfoAuthorizeEntity>(entity.Id, "SchemeInfoId");
                foreach (string item in shcemeAuthorizeData)
                {
                    if (item != "")
                    {
                        WFSchemeInfoAuthorizeEntity _authorizeEntity = new WFSchemeInfoAuthorizeEntity();
                        _authorizeEntity.Create();
                        _authorizeEntity.SchemeInfoId = entity.Id;
                        _authorizeEntity.ObjectId = item;
                        db.Insert(_authorizeEntity);
                    }
                }
                db.Commit();
                return 1;
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 更新流程模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                infoserver.UpdateState(keyValue, state);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
