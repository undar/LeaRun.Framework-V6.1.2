using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.27
    /// 描 述：授权数据范围
    /// </summary
    public class AuthorizeDataEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 授权数据主键
        /// </summary>		
        public string AuthorizeDataId { get; set; }
        /// <summary>
        /// 授权类型:1-仅限本人2-仅限本人及下属3-所在部门4-所在公司5-按明细设置
        /// </summary>		
        public int? AuthorizeType { get; set; }
        /// <summary>
        /// 对象分类:1-部门2-角色3-岗位4-职位5-工作组
        /// </summary>		
        public int Category { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>		
        public string ObjectId { get; set; }
        /// <summary>
        /// 项目Id
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 资源主键
        /// </summary>		
        public string ResourceId { get; set; }
        /// <summary>
        /// 只读
        /// </summary>		
        public int IsRead { get; set; }
        /// <summary>
        /// 约束表达式
        /// </summary>		
        public string AuthorizeConstraint { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
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
            this.AuthorizeDataId = Guid.NewGuid().ToString();
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
            this.AuthorizeDataId = keyValue;
        }
        #endregion
    }
}