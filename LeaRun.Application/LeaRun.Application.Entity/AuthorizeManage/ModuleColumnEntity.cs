using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    public class ModuleColumnEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 列主键
        /// </summary>	
        public string ModuleColumnId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>		
        public string ModuleId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>		
        public string FullName { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ModuleColumnId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ModuleColumnId = keyValue;
        }
        #endregion
    }
}
