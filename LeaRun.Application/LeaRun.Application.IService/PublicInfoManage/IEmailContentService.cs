using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.PublicInfoManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件内容
    /// </summary>
    public interface IEmailContentService
    {
        #region 获取数据
        /// <summary>
        /// 未读邮件
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetUnreadMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 未读邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetUnreadMailCount(string userId);
        /// <summary>
        /// 星标邮件
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetAsteriskMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 星标邮件数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetAsteriskMailCount(string userId);
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetDraftMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 草稿箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetDraftMailCount(string userId);
        /// <summary>
        /// 回收箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetRecycleMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 回收箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetRecycleMailCount(string userId);
        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetAddresseeMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 收件箱数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetAddresseeMailCount(string userId);
        /// <summary>
        /// 已发送
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetSentMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 已发送数量
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        int GetSentMailCount(string userId);
        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EmailContentEntity GetEntity(string keyValue);
        /// <summary>
        /// 收件邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EmailAddresseeEntity GetAddresseeEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveDraftForm(string keyValue);
        /// <summary>
        /// 删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveAddresseeForm(string keyValue);
        /// <summary>
        /// 彻底删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void ThoroughRemoveAddresseeForm(string keyValue);
        /// <summary>
        /// 删除回收
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="Type">类型</param>
        void RemoveRecycleForm(string keyValue, int Type);
        /// <summary>
        /// 删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveSentForm(string keyValue);
        /// <summary>
        /// 彻底删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        void ThoroughRemoveSentForm(string keyValue);
        /// <summary>
        /// 保存邮件表单（发送、存入草稿、草稿编辑）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailContentEntity">邮件实体</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄送人</param>
        /// <param name="bccsendIds">密送人</param>
        /// <returns></returns>
        void SaveForm(string keyValue, EmailContentEntity emailContentEntity, string[] addresssIds, string[] copysendIds, string[] bccsendIds);
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsRead">是否已读：0-未读1-已读</param>
        void ReadEmail(string keyValue, int IsRead = 1);
        /// <summary>
        /// 设置邮件星标/取消星标
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="asterisk">星标：0-取消星标1-星标</param>
        void SteriskEmail(string keyValue, int sterisk = 1);
        #endregion
    }
}
