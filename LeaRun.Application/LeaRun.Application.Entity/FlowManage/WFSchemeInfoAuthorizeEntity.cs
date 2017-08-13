using System;

namespace LeaRun.Application.Entity.FlowManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 09:58
    /// 描 述：工作流模板权限表
    /// </summary>
    public class WFSchemeInfoAuthorizeEntity : BaseEntity
    {
        #region 获取/设置 字段值
        /// <summary>
        /// 主键
        /// </summary>		
        public string Id { get; set; }
        /// <summary>
        /// 流程模板信息Id
        /// </summary>
        public string SchemeInfoId { get; set; }
        /// <summary>
        /// 权限对象Id
        /// </summary>
        public string ObjectId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
    }
}
