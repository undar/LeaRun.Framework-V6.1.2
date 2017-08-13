using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-26 21:15
    /// 描 述：干部基础
    /// </summary>
    public class Cadre_FamilyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// 人员ID
        /// </summary>
        /// <returns></returns>
        [Column("PID")]
        public string PID { get; set; }
        /// <summary>
        /// 称谓
        /// </summary>
        /// <returns></returns>
        [Column("APPELLATION")]
        public string appellation { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [Column("NAME")]
        public string name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        /// <returns></returns>
        [Column("AGE")]
        public int? age { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        /// <returns></returns>
        [Column("POLITICSSTATUS")]
        public string politicsstatus { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        /// <returns></returns>
        [Column("WORKCOM")]
        public string workcom { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        /// <returns></returns>
        [Column("DUTY")]
        public string duty { get; set; }
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