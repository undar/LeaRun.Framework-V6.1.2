using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.SystemManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-17 12:09
    /// 描 述：Excel导出
    /// </summary>
    public class ExcelExportEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        public string F_Id { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <returns></returns>
        public string F_Name { get; set; }
        /// <summary>
        /// 绑定的JQgirdId
        /// </summary>
        /// <returns></returns>
        public string F_GridId { get; set; }
        /// <summary>
        /// 功能模块Id
        /// </summary>
        /// <returns></returns>
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 按钮Id
        /// </summary>
        /// <returns></returns>
        public string F_ModuleBtnId { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns></returns>
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改人Id
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        public string F_ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_EnabledMark = 1;
            this.F_CreateDate = new DateTime?(DateTime.Now);
            this.F_CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.F_CreateUserName = OperatorProvider.Provider.Current().UserName;
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.F_ModifyUserName = OperatorProvider.Provider.Current().UserName;
                                            }
        #endregion
    }
}