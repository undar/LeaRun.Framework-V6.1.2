using System;

namespace LeaRun.Application.Entity.MessageManage
{
	/// <summary>
    /// 未读消息表
	/// <author>
    ///		<name>she</name>
    ///		<date>2015.08.01 14:00</date>
    /// </author>
    /// </summary>
    public class IMReadEntity : BaseEntity
	{
		#region 获取/设置 字段值
      	/// <summary>
		/// 未读消息主键
        /// </summary>		
        public string ReadId { get; set; }    
		/// <summary>
		/// 消息主键
        /// </summary>		
        public string ContentId { get; set; }    
		/// <summary>
		/// 用户主键
        /// </summary>		
        public string UserId { get; set; }    
		/// <summary>
		/// 发送者ID
        /// </summary>		
        public string SendId { get; set; }
        /// <summary>
        /// 发送状态（0-未送达，1-已送达，2-已阅读）
        /// </summary>		
        public int ReadStatus { get; set; }    
		/// <summary>
		/// 创建时间
        /// </summary>		
        public DateTime CreateDate { get; set; }    
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
            this.ReadId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ReadId = keyValue;
        }
        #endregion
	}
}