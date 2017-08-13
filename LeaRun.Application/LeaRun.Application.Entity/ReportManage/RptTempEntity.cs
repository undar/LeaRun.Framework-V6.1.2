using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel.DataAnnotations;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.ReportManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.1.14 14:27
    /// 描 述：报表模板
    /// </summary>
    public class RptTempEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 模板主键
        /// </summary>	
        public string TempId { get; set; }
        /// <summary>
        /// 模板名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 报表编号
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 图表类型
        /// </summary>		
        public string TempType { get; set; }
        /// <summary>
        /// 报表分类
        /// </summary>		
        public string TempCategory { get; set; }
        /// <summary>
        /// 报表介绍
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 报表参数Json
        /// </summary>		
        public string ParamJson { get; set; }
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
            this.TempId = Guid.NewGuid().ToString();
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
            this.TempId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}