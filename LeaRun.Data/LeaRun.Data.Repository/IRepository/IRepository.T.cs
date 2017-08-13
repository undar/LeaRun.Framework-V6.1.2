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
    /// 描 述：定义仓储模型中的数据标准操作接口
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IRepository<T> where T : class,new()
    {
        IRepository<T> BeginTrans();
        void Commit();
        void Rollback();

        int ExecuteBySql(string strSql);
        int ExecuteBySql(string strSql, params DbParameter[] dbParameter);
        int ExecuteByProc(string procName);
        int ExecuteByProc(string procName, params DbParameter[] dbParameter);

        int Insert(T entity);
        int Insert(List<T> entity);
        int Delete();
        int Delete(T entity);
        int Delete(List<T> entity);
        int Delete(Expression<Func<T, bool>> condition);
        int Delete(object keyValue);
        int Delete(object[] keyValue);
        int Delete(object propertyValue, string propertyName);
        int Update(T entity);
        int Update(List<T> entity);
        int Update(Expression<Func<T, bool>> condition);

        T FindEntity(object keyValue);
        T FindEntity(Expression<Func<T, bool>> condition);
        IQueryable<T> IQueryable();
        IQueryable<T> IQueryable(Expression<Func<T, bool>> condition);
        IEnumerable<T> FindList(string strSql);
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter);
        IEnumerable<T> FindList(Pagination pagination);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination);
        IEnumerable<T> FindList(string strSql, Pagination pagination);
        IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination);

        DataTable FindTable(string strSql);
        DataTable FindTable(string strSql, DbParameter[] dbParameter);
        DataTable FindTable(string strSql, Pagination pagination);
        DataTable FindTable(string strSql, DbParameter[] dbParameter, Pagination pagination);
        object FindObject(string strSql);
        object FindObject(string strSql, DbParameter[] dbParameter);
    }
}
