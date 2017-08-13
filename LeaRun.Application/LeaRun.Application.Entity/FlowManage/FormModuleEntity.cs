using System;
using System.ComponentModel.DataAnnotations.Schema;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public class FormModuleEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 表单模板Id
        /// </summary>
        /// <returns></returns>
        [Column("FRMID")]
        public string FrmId { get; set; }
        /// <summary>
        /// 表单编号
        /// </summary>
        /// <returns></returns>
        [Column("FRMCODE")]
        public string FrmCode { get; set; }
        /// <summary>
        /// 表单名称
        /// </summary>
        /// <returns></returns>
        [Column("FRMNAME")]
        public string FrmName { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        /// <returns></returns>
        [Column("FRMCATEGORY")]
        public string FrmCategory { get; set; }
        /// <summary>
        /// 表单类型0自定义表单1自定义表单(建表)2系统表单
        /// </summary>
        /// <returns></returns>
        [Column("FRMTYPE")]
        public int? FrmType { get; set; }
        /// <summary>
        /// 数据库Id
        /// </summary>
        [Column("FRMDBID")]
        public string FrmDbId { get; set; }
        /// <summary>
        /// 数据表
        /// </summary>
        [Column("FRMDBTABLE")]
        public string FrmDbTable { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        [Column("FRMDBPKEY")]
        public string FrmDbPkey { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        /// <returns></returns>
        [Column("VERSION")]
        public string Version { get; set; }
        /// <summary>
        /// 表单内容
        /// </summary>
        [Column("FRMCONTENT")]
        public string FrmContent { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        [Column("SORTCODE")]
        public int? SortCode { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        /// <returns></returns>
        [Column("DELETEMARK")]
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 状态0停用1启用3草稿
        /// </summary>
        /// <returns></returns>
        [Column("ENABLEDMARK")]
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("CREATEDATE")]
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("CREATEUSERNAME")]
        public string CreateUserName { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("CREATEUSERID")]
        public string CreateUserId { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
       [Column("MODIFYDATE")]
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERID")]
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        [Column("MODIFYUSERNAME")]
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.FrmId = Guid.NewGuid().ToString();
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateDate = DateTime.Now;
                        this.CreateUserName = OperatorProvider.Provider.Current().UserName;
                        this.DeleteMark = 0;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.FrmId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}