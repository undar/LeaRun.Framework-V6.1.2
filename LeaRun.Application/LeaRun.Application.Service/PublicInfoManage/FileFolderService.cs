using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaRun.Application.Service.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.15 10:56
    /// 描 述：文件夹
    /// </summary>
    public class FileFolderService : RepositoryFactory<FileFolderEntity>, IFileFolderService
    {
        #region 获取数据
        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileFolderEntity> GetList(string userId)
        {
            var expression = LinqExtensions.True<FileFolderEntity>();
            expression = expression.And(t => t.CreateUserId == userId);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 文件夹实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FileFolderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RestoreFile(string keyValue)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.Modify(keyValue);
            fileFolderEntity.DeleteMark = 0;
            this.BaseRepository().Update(fileFolderEntity);
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.Modify(keyValue);
            fileFolderEntity.DeleteMark = 1;
            this.BaseRepository().Update(fileFolderEntity);
        }
        /// <summary>
        /// 彻底删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存文件夹表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileFolderEntity">文件夹实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FileFolderEntity fileFolderEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                fileFolderEntity.Modify(keyValue);
                this.BaseRepository().Update(fileFolderEntity);
            }
            else
            {
                fileFolderEntity.Create();
                this.BaseRepository().Insert(fileFolderEntity);
            }
        }
        /// <summary>
        /// 共享文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        public void ShareFolder(string keyValue, int IsShare)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.FolderId = keyValue;
            fileFolderEntity.IsShare = IsShare;
            fileFolderEntity.ShareTime = DateTime.Now;
            this.BaseRepository().Update(fileFolderEntity);
        }
        #endregion
    }
}
