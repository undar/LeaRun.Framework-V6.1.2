using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Cache.Factory;
using LeaRun.Util.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaRun.SOA.SSO.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.09.01 13:32
    /// 描 述：单点登录
    /// </summary>
    public class LoginController : ApiControllerBase
    {
        /// <summary>
        /// 测试是否连接成功
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="system">系统</param>
        /// <param name="account">账户</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckLogin(string system, string account, string password)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = account;
            logEntity.OperateUserId = account;
            logEntity.Module = system;

            try
            {
                //验证账户
                UserEntity userEntity = new UserBLL().CheckLogin(account, password);

                //生成票据
                var ticket = Guid.NewGuid().ToString();
                //写入票据
                CacheFactory.Cache().WriteCache(userEntity, ticket, DateTime.Now.AddHours(8));

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return Success("登录成功", ticket);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return Error(ex.Message);
            }
        }
        /// <summary>
        /// 票据验证
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage CheckTicket(string ticket)
        {
            UserEntity userEntity = CacheFactory.Cache().GetCache<UserEntity>(ticket);
            if (userEntity != null)
            {
                return Success("通过", userEntity);
            }
            else
            {
                return Error("错误");
            }
        }
    }
}