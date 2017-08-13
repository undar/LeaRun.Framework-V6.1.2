using System;
using System.ComponentModel.DataAnnotations;
using LeaRun.Application.Code;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaRun.Application.Entity.BaseManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    [Table("Base_Organize")]
    public class OrganizeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 机构主键
        /// </summary>	
        public string OrganizeId { get; set; }
        /// <summary>
        /// 机构分类
        /// </summary>		
        public int? Category { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 公司外文
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 公司中文
        /// </summary>		
        public string ShortName { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>		
        public string Nature { get; set; }
        /// <summary>
        /// 外线电话
        /// </summary>		
        public string OuterPhone { get; set; }
        /// <summary>
        /// 内线电话
        /// </summary>		
        public string InnerPhone { get; set; }
        /// <summary>
        /// 传真
        /// </summary>		
        public string Fax { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>		
        public string Postalcode { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>		
        public string Email { get; set; }
        /// <summary>
        /// 负责人主键
        /// </summary>		
        public string ManagerId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>		
        public string Manager { get; set; }
        /// <summary>
        /// 省主键
        /// </summary>		
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市主键
        /// </summary>		
        public string CityId { get; set; }
        /// <summary>
        /// 县/区主键
        /// </summary>		
        public string CountyId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>		
        public string Address { get; set; }
        /// <summary>
        /// 公司官方
        /// </summary>		
        public string WebAddress { get; set; }
        /// <summary>
        /// 成立时间
        /// </summary>		
        public DateTime? FoundedTime { get; set; }
        /// <summary>
        /// 经营范围
        /// </summary>		
        public string BusinessScope { get; set; }
        /// <summary>
        /// 层
        /// </summary>		
        public int? Layer { get; set; }
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
            this.OrganizeId = Guid.NewGuid().ToString();
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
            this.OrganizeId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}