using LeaRun.Application.Entity.FlowManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.FlowManage
{
    public class WFProcessTransitionHistoryMap : EntityTypeConfiguration<WFProcessTransitionHistoryEntity>
    {
        /// <summary>
        /// 版 本
        /// Copyright (c) 2013-2016 聚久信息技术有限公司
        /// 创建人：陈彬彬
        /// 日 期：2016.03.18 09:58
        /// 描 述：工作流实例节点转化记录表
        /// </summary>
        public WFProcessTransitionHistoryMap()
        {
            #region 表、主键
            //表
            this.ToTable("WF_ProcessTransitionHistory");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
