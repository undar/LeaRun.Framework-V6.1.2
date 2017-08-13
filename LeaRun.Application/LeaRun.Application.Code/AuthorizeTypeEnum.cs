using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Code
{
    /// <summary>
    /// 权限类型
    /// </summary>
    public enum AuthorizeTypeEnum
    {
        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department = 1,
        /// <summary>
        /// 角色
        /// </summary>
        [Description("角色")]
        Role = 2,
        /// <summary>
        /// 岗位
        /// </summary>
        [Description("岗位")]
        Post = 3,
        /// <summary>
        /// 职位
        /// </summary>
        [Description("职位")]
        Job = 4,
        /// <summary>
        /// 用户组
        /// </summary>
        [Description("用户")]
        User = 5,
        /// <summary>
        /// 用户
        /// </summary>
        [Description("用户组")]
        UserGroup = 6,
    }
}
