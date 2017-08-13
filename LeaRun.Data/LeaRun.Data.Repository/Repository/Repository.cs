using LeaRun.Util.WebControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LeaRun.Data.Repository
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.10
    /// 描 述：定义仓储模型中的数据标准操作
    /// </summary>
    public class Repository : IRepository
    {
        #region 构造
        public IDatabase db;
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 事物提交
        public IRepository BeginTrans()
        {
            db.BeginTrans();
            return this;
        }
        public void Commit()
        {
            db.Commit();
        }
        public void Rollback()
        {
            db.Rollback();
        }
        #endregion

        #region 执行 SQL 语句
        public int ExecuteBySql(string strSql)
        {
            return db.ExecuteBySql(strSql);
        }
        public int ExecuteBySql(string strSql, params DbParameter[] dbParameter)
        {
            return db.ExecuteBySql(strSql, dbParameter);
        }
        public int ExecuteByProc(string procName)
        {
            return db.ExecuteByProc(procName);
        }
        public int ExecuteByProc(string procName, params DbParameter[] dbParameter)
        {
            return db.ExecuteByProc(procName, dbParameter);
        }
        #endregion

        #region 对象实体 添加、修改、删除
        public int Insert<T>(T entity) where T : class
        {
            return db.Insert<T>(entity);
        }
        public int Insert<T>(List<T> entity) where T : class
        {
            return db.Insert<T>(entity);
        }
        public int Delete<T>() where T : class
        {
            return db.Delete<T>();
        }
        public int Delete<T>(T entity) where T : class
        {
            return db.Delete<T>(entity);
        }
        public int Delete<T>(List<T> entity) where T : class
        {
            return db.Delete<T>(entity);
        }
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return db.Delete<T>(condition);
        }
        public int Delete<T>(object keyValue) where T : class
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete<T>(object[] keyValue) where T : class
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete<T>(object propertyValue, string propertyName) where T : class
        {
            return db.Delete<T>(propertyValue, propertyName);
        }
        public int Update<T>(T entity) where T : class
        {
            return db.Update<T>(entity);
        }
        public int Update<T>(List<T> entity) where T : class
        {
            return db.Update<T>(entity);
        }
        public int Update<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return db.Update<T>(condition);
        }
        #endregion

        #region 对象实体 查询
        public T FindEntity<T>(object keyValue) where T : class
        {
            return db.FindEntity<T>(keyValue);
        }
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return db.FindEntity<T>(condition);
        }
        public IQueryable<T> IQueryable<T>() where T : class,new()
        {
            return db.IQueryable<T>();
        }
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return db.IQueryable<T>(condition);
        }
        public IEnumerable<T> FindList<T>() where T : class,new()
        {
            return db.FindList<T>();
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            return db.FindList<T>(condition);
        }
        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            return db.FindList<T>(strSql);
        }
        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter) where T : class
        {
            return db.FindList<T>(strSql, dbParameter);
        }
        public IEnumerable<T> FindList<T>(Pagination pagination) where T : class,new()
        {
            int total = pagination.records;
            var data = db.FindList<T>(pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class,new()
        {
            int total = pagination.records;
            var data = db.FindList<T>(condition, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class
        {
            int total = pagination.records;
            var data = db.FindList<T>(strSql, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList<T>(string strSql, DbParameter[] dbParameter, Pagination pagination) where T : class
        {
            int total = pagination.records;
            var data = db.FindList<T>(strSql, dbParameter, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        #endregion

        #region 数据源 查询
        public DataTable FindTable(string strSql)
        {
            return db.FindTable(strSql);
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter)
        {
            return db.FindTable(strSql, dbParameter);
        }
        public DataTable FindTable(string strSql, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindTable(strSql, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public DataTable FindTable(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindTable(strSql, dbParameter, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public object FindObject(string strSql)
        {
            return db.FindObject(strSql);
        }
        public object FindObject(string strSql, DbParameter[] dbParameter)
        {
            return db.FindObject(strSql, dbParameter);
        }
        #endregion
    }
}
