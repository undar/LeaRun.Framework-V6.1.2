using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace LeaRun.Data.Repository
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.10.10
    /// 描 述：定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public class Repository<T> : IRepository<T> where T : class,new()
    {
        #region 构造
        public IDatabase db;
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 事物提交
        public IRepository<T> BeginTrans()
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
        public int Insert(T entity)
        {
            return db.Insert<T>(entity);
        }
        public int Insert(List<T> entity)
        {
            return db.Insert<T>(entity);
        }
        public int Delete()
        {
            return db.Delete<T>();
        }
        public int Delete(T entity)
        {
            return db.Delete<T>(entity);
        }
        public int Delete(List<T> entity)
        {
            return db.Delete<T>(entity);
        }
        public int Delete(Expression<Func<T, bool>> condition)
        {
            return db.Delete<T>(condition);
        }
        public int Delete(object keyValue)
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete(object[] keyValue)
        {
            return db.Delete<T>(keyValue);
        }
        public int Delete(object propertyValue, string propertyName)
        {
            return db.Delete<T>(propertyValue, propertyName);
        }
        public int Update(T entity)
        {
            return db.Update<T>(entity);
        }
        public int Update(List<T> entity)
        {
            return db.Update<T>(entity);
        }
        public int Update(Expression<Func<T, bool>> condition)
        {
            return db.Update<T>(condition);
        }
        #endregion

        #region 对象实体 查询
        public T FindEntity(object keyValue)
        {
            return db.FindEntity<T>(keyValue);
        }
        public T FindEntity(Expression<Func<T, bool>> condition)
        {
            return db.FindEntity<T>(condition);
        }
        public IQueryable<T> IQueryable()
        {
            return db.IQueryable<T>();
        }
        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            return db.IQueryable<T>(condition);
        }
        public IEnumerable<T> FindList(string strSql)
        {
            return db.FindList<T>(strSql);
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            return db.FindList<T>(strSql, dbParameter);
        }
        public IEnumerable<T> FindList(Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindList<T>(pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindList<T>(condition, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindList<T>(strSql, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
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
