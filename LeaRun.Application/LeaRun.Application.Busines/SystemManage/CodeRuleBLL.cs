using LeaRun.Application.Entity.SystemManage;
using LeaRun.Application.IService.SystemManage;
using LeaRun.Application.Service.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleBLL
    {
        private ICodeRuleService service = new CodeRuleService();

        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CodeRuleEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            return service.ExistEnCode(enCode, keyValue);
        }
        /// <summary>
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            return service.ExistFullName(fullName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
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
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, CodeRuleEntity codeRuleEntity)
        {
            try
            {
                //调用单据编码示例
                //codeRuleEntity.Description = service.GetBillCode(OperatorProvider.Provider.Current().UserId, "", "001");
                service.SaveForm(keyValue, codeRuleEntity);
                //service.UseRuleSeed(OperatorProvider.Provider.Current().UserId, "", "001");
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string GetBillCode(string userId, string moduleId, string enCode = "")
        {
            return service.GetBillCode(userId, moduleId, enCode);
        }
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>true/false</returns>
        public bool UseRuleSeed(string userId, string moduleId, string enCode, IRepository db = null)
        {
            return service.UseRuleSeed(userId, moduleId, enCode, db);
        }
        /// <summary>
        /// 获得指定模块或者编号的单据号（直接使用）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        public string SetBillCode(string userId, string moduleId, string enCode, IRepository db = null)
        {
            return service.SetBillCode(userId, moduleId, enCode, db);
        }
        #endregion
    }
}
