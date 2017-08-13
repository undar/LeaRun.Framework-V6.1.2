using System;
using System.Collections.Generic;
using LeaRun.Util.Extension;
using System.Linq;
using System.Linq.Expressions;

namespace LeaRun.Application.Code
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：刘晓雷
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public static class AuthorizeExtensions
    {
        #region 带权限的数据源查询
        /// <summary>
        /// 获取授权数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToAuthorizeData<T>(this IEnumerable<T> data)
        {
            if (data != null)
            {
                if (OperatorProvider.Provider.Current().IsSystem)
                    return data;
                string dataAutor = OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(dataAutor).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return data.Where(lambda.Compile());
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
