using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.BaseManage;
using System;
using System.Linq;
using System.Collections.Generic;
using LeaRun.Cache.Factory;

namespace LeaRun.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.20 13:32
    /// 描 述：过滤IP
    /// </summary>
    public class FilterIPBLL
    {
        private IFilterIPService service = new FilterIPService();

        #region 获取数据
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterIPEntity> GetList(string objectId, string visitType)
        {
            return service.GetList(objectId, visitType);
        }
        /// <summary>
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id,用逗号分隔</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterIPEntity> GetAllList(string objectId, int visitType)
        {
            return service.GetAllList(objectId, visitType);
        }
        /// <summary>
        /// 过滤IP实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterIPEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            try
            {
                service.SaveForm(keyValue, filterIPEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region IP过滤处理
        /// <summary>
        /// IP地址过滤
        /// </summary>
        /// <returns></returns>
        public bool FilterIP()
        {
            //缓存key
            string black_cacheKey = "blackIPList_" + OperatorProvider.Provider.Current().UserId;
            //缓存key
            string white_cacheKey = "whiteIPList_" + OperatorProvider.Provider.Current().UserId;
            //取得用户对象关系Id
            string objectId = OperatorProvider.Provider.Current().ObjectId;
            //取得当前角色的黑名单IP段
            IEnumerable<FilterIPEntity> blackIPList = null;
            var black_cacheList = CacheFactory.Cache().GetCache<IEnumerable<FilterIPEntity>>(black_cacheKey);
            if (black_cacheList == null)
            {
                blackIPList = service.GetAllList(objectId, 0);
                CacheFactory.Cache().WriteCache(blackIPList, black_cacheKey, DateTime.Now.AddMinutes(1));
            }
            else
            {
                blackIPList = black_cacheList;
            }
            bool isBlack = false;
            //如果有设置黑名单就进行黑名单判断
            if (blackIPList.Count() > 0)
            {
                isBlack = CheckArea(blackIPList);
            }
            //当前IP在黑名单中直接拒绝
            if (isBlack)
            {
                return false;
            }

            //当前角色的白名单IP段
            IEnumerable<FilterIPEntity> whiteIPList = null;
            var white_cacheList = CacheFactory.Cache().GetCache<IEnumerable<FilterIPEntity>>(white_cacheKey);
            if (white_cacheList == null)
            {
                whiteIPList = service.GetAllList(objectId, 1);
                CacheFactory.Cache().WriteCache(whiteIPList, black_cacheKey, DateTime.Now.AddMinutes(1));
            }
            else
            {
                whiteIPList = white_cacheList;
            }
            bool isWhite = true;
            if (whiteIPList.Count()>0)
            {
                isWhite = CheckArea(whiteIPList);
            }
            //如果有设置白名单，但是当前用户IP不在白名单中直接拒绝
            if (!isWhite)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断当前登陆用户IP是否在IP段中
        /// </summary>
        /// <param name="ipList"></param>
        /// <returns></returns>
        private bool CheckArea(IEnumerable<FilterIPEntity> ipList)
        {
            foreach (var item in ipList)
            {
                string strIP = item.IPLimit;
                string[] ipArry = strIP.Split(',');
                //黑名单起始IP
                string[] startArry = ipArry[0].Split('.');
                string startHead = startArry[0] + "." + startArry[1] + "." + startArry[2];
                int start = int.Parse(startArry[3]);
                //黑名单结束IP
                string[] endArry = ipArry[1].Split('.');
                string endHead = endArry[0] + "." + endArry[1] + "." + endArry[2];
                int end = int.Parse(endArry[3]);
                //当前IP
                string strIpAddress = OperatorProvider.Provider.Current().IPAddress;
                string[] ipAddressArry = strIpAddress.Split('.');
                string ipAddressHead = ipAddressArry[0] + "." + ipAddressArry[1] + "." + ipAddressArry[2];
                int ipAddress = int.Parse(ipAddressArry[3]);
                if (ipAddress >= start && ipAddress <= end)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
