using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流实例流转历史记录
    /// </summary>
    public class WFProcessTransitionHistoryEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 实例进程ID
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 开始节点Id
        /// </summary>
        public string fromNodeId { get; set; }
        /// <summary>
        /// 开始节点类型
        /// </summary>
        public int? fromNodeType { get; set; }
        /// <summary>
        /// 开始节点名称
        /// </summary>
        public string fromNodeName { get; set; }
        /// <summary>
        /// 结束节点Id
        /// </summary>
        public string toNodeId { get; set; }
        /// <summary>
        /// 结束节点类型
        /// </summary>
        public int? toNodeType { get; set; }
        /// <summary>
        /// 结束节点名称
        /// </summary>
        public string toNodeName { get; set; }
        /// <summary>
        /// 转化状态0正常，1驳回
        /// </summary>
        public int? TransitionSate { get; set; }
        /// <summary>
        /// 是否结束
        /// </summary>
        public int? isFinish { get; set; }
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
       
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
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
