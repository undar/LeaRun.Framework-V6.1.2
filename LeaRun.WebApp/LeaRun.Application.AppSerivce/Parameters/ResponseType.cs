using System.ComponentModel;

namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// 响应类型
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Success = 0,
        /// <summary>
        /// 访问
        /// </summary>
        [Description("用户已离线")]
        Leave = 1,
        /// <summary>
        /// 离开
        /// </summary>
        [Description("请求失败")]
        Fail = 2
    }
}
