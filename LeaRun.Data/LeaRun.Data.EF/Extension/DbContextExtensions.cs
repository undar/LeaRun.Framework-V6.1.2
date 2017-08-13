using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;

namespace LeaRun.Data.EF.Extension
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.10
    /// 描 述：EF上下文 扩展方法
    /// </summary>
    public static class DbContextExtensions
    {
        /// <summary>
        /// 格式化SQL语句
        /// </summary>
        /// <param name="strsql">SQL语句</param>
        /// <returns></returns>
        public static string FormatSQL(string strsql)
        {
            return null;
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName)
        {
            StringBuilder strSql = new StringBuilder("delete from " + tableName + "");
            return strSql.ToString();
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName, object propertyName, object propertyValue)
        {
            StringBuilder strSql = new StringBuilder("delete from " + tableName + " where " + propertyName + " = '" + propertyValue + "'");
            return strSql.ToString();
        }
        /// <summary>
        /// 拼接批量删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="propertyName">实体属性名称</param>
        /// <param name="propertyValue">字段值：数组1,2,3,4,5,6.....</param>
        /// <returns></returns>
        public static string DeleteSql(string tableName, object propertyName, object[] propertyValue)
        {
            StringBuilder strSql = new StringBuilder("delete from " + tableName + " where " + propertyName + " IN (");
            foreach (var item in propertyValue)
            {
                strSql.Append("'" + item + "',");
            }
            strSql.Append(")");
            return strSql.ToString();
        }
        /// <summary>
        /// 获取实体映射对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbcontext"></param>
        /// <returns></returns>
        public static EntitySet GetEntitySet<T>(DbContext dbcontext) where T : class
        {
            var metadata = ((IObjectContextAdapter)dbcontext).ObjectContext.MetadataWorkspace;
            string strname = typeof(T).Name;
            var tables = metadata.GetItemCollection(DataSpace.SSpace)
                .GetItems<EntityContainer>().Single().BaseEntitySets.OfType<EntitySet>()
                .Where(s => !s.MetadataProperties.Contains("Type")
                    || s.MetadataProperties["Type"].ToString() == "Tables");
            foreach (var table in tables)
            {
                if (table.MetadataProperties["Name"].Value.ToString() == strname)
                {
                    return table;
                }
            }
            return null;
        }
        /// <summary>
        /// 存储过程语句
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">执行命令所需的sql语句对应参数</param>
        /// <returns></returns>
        public static string BuilderProc(string procName, params DbParameter[] dbParameter)
        {
            StringBuilder strSql = new StringBuilder("exec " + procName);
            if (dbParameter != null)
            {
                foreach (var item in dbParameter)
                {
                    strSql.Append(" " + item + ",");
                }
                strSql = strSql.Remove(strSql.Length - 1, 1);
            }
            return strSql.ToString();
        }
    }
}
