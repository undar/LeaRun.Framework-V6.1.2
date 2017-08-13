using System;
using LeaRun.Application.Code;

namespace LeaRun.Application.Entity.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2017-07-25 00:22
    /// 描 述：域内人才
    /// </summary>
    public class Talent_InTeamEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        public string sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        public string nation { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        /// <returns></returns>
        public DateTime? birth { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        /// <returns></returns>
        public string domicile { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        /// <returns></returns>
        public string workcom { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        /// <returns></returns>
        public string duty { get; set; }
        /// <summary>
        /// 职称
        /// </summary>
        /// <returns></returns>
        public string professionaltitle { get; set; }
        /// <summary>
        /// 编制类型
        /// </summary>
        /// <returns></returns>
        public string authtype { get; set; }
        /// <summary>
        /// 参加工作时间
        /// </summary>
        /// <returns></returns>
        public DateTime? workingtime { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        /// <returns></returns>
        public string politicsstatus { get; set; }
        /// <summary>
        /// 入党时间
        /// </summary>
        /// <returns></returns>
        public DateTime? joinpartytiem { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        /// <returns></returns>
        public string highestedu { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        /// <returns></returns>
        public string graduateschool { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        public string specialty { get; set; }
        /// <summary>
        /// 业绩
        /// </summary>
        /// <returns></returns>
        public string performance { get; set; }
        /// <summary>
        /// 荣誉
        /// </summary>
        /// <returns></returns>
        public string honor { get; set; }
        /// <summary>
        /// 人才类型
        /// </summary>
        /// <returns></returns>
        public string talenttype { get; set; }
        /// <summary>
        /// 研究方向
        /// </summary>
        /// <returns></returns>
        public string researchorientation { get; set; }
        /// <summary>
        /// 联系方式1
        /// </summary>
        /// <returns></returns>
        public string contactway1 { get; set; }
        /// <summary>
        /// 是否推荐为骨干专家
        /// </summary>
        /// <returns></returns>
        public string specialist { get; set; }
        /// <summary>
        /// 报送单位
        /// </summary>
        /// <returns></returns>
        public string submissioncom { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        /// <returns></returns>
        public string submissionperson { get; set; }
        /// <summary>
        /// 联系方式2
        /// </summary>
        /// <returns></returns>
        public string contactway2 { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public string create_on { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        /// <returns></returns>
        public string create_by { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        /// <returns></returns>
        public string update_on { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        /// <returns></returns>
        public string update_by { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        /// <returns></returns>
        public string flag { get; set; }
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