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
    public class Talent_OutTeamEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [Column("NAME")]
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [Column("SEX")]
        public string sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        [Column("NATION")]
        public string nation { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        /// <returns></returns>
        [Column("BIRTH")]
        public DateTime? birth { get; set; }
        /// <summary>
        /// 户籍所在地
        /// </summary>
        /// <returns></returns>
        [Column("DOMICILE")]
        public string domicile { get; set; }
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
        /// <summary>
        /// 职称
        /// </summary>
        /// <returns></returns>
        [Column("PROFESSIONALTITLE")]
        public string professionaltitle { get; set; }
        /// <summary>
        /// 编制类型
        /// </summary>
        /// <returns></returns>
        [Column("AUTHTYPE")]
        public string authtype { get; set; }
        /// <summary>
        /// 参加工作时间
        /// </summary>
        /// <returns></returns>
        [Column("WORKINGTIME")]
        public DateTime? workingtime { get; set; }
        /// <summary>
        /// 政治面貌
        /// </summary>
        /// <returns></returns>
        [Column("POLITICSSTATUS")]
        public string politicsstatus { get; set; }
        /// <summary>
        /// 入党时间
        /// </summary>
        /// <returns></returns>
        [Column("JOINPARTYTIEM")]
        public DateTime? joinpartytiem { get; set; }
        /// <summary>
        /// 最高学历
        /// </summary>
        /// <returns></returns>
        [Column("HIGHESTEDU")]
        public string highestedu { get; set; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        /// <returns></returns>
        [Column("GRADUATESCHOOL")]
        public string graduateschool { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        [Column("SPECIALTY")]
        public string specialty { get; set; }
        /// <summary>
        /// 人才类型
        /// </summary>
        /// <returns></returns>
        [Column("TALENTTYPE")]
        public string talenttype { get; set; }
        /// <summary>
        /// 研究方向
        /// </summary>
        /// <returns></returns>
        [Column("RESEARCHORIENTATION")]
        public string researchorientation { get; set; }
        /// <summary>
        /// 联系方式1
        /// </summary>
        /// <returns></returns>
        [Column("CONTACTWAY1")]
        public string contactway1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("MEMO")]
        public string memo { get; set; }
        /// <summary>
        /// 报送单位
        /// </summary>
        /// <returns></returns>
        [Column("SUBMISSIONCOM")]
        public string submissioncom { get; set; }
        /// <summary>
        /// 报送人
        /// </summary>
        /// <returns></returns>
        [Column("SUBMISSIONPERSON")]
        public string submissionperson { get; set; }
        /// <summary>
        /// 联系方式2
        /// </summary>
        /// <returns></returns>
        [Column("CONTACTWAY2")]
        public string contactway2 { get; set; }
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