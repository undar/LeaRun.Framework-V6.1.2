using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Application.IService.AuthorizeManage;
using LeaRun.Data;
using LeaRun.Data.Repository;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class,new()
    {
        private IRepository db = new RepositoryFactory().BaseRepository();
        private AuthorizeService authorizeService = new AuthorizeService();
        #region 带权限的数据源查询
        public IQueryable<T> IQueryable()
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().IQueryable();
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda);
            }
        }
        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return db.IQueryable<T>(condition);
        }
        public IEnumerable<T> FindList(Pagination pagination)
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().FindList(pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().FindList(lambda, pagination);
            }
        }
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return this.BaseRepository().FindList(condition, pagination);
        }
        public IEnumerable<T> FindList(string strSql)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql);
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter);
        }
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, pagination);
        }
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        }
        #endregion

        #region 取数据权限用户
        private string GetReadUserId()
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return "";
            }
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
        }
        private string GetReadSql()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
        }
        #endregion
    }
}
