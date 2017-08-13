
namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.24 13:32
    /// 描 述：数据库表字段
    /// </summary>
    public class DataBaseTableFieldEntity
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string column { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public string datatype { get; set; }
        /// <summary>
        /// 数据长度
        /// </summary>
        public int? length { get; set; }
        /// <summary>
        /// 允许空
        /// </summary>
        public string isnullable { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string identity { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string key { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string defaults { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string remark { get; set; }
    }
}
