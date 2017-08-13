namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.09 13:57
    /// 描 述:分页条件实体
    /// </summary>
    public class PaginationModule
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页几条数据
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sidx { get; set; }
        /// <summary>
        /// 排序类型
        /// </summary>
        public string sord { get; set; }
        /// <summary>
        /// 查询条件数据
        /// </summary>
        public string queryData { get; set; }
    }

    public class DataPageList<T>where T:class
    {
        /// <summary>
        /// 列表数据
        /// </summary>
        public T rows {get;set;}
        /// <summary>
        /// 总共记录数
        /// </summary>
        public int total {get;set;}
        /// <summary>
        /// 表示第几页
        /// </summary>
        public int page {get;set;}
        /// <summary>
        /// 记录数
        /// </summary>
        public int records {get;set;}
        /// <summary>
        /// 查询耗时
        /// </summary>
        public string costtime { get; set; }
    }
}