using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LeaRun.Data
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.10
    /// 描 述：操作数据库接口
    /// </summary>
    public interface IDatabase
    {
        IDatabase BeginTrans();
        int Commit();
        void Rollback();
        void Close();
        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, DbParameter[] dbParameter);
        int Insert<T>(T entity) where T : class;
        int Insert<T>(IEnumerable<T> entities) where T : class;
        int Delete<T>() where T : class;
        int Delete<T>(T entity) where T : class;
        int Delete<T>(IEnumerable<T> entities) where T : class;
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new();
        int Delete<T>(object KeyValue) where T : class;
        int Delete<T>(object[] KeyValue) where T : class;
        int Delete<T>(object propertyValue, string propertyName) where T : class;
        int Update<T>(T entity) where T : class;
        int Update<T>(IEnumerable<T> entities) where T : class;
        int Update<T>(Expression<Func<T, bool>> condition) where T : class,new();
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameter);
        T FindEntity<T>(object KeyValue) where T : class;
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IQueryable<T> IQueryable<T>() where T : class,new();
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>() where T : class,new();
        IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql) where T : class;
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class;
        IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
        IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;
        IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
    }
}
