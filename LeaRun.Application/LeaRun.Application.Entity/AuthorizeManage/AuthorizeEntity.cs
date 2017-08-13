using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.27
    /// 描 述：授权功能
    /// </summary
    public class AuthorizeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 授权功能主键
        /// </summary>		
        public string AuthorizeId { get; set; }
        /// <summary>
        /// 对象分类:1-部门2-角色3-岗位4-职位5-工作组
        /// </summary>		
        public int? Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>		
        public string ObjectId { get; set; }
        /// <summary>
        /// 项目类型:1-菜单2-按钮3-视图4表单
        /// </summary>		
        public int? ItemType { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 创建时间
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
            this.AuthorizeId = Guid.NewGuid().ToString();
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
            this.AuthorizeId = keyValue;
        }
        #endregion
    }
}