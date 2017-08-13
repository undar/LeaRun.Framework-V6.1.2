using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Data.Repository;
using LeaRun.Util.WebControl;
using System.Collections.Generic;

namespace LeaRun.Application.IService.CustomerManage
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创 建：undar
    /// 日 期：2016-04-28 16:48
    /// 描 述：现金余额
    /// </summary>
    public interface ICashBalanceService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<CashBalanceEntity> GetList(string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 添加收支余额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cashBalanceEntity"></param>
        void AddBalance(IRepository db, CashBalanceEntity cashBalanceEntity);
        #endregion
    }
}
