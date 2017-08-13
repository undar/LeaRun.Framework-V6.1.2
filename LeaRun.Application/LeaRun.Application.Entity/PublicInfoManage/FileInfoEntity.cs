using LeaRun.Application.Code;
using System;

namespace LeaRun.Application.Entity.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.15 10:56
    /// 描 述：文件信息
    /// </summary>
    public class FileInfoEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 文件主键
        /// </summary>		
        public string FileId { get; set; }
        /// <summary>
        /// 文件夹主键
        /// </summary>		
        public string FolderId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>		
        public string FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>		
        public string FilePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>		
        public string FileSize { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>		
        public string FileExtensions { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>		
        public string FileType { get; set; }
        /// <summary>
        /// 共享
        /// </summary>		
        public int? IsShare { get; set; }
        /// <summary>
        /// 共享连接
        /// </summary>		
        public string ShareLink { get; set; }
        /// <summary>
        /// 共享提取码
        /// </summary>		
        public int? ShareCode { get; set; }
        /// <summary>
        /// 共享日期
        /// </summary>		
        public DateTime? ShareTime { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>		
        public int? DownloadCount { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>		
        public int? IsTop { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>		
        public int? SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>		
        public int? DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>		
        public int? EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        public string Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>		
        public string CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>		
        public DateTime? ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        public string ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.FileId = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
            this.CreateUserId = OperatorProvider.Provider.Current().UserId;
            this.CreateUserName = OperatorProvider.Provider.Current().UserName;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
            this.DeleteMark = 0;
            this.EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.FileId = keyValue;
            this.ModifyDate = DateTime.Now;
            this.ModifyUserId = OperatorProvider.Provider.Current().UserId;
            this.ModifyUserName = OperatorProvider.Provider.Current().UserName;
        }
        #endregion
    }
}