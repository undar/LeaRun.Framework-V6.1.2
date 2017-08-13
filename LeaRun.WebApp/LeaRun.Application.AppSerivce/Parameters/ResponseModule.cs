namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.09 13:57
    /// 描 述:响应数据实体
    /// </summary>
    public class ResponseModule<T> where T : class
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string token { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public Status status { set; get; }
        /// <summary>
        /// 返回结果数据
        /// </summary>
        public T result { set; get; }
        /// <summary>
        /// 用户id（可选）
        /// </summary>
        public string userid { set; get; }
    }
    public class ResponseModule
    {
        /// <summary>
        /// 状态
        /// </summary>
        public Status status { set; get; }
    }
    public class Status
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public string code{set;get;}
        /// <summary>
        /// 原因分析
        /// </summary>
        public string desc { set; get; }
    }
}