using LeaRun.Application.Entity.WeChatManage;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号部门
    /// </summary>
    public class WeChatDeptRelationMap : EntityTypeConfiguration<WeChatDeptRelationEntity>
    {
        public WeChatDeptRelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("WeChat_DeptRelation");
            //主键
            this.HasKey(t => t.DeptId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
