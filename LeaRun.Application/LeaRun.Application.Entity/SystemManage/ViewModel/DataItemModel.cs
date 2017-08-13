
namespace LeaRun.Application.Entity.SystemManage.ViewModel
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemModel
    {
        #region 实体成员
        /// <summary>
        /// 分类主键
        /// </summary>		
        public string ItemId { get; set; }
        /// <summary>
        /// 分类编码
        /// </summary>		
        public string EnCode { get; set; }
        /// <summary>
        /// 明细主键
        /// </summary>		
        public string ItemDetailId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>		
        public string ParentId { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>		
        public string ItemName { get; set; }
        /// <summary>
        /// 项目值
        /// </summary>		
        public string ItemValue { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>		
        public string SimpleSpelling { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        #endregion
    }
}
