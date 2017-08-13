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
    public class Cadre_BaseEntity : BaseEntity
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
        /// 照片
        /// </summary>
        /// <returns></returns>
        [Column("CADREPHOTO")]
        public string cadrephoto { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        /// <returns></returns>
        [Column("SEX")]
        public string sex { get; set; }
        /// <summary>
        /// 出生年月
        /// </summary>
        /// <returns></returns>
        [Column("BIRTH")]
        public DateTime? birth { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        /// <returns></returns>
        [Column("NATION")]
        public string nation { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        /// <returns></returns>
        [Column("NATIVEPLACE")]
        public string nativeplace { get; set; }
        /// <summary>
        /// 出生地
        /// </summary>
        /// <returns></returns>
        [Column("BIRTHPLACE")]
        public string birthplace { get; set; }
        /// <summary>
        /// 入党时间
        /// </summary>
        /// <returns></returns>
        [Column("JOINPARTYTIEM")]
        public DateTime? joinpartytiem { get; set; }
        /// <summary>
        /// 参加工作时间
        /// </summary>
        /// <returns></returns>
        [Column("WORKINGTIME")]
        public DateTime? workingtime { get; set; }
        /// <summary>
        /// 健康状况
        /// </summary>
        /// <returns></returns>
        [Column("HEALTHCONDITION")]
        public string healthcondition { get; set; }
        /// <summary>
        /// 专业技术职务
        /// </summary>
        /// <returns></returns>
        [Column("DUTY")]
        public string duty { get; set; }
        /// <summary>
        /// 熟悉专业有何特长
        /// </summary>
        /// <returns></returns>
        [Column("SPECIALITYSTRONG")]
        public string specialitystrong { get; set; }
        /// <summary>
        /// 原始学历 全日制教育
        /// </summary>
        /// <returns></returns>
        [Column("ORIGINALEDU")]
        public string originaledu { get; set; }
        /// <summary>
        /// 毕业院校 全日制毕业院校 
        /// </summary>
        /// <returns></returns>
        [Column("ORIGINALSCHOOL")]
        public string originalschool { get; set; }
        /// <summary>
        /// 专业 全日制专业
        /// </summary>
        /// <returns></returns>
        [Column("ORIGINALSPECIALITY")]
        public string originalspeciality { get; set; }
        /// <summary>
        /// 最高学历 在职教育
        /// </summary>
        /// <returns></returns>
        [Column("HIGHESTEDU")]
        public string highestedu { get; set; }
        /// <summary>
        /// 毕业院校 在职毕业院校
        /// </summary>
        /// <returns></returns>
        [Column("HIGHESTSCHOOL")]
        public string highestschool { get; set; }
        /// <summary>
        /// 专业 在职专业
        /// </summary>
        /// <returns></returns>
        [Column("HIGHESTSPECIALTY")]
        public string highestspecialty { get; set; }
        /// <summary>
        /// 现任职务
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTDUTY")]
        public string currentduty { get; set; }
        /// <summary>
        /// 个人简历
        /// </summary>
        /// <returns></returns>
        [Column("VITA")]
        public string vita { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        /// <returns></returns>
        [Column("TELEPHONE")]
        public string telephone { get; set; }
        /// <summary>
        /// 奖惩情况
        /// </summary>
        /// <returns></returns>
        [Column("REWARDSPUNISHMENTS")]
        public string rewardspunishments { get; set; }
        /// <summary>
        /// 年度考核结果
        /// </summary>
        /// <returns></returns>
        [Column("YEAREXAMRES")]
        public string yearexamres { get; set; }
        /// <summary>
        /// 党派
        /// </summary>
        /// <returns></returns>
        [Column("PARTY")]
        public string party { get; set; }
        /// <summary>
        /// 专业类别
        /// </summary>
        /// <returns></returns>
        [Column("MAJORTYPE")]
        public string majortype { get; set; }
        /// <summary>
        /// 干部身份
        /// </summary>
        /// <returns></returns>
        [Column("CADRESTATUS")]
        public string cadrestatus { get; set; }
        /// <summary>
        /// 编制
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTCOMPILE")]
        public string currentcompile { get; set; }
        /// <summary>
        /// 任现职务时间
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTDUTYDATE")]
        public DateTime? currentdutydate { get; set; }
        /// <summary>
        /// 任现职级时间
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTRANKDATE")]
        public DateTime? currentrankdate { get; set; }
        /// <summary>
        /// 现任职级
        /// </summary>
        /// <returns></returns>
        [Column("CURRENTRANK")]
        public string currentrank { get; set; }
        /// <summary>
        /// 呈报单位
        /// </summary>
        /// <returns></returns>
        [Column("REPORTCOM")]
        public string reportcom { get; set; }
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