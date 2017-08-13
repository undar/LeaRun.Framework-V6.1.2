using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤时段
    /// </summary>
    public class FilterTimeEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 过滤时段主键
        /// </summary>		
        public string FilterTimeId { get; set; }
        /// <summary>
        /// 对象类型
        /// </summary>		
        public string ObjectType { get; set; }
        /// <summary>
        /// 对象Id
        /// </summary>		
        public string ObjectId { get; set; }
        /// <summary>
        /// 访问
        /// </summary>		
        public int? VisitType { get; set; }
        /// <summary>
        /// 星期一
        /// </summary>		
        public string WeekDay1 { get; set; }
        /// <summary>
        /// 星期二
        /// </summary>		
        public string WeekDay2 { get; set; }
        /// <summary>
        /// 星期三
        /// </summary>		
        public string WeekDay3 { get; set; }
        /// <summary>
        /// 星期四
        /// </summary>		
        public string WeekDay4 { get; set; }
        /// <summary>
        /// 星期五
        /// </summary>		
        public string WeekDay5 { get; set; }
        /// <summary>
        /// 星期六
        /// </summary>		
        public string WeekDay6 { get; set; }
        /// <summary>
        /// 星期日
        /// </summary>		
        public string WeekDay7 { get; set; }
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
            this.FilterTimeId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.FilterTimeId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}
