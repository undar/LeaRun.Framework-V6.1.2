
using LeaRun.Application.Entity;
using LeaRun.Application.IService;
using LeaRun.Application.Service;
using System;
namespace LeaRun.Application.Busines
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.05.11 16:23
    /// 描 述：注册用户信息表
    /// </summary>
    public class AccountBLL
    {
        private IAccountService service = new AccountService();

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="mobileCode">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public AccountEntity CheckLogin(string mobileCode, string password)
        {
            return service.CheckLogin(mobileCode, password);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        public string GetSecurityCode(string mobileCode)
        {
            return service.GetSecurityCode(mobileCode);
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="accountEntity">实体对象</param>
        public void Register(AccountEntity accountEntity)
        {
            service.Register(accountEntity);
        }
        /// <summary>
        /// 登录限制
        /// </summary>
        /// <param name="platform">平台（苹果、安卓、PC浏览器）</param>
        /// <param name="account">账户</param>
        /// <param name="IPAddress">IP地址</param>
        /// <param name="iPAddressName">IP所在城市</param>
        public void LoginLimit(string platform, string account, string iPAddress, string iPAddressName)
        {
            try
            {
                service.LoginLimit(platform, account, iPAddress, iPAddressName);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
