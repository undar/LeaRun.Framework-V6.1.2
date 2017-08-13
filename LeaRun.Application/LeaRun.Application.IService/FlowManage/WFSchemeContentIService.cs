using LeaRun.Application.Entity.FlowManage;
using System.Collections.Generic;
using System.Data;

namespace LeaRun.Application.IService.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.04 16:58
    /// 描 述：工作流模板内容表操作接口（支持：SqlServer）
    /// </summary>
    public interface WFSchemeContentIService
    {
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="contentid">关联id</param>
        /// <param name="version">模板版本号</param>
        /// <returns></returns>
        WFSchemeContentEntity GetEntity(string contentid, string version);
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        IEnumerable<WFSchemeContentEntity> GetEntityList(string wfSchemeInfoId);
        /// <summary>
        /// 获取对象列表（不包括模板内容）
        /// </summary>
        /// <param name="wfSchemeInfoId">工作流模板信息表Id</param>
        /// <returns></returns>
        DataTable GetTableList(string wfSchemeInfoId);
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        int SaveEntity(WFSchemeContentEntity entity, string keyValue);
    }
}
