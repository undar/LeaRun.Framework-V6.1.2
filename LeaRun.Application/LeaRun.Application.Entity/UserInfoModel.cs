using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Entity
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.21 09:58
    /// 描 述：连接用户信息类(包括web端登录,或者接口形式的连接)
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 连接时间
        /// </summary>
        public DateTime ConnectTime { get; set; }
        /// <summary>
        /// 公司Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }
        /// <summary>
        /// 对象用户关系Id,对象分类:1-部门2-角色3-岗位4-群组
        /// </summary>
        public string ObjectId { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 登录IP地址所在地址
        /// </summary>
        public string IPAddressName { get; set; }
        /// <summary>
        /// 是否系统账户；拥有所以权限
        /// </summary>
        public bool IsSystem { get; set; }
        /// <summary>
        /// 登录Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Secretkey { get; set; }
    }
}
