using LeaRun.Data.EF.Extension;
using LeaRun.Util;
using LeaRun.Util.Ioc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text.RegularExpressions;
using Microsoft.Practices.Unity;

namespace LeaRun.Data.EF
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.10
    /// 描 述：操作数据库
    /// </summary>
    public class Database : IDatabase
    {
        #region 构造函数
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        public Database(string connString, string DbType)
        {
            if (DbType == "")
            {
                dbcontext = (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(new ParameterOverride(
                          "connString", connString));
            }
            else
            {
                dbcontext = (DbContext)UnityIocHelper.DBInstance.GetService<IDbContext>(DbType, new ParameterOverride(
                           "connString", connString));
            }

        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public DbContext dbcontext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction dbTransaction { get; set; }
        #endregion

        #region 事物提交
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTrans()
        {
            DbConnection dbConnection = ((IObjectContextAdapter)dbcontext).ObjectContext.Connection;
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbConnection.BeginTransaction();
            return this;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit()
        {
            try
            {
                int returnValue = dbcontext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    this.Close();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    string msg = ExceptionMessage.GetSqlExceptionMessage(sqlEx.Number);
                    throw DataAccessException.ThrowDataAccessException(sqlEx, msg);
                }
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            this.dbTransaction.Rollback();
            this.dbTransaction.Dispose();
            this.Close();
        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            dbcontext.Dispose();
        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            if (dbTransaction == null)
            {
                return dbcontext.Database.ExecuteSqlCommand(strSql);
            }
            else
            {
                dbcontext.Database.ExecuteSqlCommand(strSql);
                return dbTransaction == null ? this.Commit() : 0;
            }
        }
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                return dbcontext.Database.ExecuteSqlCommand(strSql, dbParameter);
            }
            else
            {
                dbcontext.Database.ExecuteSqlCommand(strSql, dbParameter);
                return dbTransaction == null ? this.Commit() : 0;
            }
        }
        public int ExecuteByProc(string procName)
        {
            if (dbTransaction == null)
            {
                return dbcontext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName));
            }
            else
            {
                dbcontext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName));
                return dbTransaction == null ? this.Commit() : 0;
            }
        }
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            if (dbTransaction == null)
            {
                return dbcontext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName, dbParameter), dbParameter);
            }
            else
            {
                dbcontext.Database.ExecuteSqlCommand(DbContextExtensions.BuilderProc(procName, dbParameter), dbParameter);
                return dbTransaction == null ? this.Commit() : 0;
            }
        }
        #endregion

        #region 对象实体 添加、修改、删除
        public int Insert<T>(T entity) where T : class
        {
            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Delete<T>() where T : class
        {
            EntitySet entitySet = DbContextExtensions.GetEntitySet<T>(dbcontext);
            if (entitySet != null)
            {
                string tableName = entitySet.MetadataProperties.Contains("Table") && entitySet.MetadataProperties["Table"].Value != null
                               ? entitySet.MetadataProperties["Table"].Value.ToString()
                               : entitySet.Name;
                return this.ExecuteBySql(DbContextExtensions.DeleteSql(tableName));
            }
            return -1;
        }
        public int Delete<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Set<T>().Remove(entity);
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Set<T>().Attach(entity);
                dbcontext.Set<T>().Remove(entity);
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            IEnumerable<T> entities = dbcontext.Set<T>().Where(condition).ToList();
            return entities.Count() > 0 ? Delete(entities) : 0;
        }
        public int Delete<T>(object keyValue) where T : class
        {
            EntitySet entitySet = DbContextExtensions.GetEntitySet<T>(dbcontext);
            if (entitySet != null)
            {
                string tableName = entitySet.MetadataProperties.Contains("Table") && entitySet.MetadataProperties["Table"].Value != null
                               ? entitySet.MetadataProperties["Table"].Value.ToString()
                               : entitySet.Name;
                string keyFlied = entitySet.ElementType.KeyMembers[0].Name;
                return this.ExecuteBySql(DbContextExtensions.DeleteSql(tableName, keyFlied, keyValue));
            }
            return -1;
        }
        public int Delete<T>(object[] keyValue) where T : class
        {
            EntitySet entitySet = DbContextExtensions.GetEntitySet<T>(dbcontext);
            if (entitySet != null)
            {
                string tableName = entitySet.MetadataProperties.Contains("Table") && entitySet.MetadataProperties["Table"].Value != null
                               ? entitySet.MetadataProperties["Table"].Value.ToString()
                               : entitySet.Name;
                string keyFlied = entitySet.ElementType.KeyMembers[0].Name;
                return this.ExecuteBySql(DbContextExtensions.DeleteSql(tableName, keyFlied, keyValue));
            }
            return -1;
        }
        public int Delete<T>(object propertyValue, string propertyName) where T : class
        {
            EntitySet entitySet = DbContextExtensions.GetEntitySet<T>(dbcontext);
            if (entitySet != null)
            {
                string tableName = entitySet.MetadataProperties.Contains("Table") && entitySet.MetadataProperties["Table"].Value != null
                               ? entitySet.MetadataProperties["Table"].Value.ToString()
                               : entitySet.Name;
                return this.ExecuteBySql(DbContextExtensions.DeleteSql(tableName, propertyName, propertyValue));
            }
            return -1;
        }
        public int Update<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            Hashtable props = ConvertExtension.GetPropertyInfo<T>(entity);
            foreach (string item in props.Keys)
            {
                object value = dbcontext.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    if (value.ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(item).CurrentValue = null;
                    dbcontext.Entry(entity).Property(item).IsModified = true;
                }
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                this.Update(entity);
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        public int Update<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return 0;
        }
        #endregion

        #region 对象实体 查询
        public T FindEntity<T>(object keyValue) where T : class
        {
            return dbcontext.Set<T>().Find(keyValue);
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return dbcontext.Set<T>().Where(condition).FirstOrDefault();
        }
        public IQueryable<T> IQueryable<T>() where T : class,new()
        {
            return dbcontext.Set<T>();
        }
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return dbcontext.Set<T>().Where(condition);
        }
        public IEnumerable<T> FindList<T>() where T : class,new()
        {
            return dbcontext.Set<T>().ToList();
        }
        public IEnumerable<T> FindList<T>(Func<T, object> keySelector) where T : class,new()
        {
            return dbcontext.Set<T>().OrderBy(keySelector).ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return dbcontext.Set<T>().Where(condition).ToList();
        }
        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return FindList<T>(strSql, null);
        }
        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            using (var dbConnection = dbcontext.Database.Connection)
            {
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, dbParameter);
                return ConvertExtension.IDataReaderToList<T>(IDataReader);
            }
        }
        public IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            string[] _order = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbcontext.Set<T>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
            return tempData.ToList();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            string[] _order = orderField.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbcontext.Set<T>().Where(condition);
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(T), "t");
                var property = typeof(T).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<T>(resultExp);
            total = tempData.Count();
            tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
            return tempData.ToList();
        }
        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            return FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }
        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            using (var dbConnection = dbcontext.Database.Connection)
            {
                StringBuilder sb = new StringBuilder();
                if (pageIndex == 0)
                {
                    pageIndex = 1;
                }
                int num = (pageIndex - 1) * pageSize;
                int num1 = (pageIndex) * pageSize;
                string OrderBy = "";

                if (!string.IsNullOrEmpty(orderField))
                {
                    if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                    {
                        OrderBy = "Order By " + orderField;
                    }
                    else
                    {
                        OrderBy = "Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                    }
                }
                else
                {
                    OrderBy = "order by (select 0)";
                }
                sb.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
                sb.Append(" As rowNum, * From (" + strSql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
                total = Convert.ToInt32(new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, "Select Count(1) From (" + strSql + ") As t", dbParameter));
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, sb.ToString(), dbParameter);
                return ConvertExtension.IDataReaderToList<T>(IDataReader);
            }
        }
        #endregion

        #region 数据源查询
        public DataTable FindTable(string strSql)
        {
            return FindTable(strSql, null);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = dbcontext.Database.Connection)
            {
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, strSql, dbParameter);
                return ConvertExtension.IDataReaderToDataTable(IDataReader);
            }
        }
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return FindTable(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            using (var dbConnection = dbcontext.Database.Connection)
            {
                //此处，分页支持3种库
                StringBuilder builder = new StringBuilder();
                switch (DbHelper.DbType)
                {
                    case DatabaseType.SqlServer:
                        builder.Append(new DatabasePage().SqlPageSql(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex));
                        break;

                    case DatabaseType.MySql:
                        builder.Append(new DatabasePage().MySqlPageSql(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex));
                        break;

                    case DatabaseType.Oracle:
                        builder.Append(new DatabasePage().OraclePageSql(strSql, dbParameter, orderField, isAsc, pageSize, pageIndex));
                        break;

                    default:
                        throw new Exception("数据库类型目前不支持！");
                }
              
                total = Convert.ToInt32(new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, "Select Count(1) From (" + strSql + ") As t", dbParameter));
                var IDataReader = new DbHelper(dbConnection).ExecuteReader(CommandType.Text, builder.ToString(), dbParameter);
                DataTable resultTable = ConvertExtension.IDataReaderToDataTable(IDataReader);
                resultTable.Columns.Remove("rowNum");
                return resultTable;
            }
        }
        public object FindObject(string strSql)
        {
            return FindObject(strSql, null);
        }
        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            using (var dbConnection = dbcontext.Database.Connection)
            {
                return new DbHelper(dbConnection).ExecuteScalar(CommandType.Text, strSql, dbParameter);
            }
        }
        #endregion
    }
}
