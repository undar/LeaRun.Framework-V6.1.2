using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号部门
    /// </summary>
    public class WeChatDeptRelationEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 部门对应关系主键
        /// </summary>		
        public string DeptRelationId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>		
        public string DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>		
        public string DeptName { get; set; }
        /// <summary>
        /// 微信部门Id
        /// </summary>		
        public int? WeChatDeptId { get; set; }
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
            this.DeptRelationId = keyValue;
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}