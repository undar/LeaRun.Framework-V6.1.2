using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-25 21:08
    /// 描 述：域外人才
    /// </summary>
    public class Talent_outFamilyEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// 人才编号
        /// </summary>
        /// <returns></returns>
        [Column("TALENTID")]
        public string talentid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string f_name { get; set; }
        /// <summary>
        /// 称谓
        /// </summary>
        /// <returns></returns>
        [Column("F_APPELLATION")]
        public string f_appellation { get; set; }
        /// <summary>
        /// 工作地点
        /// </summary>
        /// <returns></returns>
        [Column("F_WORKADDRESS")]
        public string f_workaddress { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTACTWAY")]
        public string f_contactway { get; set; }
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