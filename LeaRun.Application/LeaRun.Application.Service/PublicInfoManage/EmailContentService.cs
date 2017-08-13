using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LeaRun.Application.Service.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件内容
    /// </summary>
    public class EmailContentService : RepositoryFactory<EmailContentEntity>, IEmailContentService
    {
        #region 获取数据
        /// <summary>
        /// 未读邮件
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetUnreadMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.AddresseeId AS ContentId ,
                                    c.Theme ,
                                    c.ThemeColor ,
                                    c.SenderId ,
                                    c.SenderName ,
                                    c.SenderTime ,
                                    c.SendPriority,
                                    a.IsHighlight,
                                    a.CreateDate
                            FROM    Email_Addressee a
                                    LEFT JOIN Email_Content c ON c.ContentId = a.ContentId
                            WHERE   a.RecipientId = @userId AND a.IsRead <> 1");
            strSql.Append(" AND a.DeleteMark = 0");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND c.Theme like @Theme");
                parameter.Add(DbParameters.CreateDbParameter("@Theme", '%' + keyword + '%'));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 未读邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetUnreadMailCount(string userId)
        {
            var expression = LinqExtensions.True<EmailAddresseeEntity>();
            expression = expression.And(t => t.RecipientId == userId);
            expression = expression.And(t => t.IsRead != 1);
            expression = expression.And(t => t.DeleteMark == 0);
            return new RepositoryFactory().BaseRepository().IQueryable<EmailAddresseeEntity>(expression).Count();
        }
        /// <summary>
        /// 星标邮件
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetAsteriskMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.AddresseeId AS ContentId ,
                                    c.Theme ,
                                    c.ThemeColor ,
                                    c.SenderId ,
                                    c.SenderName ,
                                    c.SenderTime ,
                                    c.SendPriority,
                                    a.IsHighlight,
                                    a.CreateDate
                            FROM    Email_Addressee a
                                    LEFT JOIN Email_Content c ON c.ContentId = a.ContentId
                            WHERE   a.RecipientId = @userId AND a.IsHighlight = 1");
            strSql.Append(" AND a.DeleteMark = 0");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND c.Theme like @Theme");
                parameter.Add(DbParameters.CreateDbParameter("@Theme", '%' + keyword + '%'));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 星标邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetAsteriskMailCount(string userId)
        {
            var expression = LinqExtensions.True<EmailAddresseeEntity>();
            expression = expression.And(t => t.RecipientId == userId);
            expression = expression.And(t => t.IsHighlight == 1);
            expression = expression.And(t => t.DeleteMark == 0);
            return new RepositoryFactory().BaseRepository().IQueryable<EmailAddresseeEntity>(expression).Count();
        }
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetDraftMail(Pagination pagination, string userId, string keyword)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.SendState == 0);
            expression = expression.And(t => t.CreateUserId == userId);
            if (!keyword.IsEmpty())
            {
                expression = expression.And(t => t.Theme.Contains(keyword));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 草稿箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetDraftMailCount(string userId)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.SendState == 0);
            expression = expression.And(t => t.CreateUserId == userId);
            return this.BaseRepository().IQueryable(expression).Count();
        }
        /// <summary>
        /// 回收箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetRecycleMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    a.AddresseeId AS ContentId ,
                                                c.Theme ,
                                                c.ThemeColor ,
                                                c.SenderId ,
                                                c.SenderName ,
                                                c.SenderTime ,
                                                c.SendPriority ,
                                                a.IsHighlight ,
                                                a.CreateDate
                                      FROM      Email_Addressee a
                                                LEFT JOIN Email_Content c ON c.ContentId = a.ContentId
                                      WHERE     a.RecipientId = @userId
                                                AND a.DeleteMark = 1
                                      UNION
                                      SELECT    c.ContentId ,
                                                c.Theme ,
                                                c.ThemeColor ,
                                                c.SenderId ,
                                                c.SenderName ,
                                                c.SenderTime ,
                                                c.SendPriority ,
                                                c.IsHighlight ,
                                                c.CreateDate
                                      FROM      Email_Content c
                                      WHERE     c.CreateUserId = @userId
                                                AND c.DeleteMark = 1
                                    ) t WHERE 1=1");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND Theme like @Theme");
                parameter.Add(DbParameters.CreateDbParameter("@Theme", '%' + keyword + '%'));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 回收箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetRecycleMailCount(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  COUNT(1)
                            FROM    ( SELECT    a.AddresseeId AS ContentId ,
                                                c.Theme ,
                                                c.ThemeColor ,
                                                c.SenderId ,
                                                c.SenderName ,
                                                c.SenderTime ,
                                                c.SendPriority ,
                                                a.IsHighlight ,
                                                a.CreateDate
                                      FROM      Email_Addressee a
                                                LEFT JOIN Email_Content c ON c.ContentId = a.ContentId
                                      WHERE     a.RecipientId = @userId
                                                AND a.DeleteMark = 1
                                      UNION
                                      SELECT    c.ContentId ,
                                                c.Theme ,
                                                c.ThemeColor ,
                                                c.SenderId ,
                                                c.SenderName ,
                                                c.SenderTime ,
                                                c.SendPriority ,
                                                c.IsHighlight ,
                                                c.CreateDate
                                      FROM      Email_Content c
                                      WHERE     c.CreateUserId = @userId
                                                AND c.DeleteMark = 1
                                    ) t");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            return this.BaseRepository().FindObject(strSql.ToString(), parameter.ToArray()).ToInt();
        }
        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetAddresseeMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.AddresseeId AS ContentId ,
                                    c.Theme ,
                                    c.ThemeColor ,
                                    c.SenderId ,
                                    c.SenderName ,
                                    c.SenderTime ,
                                    c.SendPriority,
                                    c.CreateDate,
                                    a.IsHighlight
                            FROM    Email_Addressee a
                                    LEFT JOIN Email_Content c ON c.ContentId = a.ContentId
                            WHERE   a.RecipientId = @userId");
            strSql.Append(" AND a.DeleteMark = 0 ");
            var parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND c.Theme like @Theme");
                parameter.Add(DbParameters.CreateDbParameter("@Theme", '%' + keyword + '%'));
            }
            return this.BaseRepository().FindList(strSql.ToString(), parameter.ToArray(), pagination);
        }
        /// <summary>
        /// 收件箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetAddresseeMailCount(string userId)
        {
            var expression = LinqExtensions.True<EmailAddresseeEntity>();
            expression = expression.And(t => t.RecipientId == userId);
            expression = expression.And(t => t.DeleteMark == 0);
            return new RepositoryFactory().BaseRepository().IQueryable<EmailAddresseeEntity>(expression).Count();
        }
        /// <summary>
        /// 已发送
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetSentMail(Pagination pagination, string userId, string keyword)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.SendState == 1);
            expression = expression.And(t => t.CreateUserId == userId);
            expression = expression.And(t => t.DeleteMark == 0);
            if (!keyword.IsEmpty())
            {
                expression = expression.And(t => t.Theme.Contains(keyword));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 已发送数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetSentMailCount(string userId)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.SendState == 1);
            expression = expression.And(t => t.CreateUserId == userId);
            expression = expression.And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).Count();
        }
        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailContentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 收件邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailAddresseeEntity GetAddresseeEntity(string keyValue)
        {
            return new RepositoryFactory().BaseRepository().FindEntity<EmailAddresseeEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveDraftForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveAddresseeForm(string keyValue)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.AddresseeId = keyValue;
            emailAddresseeEntity.DeleteMark = 1;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        /// <summary>
        /// 彻底删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveAddresseeForm(string keyValue)
        {
            new RepositoryFactory().BaseRepository().Delete<EmailAddresseeEntity>(keyValue);
        }
        /// <summary>
        /// 删除回收
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="Type">类型</param>
        public void RemoveRecycleForm(string keyValue, int Type)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveSentForm(string keyValue)
        {
            EmailContentEntity emailContentEntity = new EmailContentEntity();
            emailContentEntity.ContentId = keyValue;
            emailContentEntity.DeleteMark = 1;
            this.BaseRepository().Update(emailContentEntity);
        }
        /// <summary>
        /// 彻底删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveSentForm(string keyValue)
        {
            EmailContentEntity emailContentEntity = new EmailContentEntity();
            emailContentEntity.ContentId = keyValue;
            emailContentEntity.DeleteMark = 2;
            this.BaseRepository().Update(emailContentEntity);
        }
        /// <summary>
        /// 保存邮件表单（发送、存入草稿、草稿编辑）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailContentEntity">邮件实体</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄送人</param>
        /// <param name="bccsendIds">密送人</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, EmailContentEntity emailContentEntity, string[] addresssIds, string[] copysendIds, string[] bccsendIds)
        {
            if (emailContentEntity.SendState == 0)
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    emailContentEntity.Modify(keyValue);
                    this.BaseRepository().Update(emailContentEntity);
                }
                else
                {
                    emailContentEntity.Create();
                    this.BaseRepository().Insert(emailContentEntity);
                }
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        emailContentEntity.Modify(keyValue);
                        db.Update(emailContentEntity);
                    }
                    else
                    {
                        emailContentEntity.Create();
                        db.Insert(emailContentEntity);
                    }

                    #region 收件人
                    foreach (var item in addresssIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.ContentId = emailContentEntity.ContentId;
                        emailAddresseeEntity.RecipientId = item;
                        emailAddresseeEntity.RecipientState = 0;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    #region 抄送人
                    foreach (var item in copysendIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.ContentId = emailContentEntity.ContentId;
                        emailAddresseeEntity.RecipientId = item;
                        emailAddresseeEntity.RecipientState = 1;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    #region  密送人
                    foreach (var item in bccsendIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.ContentId = emailContentEntity.ContentId;
                        emailAddresseeEntity.RecipientId = item;
                        emailAddresseeEntity.RecipientState = 2;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    db.Commit();
                }
                catch (System.Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsRead">是否已读：0-未读1-已读</param>
        public void ReadEmail(string keyValue, int IsRead = 1)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.AddresseeId = keyValue;
            emailAddresseeEntity.IsRead = IsRead;
            emailAddresseeEntity.ReadCount = emailAddresseeEntity.ReadCount + 1;
            emailAddresseeEntity.ReadDate = DateTime.Now;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        /// <summary>
        /// 设置邮件星标/取消星标
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="asterisk">星标：0-取消星标1-星标</param>
        public void SteriskEmail(string keyValue, int sterisk = 1)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.AddresseeId = keyValue;
            emailAddresseeEntity.IsHighlight = sterisk;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        #endregion
    }
}
