
using LeaRun.Application.Code;
using System;
namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例表
    /// </summary>
    public class WFProcessInstanceEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 实例名称
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 自定定义标题
        /// </summary>
        public string CustomName { get; set;}
        /// <summary>
        /// 当前节点ID
        /// </summary>		
        public string ActivityId { get; set; }
        /// <summary>
        /// 获取节点类型 0会签开始,1会签结束,2一般节点,开始节点,4流程运行结束
        /// </summary>
        public int? ActivityType { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>		
        public string ActivityName { get; set; }
        /// <summary>
        /// 上一个节点Id
        /// </summary>
        public string PreviousId { get; set; }
        /// <summary>
        /// 流程实例模板Id
        /// </summary>		
        public string ProcessSchemeId { get; set; }
        /// <summary>
        /// 执行人
        /// </summary>
        public string MakerList { get; set; }
        /// <summary>
        /// 实例类型
        /// </summary>
        public string SchemeType { get; set; }
       
        /// <summary>
        /// 有效标志（0暂停,1正常运行,3草稿）
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        public int? wfLevel { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 流程是否完成(0运行中,1运行结束,2被召回,3不同意,4表示被驳回)
        /// </summary>
        public int? isFinish { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
