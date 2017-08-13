using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.AuthorizeManage.ViewModel;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.AuthorizeManage;
using LeaRun.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeBLL
    {
        private IAuthorizeService service = new AuthorizeService();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();

        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleBLL.GetList().FindAll(t => t.EnabledMark.Equals(1));
            }
            else
            {
                return service.GetModuleList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleButtonBLL.GetList();
            }
            else
            {
                return service.GetModuleButtonList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleColumnBLL.GetList();
            }
            else
            {
                return service.GetModuleColumnList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            return service.GetUrlList(userId);
        }
        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="action">请求地址</param>
        /// <returns></returns>
        public bool ActionAuthorize(string userId, string moduleId, string action)
        {
            List<AuthorizeUrlModel> authorizeUrlList = new List<AuthorizeUrlModel>();
            var cacheList = CacheFactory.Cache().GetCache<List<AuthorizeUrlModel>>("AuthorizeUrl_" + userId);
            if (cacheList == null)
            {
                authorizeUrlList = this.GetUrlList(userId).ToList();
                CacheFactory.Cache().WriteCache(authorizeUrlList, "AuthorizeUrl_" + userId, DateTime.Now.AddMinutes(1));
            }
            else
            {
                authorizeUrlList = cacheList;
            }
            authorizeUrlList = authorizeUrlList.FindAll(t => t.ModuleId.Equals(moduleId));
            foreach (AuthorizeUrlModel item in authorizeUrlList)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    string[] url = item.UrlAddress.Split('?');
                    if (item.ModuleId == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthorUserId(operators, isWrite);
        }
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthor(operators, isWrite);
        }
    }
}
