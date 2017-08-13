using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.DemoManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-02-21 09:18
    /// 描 述：测试1
    /// </summary>
    public class t2Entity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        /// <returns></returns>
        [Column("USERNAME")]
        public string username { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        [Column("PWD")]
        public string pwd { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        /// <returns></returns>
        [Column("DID")]
        public string did { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.id = Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id = keyValue;
                                            }
        #endregion
    }
}