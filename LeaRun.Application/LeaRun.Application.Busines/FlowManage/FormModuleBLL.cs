using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Application.Service.FlowManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using LeaRun.Data.Repository;

namespace LeaRun.Application.Busines.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleBLL
    {
        private FormModuleIService service = new FormModuleService();
        private FormModuleContentIService contentservice = new FormModuleContentService();

        #region 获取数据
        public IEnumerable<FormModuleEntity> GetList()
        {
            return service.GetList();
        }
        public DataTable GetAllList()
        {
            return service.GetAllList();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<FormModuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FormModuleEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
       
    
     
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FormModuleEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            FormModuleContentEntity modelentity = new FormModuleContentEntity();
          
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //修改的时候判断下版本。同时修改日期，修改人处理下
                    FormModuleEntity entity1 = GetEntity(keyValue);
                    modelentity.FrmVersion = entity1.Version;
                    if (entity.EnabledMark==3)
                    {
                        modelentity.FrmVersion = "cg";
                    }
                    modelentity.FrmId = keyValue;
                    modelentity.FrmContent = entity.FrmContent;
                    FormModuleContentEntity modelentityold = contentservice.GetEntity(keyValue, entity1.Version);
                    if (modelentityold.FrmContent != modelentity.FrmContent)
                    {
                        if (modelentity.FrmVersion == "cg")
                        {
                            modelentityold.FrmContent = modelentity.FrmContent;
                            modelentityold.FrmVersion = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            modelentity.FrmVersion = modelentityold.FrmVersion;
                            db.Update<FormModuleContentEntity>(modelentityold);
                        }
                        else
                        {
                            modelentity.Create();
                            modelentity.FrmId = keyValue;
                            modelentity.FrmVersion = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            db.Insert<FormModuleContentEntity>(modelentity);
                        }
                    }
                    else
                    {
                        modelentity.FrmVersion = modelentityold.FrmVersion;
                    }
                    entity.Modify(keyValue);
                    entity.Version = modelentity.FrmVersion;
                    db.Update<FormModuleEntity>(entity);
                }
                else
                {
                    //新增
                    //主表
                    entity.Create();
                    entity.Version = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                    db.Insert(entity);
                    //明细
                    modelentity.Create();
                    modelentity.FrmVersion = entity.Version;
                    modelentity.FrmId = entity.FrmId;
                    modelentity.FrmContent = entity.FrmContent;
                    db.Insert(modelentity);


                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 更新表单模板状态（启用，停用）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="status">状态 1:启用;0.停用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                service.UpdateState(keyValue, state);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
