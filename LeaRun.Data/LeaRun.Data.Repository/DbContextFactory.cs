using LeaRun.Data.EF;

namespace LeaRun.Data.Repository
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbContextFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <returns></returns>
        public static IDatabase Base(string connString)
        {
            return new Database(connString);
        }
        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            return new Database("Base");
        }
        /// <summary>
        /// 连接日志库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Log()
        {
            return new Database("Log");
        }
    }



}
