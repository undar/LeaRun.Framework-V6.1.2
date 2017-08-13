using System;

namespace LeaRun.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.05.11 16:23
    /// 描 述：注册账户
    /// </summary>
    public class AccountEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobileCode { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string SecurityCode { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public DateTime? ExpireTime { get; set; }
        /// <summary>
        /// IPAddress
        /// </summary>		
        public string IPAddress { get; set; }
        /// <summary>
        /// IPAddressName
        /// </summary>		
        public string IPAddressName { get; set; }
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
        /// 最后登录时间
        /// </summary>		
        public DateTime? LastVisit { get; set; }
        /// <summary>
        /// 登录次数
        /// </summary>		
        public int? LogOnCount { get; set; }
        /// <summary>
        /// 授权登录次数
        /// </summary>
        public int? AmountCount { get; set; }
    }
}
