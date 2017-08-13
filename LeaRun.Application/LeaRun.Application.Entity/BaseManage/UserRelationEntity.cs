using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.03 10:58
    /// 描 述：用户关系
    /// </summary>
    public class UserRelationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户关系主键
        /// </summary>		
        public string UserRelationId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 分类:1-部门2-角色3-岗位4-职位5-工作组
        /// </summary>		
        public int? Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>		
        public string ObjectId { get; set; }
        /// <summary>
        /// 默认对象
        /// </summary>
        public int? IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int SortCode { get; set; }
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
            this.UserRelationId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.IsDefault = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            
        }
        #endregion
    }
}