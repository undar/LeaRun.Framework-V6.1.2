using System;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流委托记录表
    /// </summary>
    public class WFDelegateRecordEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 委托规则Id
        /// </summary>
        public string WFDelegateRuleId { get; set; }
        /// <summary>
        /// 委托人Id
        /// </summary>
        public string FromUserId { get; set; }
        /// <summary>
        /// 委托人
        /// </summary>		
        public string FromUserName { get; set; }
        /// <summary>
        /// 被委托人Id
        /// </summary>		
        public string ToUserId { get; set; }
        /// <summary>
        /// 被委托人名称
        /// </summary>		
        public string ToUserName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 流程实例Id
        /// </summary>		
        public string ProcessId { get; set; }
        /// <summary>
        /// 实例编号
        /// </summary>		
        public string ProcessCode { get; set; }
        /// <summary>
        /// 实例自定义名称
        /// </summary>
        public string ProcessName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;        }
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
