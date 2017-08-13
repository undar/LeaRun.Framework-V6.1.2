using LeaRun.Application.Entity.FlowManage;
using LeaRun.Application.IService.FlowManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;

namespace LeaRun.Application.Service.FlowManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例节点转化记录操作（支持：SqlServer）
    /// </summary>
    public class WFProcessTransitionHistoryService : RepositoryFactory, WFProcessTransitionHistoryIService
    {
        #region 获取数据
        /// <summary>
        /// 获取流转实体
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="toNodeId">流转到的节点Id</param>
        /// <returns></returns>
        public WFProcessTransitionHistoryEntity GetEntity(string processId,string toNodeId)
        {
            try
            {
                var Expression = LinqExtensions.True<WFProcessTransitionHistoryEntity>();
                Expression = Expression.And<WFProcessTransitionHistoryEntity>(t => t.ProcessId == processId);
                Expression = Expression.And<WFProcessTransitionHistoryEntity>(t => t.toNodeId == toNodeId);

                return this.BaseRepository().FindEntity<WFProcessTransitionHistoryEntity>(Expression);
            }
            catch {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存实例
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, WFProcessTransitionHistoryEntity entity)
        {
            try
            {
                int num;
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    num = this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    num = this.BaseRepository().Update(entity);
                }
                return num;
            }
            catch
            {
                throw;
            }
        }

        #endregion
    }
}
