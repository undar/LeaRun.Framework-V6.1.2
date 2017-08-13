using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件收件人
    /// </summary>
    public class EmailAddresseeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 邮箱收件人主键
        /// </summary>		
        public string AddresseeId { get; set; }
        /// <summary>
        /// 邮件信息主键
        /// </summary>		
        public string ContentId { get; set; }
        /// <summary>
        /// 邮件分类主键
        /// </summary>		
        public string CategoryId { get; set; }
        /// <summary>
        /// 收件人Id
        /// </summary>		
        public string RecipientId { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>		
        public string RecipientName { get; set; }
        /// <summary>
        /// 收件状态（0-收件1-抄送2-密送）
        /// </summary>		
        public int? RecipientState { get; set; }
        /// <summary>
        /// 是否阅读
        /// </summary>		
        public int? IsRead { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>		
        public int? ReadCount { get; set; }
        /// <summary>
        /// 最后阅读日期
        /// </summary>		
        public DateTime? ReadDate { get; set; }
        /// <summary>
        /// 设置红旗
        /// </summary>		
        public int? IsHighlight { get; set; }
        /// <summary>
        /// 设置待办
        /// </summary>		
        public int? Backlog { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
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
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.AddresseeId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.ReadCount = 0;
            this.IsRead = 0;
        }
        #endregion
    }
}