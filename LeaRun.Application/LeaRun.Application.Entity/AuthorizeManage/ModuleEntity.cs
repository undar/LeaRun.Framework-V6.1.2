using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.27 09:16
    /// 描 述：系统功能
    /// </summary>
    public class ModuleEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId { set; get; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string ParentId { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public string EnCode { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { set; get; }
        /// <summary>
        /// 导航地址
        /// </summary>
        public string UrlAddress { set; get; }
        /// <summary>
        /// 导航目标
        /// </summary>
        public string Target { set; get; }
        /// <summary>
        /// 菜单选项
        /// </summary>
        public int? IsMenu { set; get; }
        /// <summary>
        /// 允许展开
        /// </summary>
        public int? AllowExpand { set; get; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public int? IsPublic { set; get; }
        /// <summary>
        /// 允许编辑
        /// </summary>
        public int? AllowEdit { set; get; }
        /// <summary>
        /// 允许删除
        /// </summary>
        public int? AllowDelete { set; get; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int? DeleteMark { set; get; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int? EnabledMark { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { set; get; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string CreateUserId { set; get; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string CreateUserName { set; get; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { set; get; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string ModifyUserId { set; get; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string ModifyUserName { set; get; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ModuleId = Guid.NewGuid().ToString();
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
            this.ModuleId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
