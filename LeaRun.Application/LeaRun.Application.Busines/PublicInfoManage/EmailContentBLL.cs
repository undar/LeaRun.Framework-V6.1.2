using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Application.IService.PublicInfoManage;
using LeaRun.Application.Service.PublicInfoManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;

namespace LeaRun.Application.Busines.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件内容
    /// </summary>
    public class EmailContentBLL
    {
        private IEmailContentService service = new EmailContentService();

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
            return service.GetUnreadMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 未读邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetUnreadMailCount(string userId)
        {
            return service.GetUnreadMailCount(userId);
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
            return service.GetAsteriskMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 星标邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetAsteriskMailCount(string userId)
        {
            return service.GetAsteriskMailCount(userId);
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
            return service.GetDraftMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 草稿箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetDraftMailCount(string userId)
        {
            return service.GetDraftMailCount(userId);
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
            return service.GetRecycleMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 回收箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetRecycleMailCount(string userId)
        {
            return service.GetRecycleMailCount(userId);
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
            return service.GetAddresseeMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 收件箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetAddresseeMailCount(string userId)
        {
            return service.GetAddresseeMailCount(userId);
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
            return service.GetSentMail(pagination, userId, keyword);
        }
        /// <summary>
        /// 已发送数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public int GetSentMailCount(string userId)
        {
            return service.GetSentMailCount(userId);
        }
        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailContentEntity GetEntity(string keyValue)
        {
            EmailContentEntity emailContentEntity = service.GetEntity(keyValue);
            if (emailContentEntity != null)
            {
                emailContentEntity.AddresssHtml = WebHelper.HtmlDecode(emailContentEntity.AddresssHtml);
                emailContentEntity.CopysendHtml = WebHelper.HtmlDecode(emailContentEntity.CopysendHtml);
                emailContentEntity.BccsendHtml = WebHelper.HtmlDecode(emailContentEntity.BccsendHtml);
            }
            return emailContentEntity;
        }
        /// <summary>
        /// 收件人邮件明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailAddresseeEntity GetAddresseeEntity(string keyValue)
        {
            return service.GetAddresseeEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        public void RemoveForm(string keyValue, string emailType)
        {
            try
            {
                switch (emailType)
                {
                    case "unreadMail":          //未读
                        break;
                    case "starredMail":         //星标
                        break;
                    case "draftMail":           //草稿
                        service.RemoveDraftForm(keyValue);
                        break;
                    case "recycleMail":         //回收
                        break;
                    case "addresseeMail":       //收件
                        service.RemoveAddresseeForm(keyValue);
                        break;
                    case "sendMail":            //已发
                        service.RemoveSentForm(keyValue);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 彻底删除邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        public void ThoroughRemoveForm(string keyValue, string emailType)
        {
            try
            {
                switch (emailType)
                {
                    case "unreadMail":
                        break;
                    case "starredMail":
                        break;
                    case "draftMail":
                        service.RemoveDraftForm(keyValue);
                        break;
                    case "recycleMail":
                        EmailContentEntity emailcontententity = this.GetEntity(keyValue);
                        if (emailcontententity == null)
                        {
                            service.ThoroughRemoveAddresseeForm(keyValue);
                        }
                        else
                        {
                            service.ThoroughRemoveSentForm(keyValue);
                        }
                        break;
                    case "addresseeMail":
                        service.ThoroughRemoveAddresseeForm(keyValue);
                        break;
                    case "sendMail":
                        service.ThoroughRemoveSentForm(keyValue);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
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
        public void SaveForm(string keyValue, EmailContentEntity emailContentEntity, string addresssIds, string copysendIds, string bccsendIds)
        {
            try
            {
                string[] arrayaddresssId = addresssIds.Split(',');
                string[] arraycopysendId = copysendIds.Split(',');
                string[] arraybccsendId = bccsendIds.Split(',');
                emailContentEntity.AddresssHtml = WebHelper.HtmlEncode(emailContentEntity.AddresssHtml);
                emailContentEntity.CopysendHtml = WebHelper.HtmlEncode(emailContentEntity.CopysendHtml);
                emailContentEntity.BccsendHtml = WebHelper.HtmlEncode(emailContentEntity.BccsendHtml);
                service.SaveForm(keyValue, emailContentEntity, arrayaddresssId, arraycopysendId, arraybccsendId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsRead">是否已读：0-未读1-已读</param>
        public void ReadEmail(string keyValue, int IsRead = 1)
        {
            try
            {
                service.ReadEmail(keyValue, IsRead);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 设置邮件星标/取消星标
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="asterisk">星标：0-取消星标1-星标</param>
        public void SteriskEmail(string keyValue, int sterisk = 1)
        {
            try
            {
                service.SteriskEmail(keyValue, sterisk);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
