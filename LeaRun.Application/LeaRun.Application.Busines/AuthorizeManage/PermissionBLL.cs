using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Util;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LeaRun.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.5 22:35
    /// 描 述：权限配置管理（角色、岗位、职位、用户组、用户）
    /// </summary>
    public class PermissionBLL
    {
        private IPermissionService service = new PermissionService();
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetMemberList(string objectId)
        {
            return service.GetMemberList(objectId);
        }
         /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectList(string userId)
        {
            return service.GetObjectList(userId);
        }
        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public string GetObjectStr(string userId)
        {
            StringBuilder sbId = new StringBuilder();
            List<UserRelationEntity> list = service.GetObjectList(userId).ToList();
            if (list.Count > 0)
            {
                foreach (UserRelationEntity item in list)
                {
                    sbId.Append(item.ObjectId + ",");
                }
                sbId.Append(userId);
            }
            else
            {
                sbId.Append(userId + ",");
            }
            return sbId.ToString();
        }
        /// <summary>
        /// 获取功能列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleList(string objectId)
        {
            return service.GetModuleList(objectId);
        }
        /// <summary>
        /// 获取按钮列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleButtonList(string objectId)
        {
            return service.GetModuleButtonList(objectId);
        }
        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetModuleColumnList(string objectId)
        {
            return service.GetModuleColumnList(objectId);
        }
        /// <summary>
        /// 获取数据权限列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeDataEntity> GetAuthorizeDataList(string objectId)
        {
            return service.GetAuthorizeDataList(objectId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加成员
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="userIds">成员Id：1,2,3,4</param>
        public void SaveMember(AuthorizeTypeEnum authorizeType, string objectId, string userIds)
        {
            try
            {
                string[] arrayUserId = userIds.Split(',');
                service.SaveMember(authorizeType, objectId, arrayUserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存授权
        /// </summary>
        /// <param name="authorizeType">权限分类</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="authorizeDataJson">数据权限</param>
        /// <returns></returns>
        public void SaveAuthorize(AuthorizeTypeEnum authorizeType, string objectId, string moduleIds, string moduleButtonIds, string moduleColumnIds, string authorizeDataJson)
        {
            try
            {
                string[] arrayModuleId = moduleIds.Split(',');
                string[] arrayModuleButtonId = moduleButtonIds.Split(',');
                string[] arrayModuleColumnId = moduleColumnIds.Split(',');
                IEnumerable<AuthorizeDataEntity> authorizeDataList = authorizeDataJson.ToList<AuthorizeDataEntity>();
                service.SaveAuthorize(authorizeType, objectId, arrayModuleId, arrayModuleButtonId, arrayModuleColumnId, authorizeDataList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
