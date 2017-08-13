using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.08.01 14:00
    /// 描 述：系统按钮
    /// </summary>
    public class ModuleButtonEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 按钮主键
        /// </summary>		
        public string ModuleButtonId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>		
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>		
        public string Icon { get; set; }
        /// <summary>
        /// 编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// Action地址
        /// </summary>		
        public string ActionAddress { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ModuleButtonId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ModuleButtonId = keyValue;
        }
        #endregion
    }
}