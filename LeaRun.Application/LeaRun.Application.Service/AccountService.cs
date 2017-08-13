using LeaRun.Application.Entity;
using LeaRun.Application.IService;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System;
using System.Data.Common;
using System.Linq;

namespace LeaRun.Application.Service
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.05.11 16:23
    /// 描 述：注册账户
    /// </summary>
    public class AccountService : RepositoryFactory<AccountEntity>, IAccountService
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="mobileCode">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public AccountEntity CheckLogin(string mobileCode, string password)
        {
            var expression = LinqExtensions.True<AccountEntity>();
            expression = expression.And(t => t.MobileCode == mobileCode);
            expression = expression.And(t => t.DeleteMark == 0);
            return this.BaseRepository("AccountDb").FindEntity(expression);
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="mobileCode">手机号码</param>
        /// <returns>返回6位数验证码</returns>
        public string GetSecurityCode(string mobileCode)
        {
            if (this.BaseRepository("AccountDb").IQueryable(t => t.MobileCode == mobileCode).Count() == 0)
            {
                //验证每个IP 不能获取超过5次
                if (this.BaseRepository("AccountDb").IQueryable(t => t.IPAddress == Net.Ip).Count() >= 5)
                {
                    throw new Exception("获取验证码失败。");
                }
                AccountEntity accountEntity = new AccountEntity();
                accountEntity.AccountId = Guid.NewGuid().ToString();
                accountEntity.MobileCode = mobileCode;
                accountEntity.SecurityCode = CommonHelper.RndNum(6);
                accountEntity.IPAddress = Net.Ip;
                accountEntity.CreateDate = DateTime.Now;
                accountEntity.EnabledMark = -1;
                this.BaseRepository("AccountDb").Insert(accountEntity);
                return accountEntity.SecurityCode;
            }
            else
            {
                throw new Exception("手机号已被注册过了。");
            }
        }
        /// <summary>
        /// 注册账户
        /// </summary>
        /// <param name="accountEntity">实体对象</param>
        public void Register(AccountEntity accountEntity)
        {
            var data = this.BaseRepository("AccountDb").FindEntity(t => t.MobileCode == accountEntity.MobileCode);
            if (data == null && data.SecurityCode == accountEntity.SecurityCode)
            {
                throw new Exception("短信验证码不正确。");
            }
            if (data.RegisterTime != null)
            {
                throw new Exception("手机号已被注册过了。");
            }
            accountEntity.AccountId = data.AccountId;
            accountEntity.RegisterTime = DateTime.Now;
            accountEntity.ExpireTime = DateTime.Now.AddMonths(1);
            accountEntity.DeleteMark = 0;
            accountEntity.EnabledMark = 1;
            this.BaseRepository("AccountDb").Update(accountEntity);
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
            //调用存储过程
            DbParameter[] parameter = 
            {
                DbParameters.CreateDbParameter("@IPAddress",iPAddress),
                DbParameters.CreateDbParameter("@IPAddressName",iPAddressName),
                DbParameters.CreateDbParameter("@Account",account),
                DbParameters.CreateDbParameter("@Platform",platform),
            };
            int IsOk = this.BaseRepository("AccountDb").ExecuteByProc("PROC_verify_IPAddress", parameter);
        }
    }
}
