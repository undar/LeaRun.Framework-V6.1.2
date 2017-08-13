using System;

namespace LeaRun.Application.Entity.MessageManage
{
	/// <summary>
    /// 即时消息表
	/// <author>
    ///		<name>she</name>
    ///		<date>2015.08.01 14:00</date>
    /// </author>
    /// </summary>
    public class IMContentEntity : BaseEntity
	{
		#region 获取/设置 字段值
      	/// <summary>
		/// 消息主键
        /// </summary>		
        public string ContentId { get; set; }    
		/// <summary>
		/// 是否是群组消息
        /// </summary>		
        public int IsGroup { get; set; }    
		/// <summary>
		/// 发送者ID
        /// </summary>		
        public string SendId { get; set; }    
		/// <summary>
		/// 接收者ID
        /// </summary>		
        public string ToId { get; set; }    
		/// <summary>
		/// 消息内容
        /// </summary>		
        public string MsgContent { get; set; }    
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
            this.ContentId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ContentId = keyValue;
        }
        #endregion
	}
}