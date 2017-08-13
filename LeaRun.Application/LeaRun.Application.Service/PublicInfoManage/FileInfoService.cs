using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace LeaRun.Application.Service.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.15 10:56
    /// 描 述：文件信息
    /// </summary>
    public class FileInfoService : RepositoryFactory<FileInfoEntity>, IFileInfoService
    {
        #region 获取数据
        /// <summary>
        /// 所有文件（夹）列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetList(string folderId, string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate,
                                                IsShare 
                                      FROM      Base_FileFolder  where DeleteMark = 0
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate,
                                                IsShare
                                      FROM      Base_FileInfo where DeleteMark = 0
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            if (!folderId.IsEmpty())
            {
                strSql.Append(" AND FolderId = @folderId");
                parameter.Add(DbParameters.CreateDbParameter("@folderId", folderId));
            }
            else
            {
                strSql.Append(" AND FolderId = '0'");
            }
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 文档列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetDocumentList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  FileId ,
                                    FolderId ,
                                    FileName ,
                                    FileSize ,
                                    FileType ,
                                    CreateUserId ,
                                    ModifyDate,
                                    IsShare
                            FROM    Base_FileInfo
                            WHERE   DeleteMark = 0
                                    AND FileType IN ( 'log', 'txt', 'pdf', 'doc', 'docx', 'ppt', 'pptx',
                                                      'xls', 'xlsx' )
                                    AND CreateUserId = @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetImageList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  FileId ,
                                    FolderId ,
                                    FileName ,
                                    FileSize ,
                                    FileType ,
                                    CreateUserId ,
                                    ModifyDate
                                    IsShare
                            FROM    Base_FileInfo
                            WHERE   DeleteMark = 0
                                    AND FileType IN ( 'ico', 'gif', 'jpeg', 'jpg', 'png', 'psd' )
                                    AND CreateUserId = @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            strSql.Append(" ORDER BY ModifyDate ASC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 回收站文件（夹）列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetRecycledList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileFolder  where DeleteMark = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileInfo where DeleteMark = 1
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 我的文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetMyShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileFolder  WHERE DeleteMark = 0 AND IsShare = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                ModifyDate
                                      FROM      Base_FileInfo WHERE DeleteMark = 0 AND IsShare = 1
                                    ) t WHERE CreateUserId = @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 他人文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetOthersShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    FolderId AS FileId ,
                                                ParentId AS FolderId ,
                                                FolderName AS FileName ,
                                                '' AS FileSize ,
                                                'folder' AS FileType ,
                                                CreateUserId,
                                                CreateUserName,
                                                ShareTime AS ModifyDate
                                      FROM      Base_FileFolder  WHERE DeleteMark = 0 AND IsShare = 1
                                      UNION
                                      SELECT    FileId ,
                                                FolderId ,
                                                FileName ,
                                                FileSize ,
                                                FileType ,
                                                CreateUserId,
                                                CreateUserName,
                                                ShareTime AS ModifyDate
                                      FROM      Base_FileInfo WHERE DeleteMark = 0 AND IsShare = 1
                                    ) t WHERE CreateUserId != @userId");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            strSql.Append(" ORDER BY ModifyDate DESC");
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray());
        }
        /// <summary>
        /// 文件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FileInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RestoreFile(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.DeleteMark = 0;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.DeleteMark = 1;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 彻底删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存文件表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileInfoEntity">文件信息实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FileInfoEntity fileInfoEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                fileInfoEntity.Modify(keyValue);
                this.BaseRepository().Update(fileInfoEntity);
            }
            else
            {
                fileInfoEntity.Create();
                this.BaseRepository().Insert(fileInfoEntity);
            }
        }
        /// <summary>
        /// 共享文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        public void ShareFile(string keyValue, int IsShare)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.FileId = keyValue;
            fileInfoEntity.IsShare = IsShare;
            fileInfoEntity.ShareTime = DateTime.Now;
            this.BaseRepository().Update(fileInfoEntity);
        }
        #endregion
    }
}
