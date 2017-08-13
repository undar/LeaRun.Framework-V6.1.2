using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-03-14 09:47
    /// 描 述：客户信息
    /// </summary>
    public class CustomerEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        public string CustomerId { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        /// <returns></returns>
        public string EnCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        /// <returns></returns>
        public string FullName { get; set; }
        /// <summary>
        /// 客户简称
        /// </summary>
        /// <returns></returns>
        public string ShortName { get; set; }
        /// <summary>
        /// 客户行业
        /// </summary>
        /// <returns></returns>
        public string CustIndustryId { get; set; }
        /// <summary>
        /// 客户类型
        /// </summary>
        /// <returns></returns>
        public string CustTypeId { get; set; }
        /// <summary>
        /// 客户级别
        /// </summary>
        /// <returns></returns>
        public string CustLevelId { get; set; }
        /// <summary>
        /// 客户程度
        /// </summary>
        /// <returns></returns>
        public string CustDegreeId { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        /// <returns></returns>
        public string Province { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        /// <returns></returns>
        public string City { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        public string Contact { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        public string Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        public string Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        /// <returns></returns>
        public string Fax { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        public string QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        /// <returns></returns>
        public string Wechat { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        /// <returns></returns>
        public string Hobby { get; set; }
        /// <summary>
        /// 法人代表
        /// </summary>
        /// <returns></returns>
        public string LegalPerson { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        /// <returns></returns>
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司网站
        /// </summary>
        /// <returns></returns>
        public string CompanySite { get; set; }
        /// <summary>
        /// 公司情况
        /// </summary>
        /// <returns></returns>
        public string CompanyDesc { get; set; }
        /// <summary>
        /// 跟进人员Id
        /// </summary>
        /// <returns></returns>
        public string TraceUserId { get; set; }
        /// <summary>
        /// 跟进人员
        /// </summary>
        /// <returns></returns>
        public string TraceUserName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.CustomerId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ModifyDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.CustomerId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}