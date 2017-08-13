using LeaRun.Application.Busines;
using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Cache;
using LeaRun.Application.Code;
using LeaRun.Application.Entity;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.Util;
using LeaRun.Util.Attributes;
using Nancy;
using Nancy.Responses.Negotiation;
using System;

namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.04 13:57
    /// 描 述:登录接口
    /// </summary>
    public class LoginModule : BaseModule
    {

        private OrganizeCache organizeCache = new OrganizeCache();
        private DepartmentCache departmentCache = new DepartmentCache();
        private RoleCache roleCache = new RoleCache();
        private AccountBLL accountBLL = new AccountBLL();
        public LoginModule()
            : base("/learun/api")
        {
            Post["/login/checkLogin"] = CheckLogin;
            Post["/login/outLogin"] = OutLogin;
        }


        //登录
        private Negotiator CheckLogin(dynamic _)
        {
            var recdata = this.GetModule<ReceiveModule<loginData>>();
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 1;
            logEntity.OperateTypeId = ((int)OperationType.Login).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Login);
            logEntity.OperateAccount = recdata.data.username;
            logEntity.OperateUserId = recdata.data.username;
            logEntity.Module = "LeaRun.敏捷开发框架";
            try
            {
                Operator operators = new Operator();
                loginUserInfo result = new loginUserInfo();
                #region 第三方账户验证 关闭第三方账号验证
                //AccountEntity accountEntity = accountBLL.CheckLogin(recdata.data.username, recdata.data.password);
                //if (accountEntity != null)
                //{
                //    operators.UserId = accountEntity.AccountId;
                //    operators.Code = accountEntity.MobileCode;
                //    operators.Account = accountEntity.MobileCode;
                //    operators.UserName = accountEntity.FullName;
                //    operators.Password = accountEntity.Password;
                //    operators.IPAddress = Net.Ip;
                //    operators.IPAddressName = IPLocation.GetLocation(Net.Ip);
                //    operators.LogTime = DateTime.Now;
                //    operators.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                //    operators.IsSystem = true;
                //    //登录限制
                //    LoginLimit(recdata.data.username, operators.IPAddress, operators.IPAddressName, recdata.platform);
                //    result.userid = operators.UserId;
                //    result.account = operators.Account;
                //    result.password = operators.Password;
                //    result.realname = operators.UserName;
                //    result.headicon = "";
                //    result.gender = "";
                //    result.mobile = operators.Account;
                //}
                #endregion
                #region 内部登录
                //写入当前用户信息
                UserEntity userEntity = new UserBLL().CheckLogin(recdata.data.username, recdata.data.password);
                if (userEntity != null)
                {
                    AuthorizeBLL authorizeBLL = new AuthorizeBLL();
                    operators.UserId = userEntity.UserId;
                    operators.Code = userEntity.EnCode;
                    operators.Account = userEntity.Account;
                    operators.UserName = userEntity.RealName;
                    operators.Password = userEntity.Password;
                    operators.Secretkey = userEntity.Secretkey;
                    operators.CompanyId = userEntity.OrganizeId;
                    operators.DepartmentId = userEntity.DepartmentId;
                    operators.IPAddress = Net.Ip;
                    operators.ObjectId = new PermissionBLL().GetObjectStr(userEntity.UserId);
                    operators.LogTime = DateTime.Now;
                    operators.Token = DESEncrypt.Encrypt(Guid.NewGuid().ToString());
                    if (userEntity.Account == "System")
                    {
                        operators.IsSystem = true;
                    }
                    else
                    {
                        operators.IsSystem = false;
                    }
                    //写入当前用户数据权限
                    AuthorizeDataModel dataAuthorize = new AuthorizeDataModel();
                    dataAuthorize.ReadAutorize = authorizeBLL.GetDataAuthor(operators);
                    dataAuthorize.ReadAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators);
                    dataAuthorize.WriteAutorize = authorizeBLL.GetDataAuthor(operators, true);
                    dataAuthorize.WriteAutorizeUserId = authorizeBLL.GetDataAuthorUserId(operators, true);
                    operators.DataAuthorize = dataAuthorize;

                    result.userid = userEntity.UserId;
                    result.account = userEntity.Account;
                    result.password = userEntity.Password;
                    result.realname = userEntity.RealName;
                    result.headicon = "";
                    result.gender = (userEntity.Gender == 1 ? "男" : "女");
                    result.mobile = userEntity.Mobile;
                    result.telephone = userEntity.Telephone;
                    result.email = userEntity.Email;
                    result.oicq = userEntity.OICQ;
                    result.wechat = userEntity.WeChat;
                    result.msn = userEntity.MSN;
                    result.managerid = userEntity.ManagerId;
                    result.manager = userEntity.Manager;
                    result.organizeid = userEntity.OrganizeId;
                    result.organizename = organizeCache.GetEntity(result.organizeid).FullName;
                    result.departmentid = userEntity.DepartmentId;
                    result.departmentname = departmentCache.GetEntity(userEntity.DepartmentId).FullName;//值不能为null
                    result.roleid = userEntity.RoleId;
                    result.rolename = roleCache.GetEntity(userEntity.RoleId).FullName;
                    result.dutyid = userEntity.DutyId;
                    result.dutyname = userEntity.DutyName;
                    result.postid = userEntity.PostId;
                    result.postname = userEntity.PostName;
                    result.description = userEntity.Description;
                }
                #endregion
                //移动端不采用cookie的方式
                this.WriteCache<Operator>(operators, operators.UserId);

                //写入日志
                logEntity.ExecuteResult = 1;
                logEntity.ExecuteResultJson = "登录成功";
                logEntity.WriteLog();

                return this.SendData<loginUserInfo>(result, result.userid, operators.Token, ResponseType.Success);
            }
            catch (Exception ex)
            {
                logEntity.ExecuteResult = -1;
                logEntity.ExecuteResultJson = ex.Message;
                logEntity.WriteLog();
                return this.SendData(ResponseType.Fail, ex.Message);
            }
        }
        //退出
        private Negotiator OutLogin(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule>();

                bool resValidation = this.DataValidation(recdata.userid, recdata.token);

                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "无该用户登录信息");
                }
                else
                {
                    this.RomveCache(recdata.userid);
                    return this.SendData(ResponseType.Success, "用户退出成功");
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }

        }
        /// <summary>
        /// 登录限制
        /// </summary>
        /// <param name="account">账户</param>
        /// <param name="iPAddress">IP</param>
        /// <param name="iPAddressName">IP所在城市</param>
        private void LoginLimit(string account, string iPAddress, string iPAddressName, string platform)
        {
            if (account == "System")
            {
                return;
            }
            accountBLL.LoginLimit(platform, account, iPAddress, iPAddressName);
        }
    }
    //登录信息
    public class loginData
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
    }
    //登录账号信息
    public class loginUserInfo
    { /// <summary>
        /// 用户主键
        /// </summary>		
        public string userid { get; set; }
        /// <summary>
        /// 登录账户
        /// </summary>		
        public string account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string realname { get; set; }
        /// <summary>
        /// 头像
        /// </summary>		
        public string headicon { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		
        public string gender { get; set; }
        /// <summary>
        /// 手机
        /// </summary>		
        public string mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        public string telephone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>		
        public string email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>		
        public string oicq { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>		
        public string wechat { get; set; }
        /// <summary>
        /// MSN
        /// </summary>		
        public string msn { get; set; }
        /// <summary>
        /// 主管主键
        /// </summary>		
        public string managerid { get; set; }
        /// <summary>
        /// 主管
        /// </summary>		
        public string manager { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>		
        public string organizeid { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string organizename { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public string departmentid { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>		
        public string departmentname { get; set; }
        /// <summary>
        /// 角色主键
        /// </summary>		
        public string roleid { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string rolename { get; set; }
        /// <summary>
        /// 岗位主键
        /// </summary>		
        public string dutyid { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>		
        public string dutyname { get; set; }
        /// <summary>
        /// 职位主键
        /// </summary>		
        public string postid { get; set; }
        /// <summary>
        /// 职位名称
        /// </summary>		
        public string postname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string description { get; set; }
    }
}