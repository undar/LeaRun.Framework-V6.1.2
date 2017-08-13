using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Reflection;
using LeaRun.Util.Ioc;
using LeaRun.Data.EF;

namespace LeaRun.Data.Repository
{
    public class IocHelper
    {
        private readonly TinyIoCContainer _container;
        private static readonly IocHelper instance = new IocHelper();
        public IocHelper()
        {   
            _container = new TinyIoCContainer();
            _container.Register<IDatabase, Database>();
        }
        public static IocHelper Instance
        {
            get { return instance; }
        }
        /// <summary>
        /// Ioc容器
        /// </summary>
        public TinyIoCContainer Container
        {
            get { return _container; }
        }
    }
}
