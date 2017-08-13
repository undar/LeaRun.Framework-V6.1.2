using LeaRun.Util;
using LeaRun.Util.Log;
using LeaRun.Util.WebControl;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace LeaRun.SOA.SSO
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.9 10:45
    /// 描 述：基控Api制器
    /// </summary>
    public abstract class ApiControllerBase : ApiController
    {
        private Log _logger;
        /// <summary>
        /// 日志操作
        /// </summary>
        public Log Logger
        {
            get { return _logger ?? (_logger = LogFactory.GetLogger(this.GetType().ToString())); }
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual string ToJsonResult(object data)
        {
            return data.ToJson();
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success(string message)
        {
            return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.success, message = message }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
        /// <summary>
        /// 返回成功消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="data">数据</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Success(string message, object data)
        {
            return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.success, message = message, resultdata = data }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
        /// <summary>
        /// 返回失败消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage Error(string message)
        {
            return new HttpResponseMessage { Content = new StringContent(new AjaxResult { type = ResultType.error, message = message }.ToJson(), Encoding.GetEncoding("UTF-8"), "application/json") };
        }
    }
}
