using LeaRun.Application.Entity.SystemManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则
    /// </summary>
    public interface ICodeRuleService
    {
        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CodeRuleEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue);
        /// <summary>
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, CodeRuleEntity codeRuleEntity);
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        string GetBillCode(string userId, string moduleId, string enCode);
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>true/false</returns>
        bool UseRuleSeed(string userId, string moduleId, string enCode, IRepository db = null);
        /// <summary>
        /// 获得指定模块或者编号的单据号（直接使用）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="moduleId">模块ID</param>
        /// <param name="enCode">模板编码</param>
        /// <returns>单据号</returns>
        string SetBillCode(string userId, string moduleId, string enCode, IRepository db = null);
        #endregion
    }
}
