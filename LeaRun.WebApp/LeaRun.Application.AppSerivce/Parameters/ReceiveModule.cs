namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.09 13:57
    /// 描 述:接收数据实体
    /// </summary>
    public class ReceiveModule<T>where T : class
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string token { set; get; }
        /// <summary>
        /// 传送数据
        /// </summary>
        public T data { set; get; }
        /// <summary>
        /// 用户id（可选）
        /// </summary>
        public string userid { set; get; }
        /// <summary>
        /// 平台信息
        /// </summary>
        public string platform { set; get; }
    }

    public class ReceiveModule
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string token { set; get; }
        /// <summary>
        /// 用户id（可选）
        /// </summary>
        public string userid { set; get; }
        /// <summary>
        /// 平台信息
        /// </summary>
        public string platform { set; get; }
    }
}