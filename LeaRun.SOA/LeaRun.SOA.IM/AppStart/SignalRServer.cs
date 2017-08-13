using LeaRun.Application.Busines.MessageManage;
using LeaRun.Application.Entity.MessageManage;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace LeaRun.SOA.IM
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.25 15:12
    /// 描 述：即使通信服务
    /// </summary>
    public class SignalRServer
    {
        /// <summary>
        /// 开启服务
        /// </summary>
        public static void Start()
        {
            try
            {
                DataInit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("初始化加载基础信息失败：{0}", ex.ToString());
                Console.ReadLine();
                return;
            }
            string SignalRURI = ConfigurationManager.AppSettings["SignalRURI"].ToString();
            try
            {
                try
                {
                    using (WebApp.Start(SignalRURI))
                    {
                        Console.WriteLine("服务开启成功,运行在{0}", SignalRURI);
                        Console.ReadLine();
                    }
                }
                catch (TargetInvocationException ex)
                {
                    Console.WriteLine("服务开启失败. 已经有一个服务运行在{0}", SignalRURI);
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
            }
            catch (Exception ex){
                Console.WriteLine("服务开启异常：{0}", ex.ToString());
                Console.ReadLine();
            }
           
        }
        /// <summary>
        /// 初始化加载联系人列表信息
        /// </summary>
        public static void DataInit()
        {
            //获取联系人列表
            IMUserBLL msguserbll = new IMUserBLL();
            IEnumerable<IMUserModel> list = msguserbll.GetList("");
            UserStorage.userAllList.Clear();
            foreach (IMUserModel item in list)
            {
                UserStorage.userAllList.Add(item.UserId,item);
            }
        }
    }
}
