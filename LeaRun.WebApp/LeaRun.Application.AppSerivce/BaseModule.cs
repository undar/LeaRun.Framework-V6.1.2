using LeaRun.Application.Code;
using LeaRun.Cache.Factory;
using LeaRun.Util;
using LeaRun.Util.Attributes;
using Nancy;
using Nancy.Json;
using Nancy.Responses.Negotiation;
using System;
using System.IO;

namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.04.18 13:57
    /// 描 述：基类
    /// </summary>
    public abstract class BaseModule : NancyModule
    {
        public BaseModule()
            : base()
        {
             
        }
        public BaseModule(string modulePath)
            : base(modulePath)
        {

        }
        /// <summary>
        /// 获取提交数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetModule<T>()where T:class
        {
             var data = this.Request.Body;
             if (data.Length > 0)
             {
                 StreamReader reader = new StreamReader(data);
                 string strbase64 = reader.ReadToEnd();
                 byte[] bbase64 = Convert.FromBase64String(strbase64);
                 string str = System.Text.Encoding.UTF8.GetString(bbase64);
                 T obj = str.ToObject<T>();
                 return obj;
             }
             else
             {
                 return null;
             }
        }
        /// <summary>
        /// 验证信息是否合法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool DataValidation(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return false;
            }
            Operator operator_ = this.ReadCache<Operator>(userId);
            if (operator_ == null || operator_.Token != token)
            {
                return false;
            }
            OperatorProvider.AppUserId = userId;
            return true;
        }

        #region 响应接口
        /// <summary>
        /// 响应数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="userid"></param>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Negotiator SendData<T>(T obj,string userid,string token,ResponseType type) where T : class
        {
            JsonSettings.MaxJsonLength = Int32.MaxValue;
            ResponseModule<T> res = new ResponseModule<T>();
            res.status = new Status();
            res.userid = userid;
            res.status.code = ((int)type).ToString();
            res.status.desc = EnumAttribute.GetDescription(type);
            res.result = obj;
            res.token = token;
            return Negotiate
            .WithStatusCode(HttpStatusCode.OK)
            .WithModel(res);
        }

        public Negotiator SendData(ResponseType type,string msg)
        {
            ResponseModule res = new ResponseModule();
            res.status = new Status();
            res.status.code = ((int)type).ToString();
            res.status.desc = msg;
            return Negotiate
            .WithStatusCode(HttpStatusCode.OK)
            .WithModel(res);
        }

        public Negotiator SendData(ResponseType type)
        {
            ResponseModule res = new ResponseModule();
            res.status = new Status();
            res.status.code = ((int)type).ToString();
            res.status.desc = EnumAttribute.GetDescription(type); 
            return Negotiate
            .WithStatusCode(HttpStatusCode.OK)
            .WithModel(res);
        }
        #endregion

        #region 读取操作
        /// <summary>
        /// 写入缓存，一天过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="cacheKey"></param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            CacheFactory.Cache().WriteCache<T>(value, cacheKey,DateTime.Now.AddDays(1));
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public T ReadCache<T>(string cacheKey) where T : class
        {
            return CacheFactory.Cache().GetCache<T>(cacheKey);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        public void RomveCache(string cacheKey)
        {
            CacheFactory.Cache().RemoveCache(cacheKey);
        }

        #endregion

    }
}