using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.WeChatManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.23 11:31
    /// 描 述：企业号应用
    /// </summary>
    public class WeChatAppEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 应用主键
        /// </summary>		
        public string AppId { get; set; }
        /// <summary>
        /// 应用Logo
        /// </summary>		
        public string AppLogo { get; set; }
        /// <summary>
        /// 应用名称
        /// </summary>		
        public string AppName { get; set; }
        /// <summary>
        /// 应用类型
        /// </summary>		
        public int? AppType { get; set; }
        /// <summary>
        /// 应用介绍
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 应用主页
        /// </summary>		
        public string AppUrl { get; set; }
        /// <summary>
        /// 可信域名
        /// </summary>		
        public string RedirectDomain { get; set; }
        /// <summary>
        /// 应用菜单
        /// </summary>		
        public string MenuJson { get; set; }
        /// <summary>
        /// 是否接收用户变更通知
        /// </summary>		
        public int? IsReportUser { get; set; }
        /// <summary>
        /// 是否上报用户进入应用事件
        /// </summary>		
        public int? IsReportenter { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
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
            this.AppId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}