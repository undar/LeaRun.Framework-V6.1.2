using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.18 11:02
    /// 描 述：数据库连接管理
    /// </summary>
    public class DataBaseLinkEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 数据库连接主键
        /// </summary>		
        public string DatabaseLinkId { get; set; }
        /// <summary>
        /// 服务器地址
        /// </summary>		
        public string ServerAddress { get; set; }
        /// <summary>
        /// 数据库名称
        /// </summary>		
        public string DBName { get; set; }
        /// <summary>
        /// 数据库别名
        /// </summary>		
        public string DBAlias { get; set; }
        /// <summary>
        /// 数据库类型
        /// </summary>		
        public string DbType { get; set; }
        /// <summary>
        /// 数据库版本
        /// </summary>		
        public string Version { get; set; }
        /// <summary>
        /// 连接地址
        /// </summary>		
        public string DbConnection { get; set; }
        /// <summary>
        /// 连接地址是否加密
        /// </summary>		
        public int? DESEncrypt { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
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
            this.DatabaseLinkId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.DatabaseLinkId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}