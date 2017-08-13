using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Application.Service.BaseManage;
using LeaRun.Util;
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
    /// 描 述：过滤时段
    /// </summary>
    public class FilterTimeBLL
    {
        private IFilterTimeService service = new FilterTimeService();

        #region 获取数据
        /// <summary>
        /// 过滤时段列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns></returns>
        public IEnumerable<FilterTimeEntity> GetList(string objectId, string visitType)
        {
            return service.GetList(objectId, visitType);
        }
        /// <summary>
        /// 过滤时段列表
        /// </summary>
        /// <param name="objectId">对象Id，用逗号隔开</param>
        /// <returns></returns>
        public IEnumerable<FilterTimeEntity> GetList(string objectId)
        {
            return service.GetList(objectId);
        }
        /// <summary>
        /// 过滤时段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FilterTimeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除过滤时段
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
        /// 保存过滤时段表单（新增、修改）
        /// </summary>
        /// <param name="filterTimeEntity">过滤时段实体</param>
        /// <returns></returns>
        public void SaveForm(FilterTimeEntity filterTimeEntity)
        {
            try
            {
                string keyValue = "";
                FilterTimeEntity entity = this.GetEntity(filterTimeEntity.ObjectId);
                if (entity != null)
                {
                    keyValue = entity.FilterTimeId;
                }
                service.SaveForm(keyValue, filterTimeEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 处理时间过滤
        /// <summary>
        /// 处理时间过滤
        /// </summary>
        /// <returns></returns>
        public bool FilterTime()
        {
            //缓存key
            string cacheKey = "FilterTime_" + OperatorProvider.Provider.Current().UserId;
            //取得用户对象关系Id
            string objectId = OperatorProvider.Provider.Current().ObjectId;
            IEnumerable<FilterTimeEntity> filterTimeList = null;
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<FilterTimeEntity>>(cacheKey);
            if (cacheList == null)
            {
                filterTimeList = this.GetList(objectId);
                CacheFactory.Cache().WriteCache(filterTimeList, cacheKey, DateTime.Now.AddMinutes(1));
            }
            else
            {
                filterTimeList = cacheList;
            }
            int weekday = Time.GetNumberWeekDay(DateTime.Now);
            string time = DateTime.Now.ToString("HH") + ":00";
            if (filterTimeList.Count() > 0)
            {
                foreach (var item in filterTimeList)
                {
                    string strFilterTime = "";
                    switch (weekday)
                    {
                        case 1:
                            strFilterTime = item.WeekDay1;
                            break;
                        case 2:
                            strFilterTime = item.WeekDay2;
                            break;
                        case 3:
                            strFilterTime = item.WeekDay3;
                            break;
                        case 4:
                            strFilterTime = item.WeekDay4;
                            break;
                        case 5:
                            strFilterTime = item.WeekDay5;
                            break;
                        case 6:
                            strFilterTime = item.WeekDay6;
                            break;
                        case 7:
                            strFilterTime = item.WeekDay7;
                            break;
                        default:
                            break;
                    }
                    if (!string.IsNullOrEmpty(strFilterTime))
                    {
                        //当前时段包含在限制时段中
                        if (strFilterTime.IndexOf(time) >= 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        #endregion
    }
}
