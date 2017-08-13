using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单
    /// </summary>
    public class ModuleFormEntity:BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 表单主键
        /// </summary>
        public string FormId { set; get; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId { set; get; }
        /// <summary>
        /// 编码
        /// </summary>
        public string EnCode { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
        /// <summary>
        /// 表单控件Json
        /// </summary>
        public string FormJson { set; get; }
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
            this.FormId = Guid.NewGuid().ToString();
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
            this.FormId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
