using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-31 20:07
    /// 描 述：干部拟调整
    /// </summary>
    public class Cadre_PlanAdjustEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }
        /// <summary>
        /// 干部编号
        /// </summary>
        /// <returns></returns>
        public string cadreid { get; set; }
        /// <summary>
        /// 干部姓名
        /// </summary>
        /// <returns></returns>
        public string cadrename { get; set; }
        /// <summary>
        /// 现任职务
        /// </summary>
        /// <returns></returns>
        public string currentduty { get; set; }
        /// <summary>
        /// 现任职级
        /// </summary>
        /// <returns></returns>
        public string currentrank { get; set; }
        /// <summary>
        /// 拟任职务
        /// </summary>
        /// <returns></returns>
        public string aspiringduty { get; set; }
        /// <summary>
        /// 拟免职务
        /// </summary>
        /// <returns></returns>
        public string avoidduty { get; set; }
        /// <summary>
        /// 事项分类
        /// </summary>
        /// <returns></returns>
        public string itemtype { get; set; }
        /// <summary>
        /// 民主推荐
        /// </summary>
        /// <returns></returns>
        public string democracy { get; set; }
        /// <summary>
        /// 得票数
        /// </summary>
        /// <returns></returns>
        public int? getticketnum { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        /// <returns></returns>
        public int? totalpersonnum { get; set; }
        /// <summary>
        /// 有无举报信件
        /// </summary>
        /// <returns></returns>
        public string accuseletter { get; set; }
        /// <summary>
        /// 呈报单位
        /// </summary>
        /// <returns></returns>
        public string reportcom { get; set; }
        /// <summary>
        /// 呈报时间
        /// </summary>
        /// <returns></returns>
        public DateTime? reportdate { get; set; }
        /// <summary>
        /// 任免建议备注
        /// </summary>
        /// <returns></returns>
        public string appointdemo { get; set; }
        /// <summary>
        /// 表决意见
        /// </summary>
        /// <returns></returns>
        public string decideidea { get; set; }
        /// <summary>
        /// 拟调整状态
        /// </summary>
        /// <returns></returns>
        public string planchangestatus { get; set; }
        /// <summary>
        /// 建议调整状态
        /// </summary>
        /// <returns></returns>
        public string suggestchangestatus { get; set; }
        /// <summary>
        /// 任免表决状态
        /// </summary>
        /// <returns></returns>
        public string appointresultstatus { get; set; }
        /// <summary>
        /// 任免票状态
        /// </summary>
        /// <returns></returns>
        public string appointticketstatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        public string demo { get; set; }
        /// <summary>
        /// 任免理由
        /// </summary>
        /// <returns></returns>
        public string appointreason { get; set; }
        /// <summary>
        /// 审批机关意见
        /// </summary>
        /// <returns></returns>
        public string approveidea { get; set; }
        /// <summary>
        /// 行政机关任免意见
        /// </summary>
        /// <returns></returns>
        public string appointidea { get; set; }
        /// <summary>
        /// 任免时间
        /// </summary>
        /// <returns></returns>
        public DateTime? appointdate { get; set; }
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