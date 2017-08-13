using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.26 09:16
    /// 描 述：系统表单实例
    /// </summary>
    public class ModuleFormInstanceEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 表单主键
        /// </summary>
        public string FormInstanceId { set; get; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string FormId { set; get; }
        /// <summary>
        /// 编码
        /// </summary>
        public string FormInstanceJson { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string ObjectId { set; get; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int? SortCode { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Description { set; get; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.FormInstanceId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.FormInstanceId = keyValue;
        }
        #endregion
    }
}


