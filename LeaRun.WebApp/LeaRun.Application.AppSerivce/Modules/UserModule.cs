using LeaRun.Application.Cache;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.WebApp;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.AppSerivce.Modules
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.04 13:57
    /// 描 述:登录接口
    /// </summary>
    public class UserModule : BaseModule
    {
        private UserCache userCache = new UserCache();
        public UserModule()
            : base("/learun/api")
        {
            Post["/user/modifyPassword"] = ModifyPassword;//暂时有问题
            Post["/user/getUserList"] = GetUserList;
        }
        /// <summary>
        /// 修改密码接口
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator ModifyPassword(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Leave);
                }
                else
                {
                    this.RomveCache(recdata.userid);
                    return this.SendData(ResponseType.Success, "用户退出成功");
                }
            }
            catch (Exception ex)
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator GetUserList(dynamic _)
        {
            try{
                var recdata = this.GetModule<ReceiveModule>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var data = userCache.GetListToApp();
                    return this.SendData<Dictionary<string, appUserInfoModel>>(data, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch { 
                return this.SendData(ResponseType.Fail, "异常");
            }   
        }
    }
}