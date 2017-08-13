using LeaRun.Application.Busines.CustomerManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.CustomerManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;

namespace LeaRun.Application.AppSerivce
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.12 13:57
    /// 描 述:客户管理接口
    /// </summary>
    public class CustomerManageModule : BaseModule
    {
        private ChanceBLL chancebll = new ChanceBLL();
        private CustomerBLL customerbll = new CustomerBLL();
        private CodeRuleBLL codeRuleBLL = new CodeRuleBLL();
        private OrderBLL orderBll = new OrderBLL();
        public CustomerManageModule()
            : base("/learun/api")
        {
            Post["/customerManage/chanceList"] = ChanceList;
            Post["/customerManage/saveChance"] = SaveChance;
            Post["/customerManage/customerList"] = CustomerList;
            Post["/customerManage/saveCustomer"] = SaveCustomer;
            Post["/customerManage/deleteChance"] = DeleteChance;
            Post["/customerManage/deleteCustomer"] = DeleteCustomer;
            Post["/customerManage/orderList"] = OrderList;
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator CustomerList(dynamic _)
        {
            try
            {
                var watch = CommonHelper.TimerStart();
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = customerbll.GetPageList(pagination, recdata.data.queryData);
                    DataPageList<IEnumerable<CustomerEntity>> dataPageList = new DataPageList<IEnumerable<CustomerEntity>>
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData<DataPageList<IEnumerable<CustomerEntity>>>(dataPageList, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }

        }
        /// <summary>
        /// 添加/编辑客户
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator SaveCustomer(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule<CustomerEntity>>();
                string moduleId = "1d3797f6-5cd2-41bc-b769-27f2513d61a9";//客户管理模块
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    customerbll.SaveForm(recdata.data.CustomerId, recdata.data, moduleId);
                    return this.SendData(ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator DeleteCustomer(dynamic _)
        {
            var recdata = this.GetModule<ReceiveModule<CustomerEntity>>();

            try
            {
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    customerbll.RemoveForm(recdata.data.CustomerId);
                    return this.SendData(ResponseType.Success);
                }
            }
            catch (System.Exception)
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }

        /// <summary>
        /// 获取商机列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator ChanceList(dynamic _)
        {
            try
            {
                var watch = CommonHelper.TimerStart();
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = chancebll.GetPageList(pagination, recdata.data.queryData);
                    DataPageList<IEnumerable<ChanceEntity>> dataPageList = new DataPageList<IEnumerable<ChanceEntity>>
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData<DataPageList<IEnumerable<ChanceEntity>>>(dataPageList, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 添加/编辑商机
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator SaveChance(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule<ChanceEntity>>();
                var moduleId = "66f6301c-1789-4525-a7d2-2b83272aafa6";
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    recdata.data.EnCode = codeRuleBLL.GetBillCode(recdata.userid, moduleId);
                    chancebll.SaveForm(recdata.data.ChanceId, recdata.data);
                    return this.SendData(ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
        /// <summary>
        /// 删除商机
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator DeleteChance(dynamic _)
        {
            var recdata = this.GetModule<ReceiveModule<ChanceEntity>>();

            try
            {
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    chancebll.RemoveForm(recdata.data.ChanceId);
                    return this.SendData(ResponseType.Success);
                }
            }
            catch (System.Exception)
            {
                return this.SendData(ResponseType.Fail, "异常");
            }  
        }
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator OrderList(dynamic _)
        {
            try
            {
                var watch = CommonHelper.TimerStart();
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    Pagination pagination = new Pagination
                    {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = orderBll.GetPageList(pagination, recdata.data.queryData);
                    DataPageList<IEnumerable<OrderEntity>> dataPageList = new DataPageList<IEnumerable<OrderEntity>>
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData<DataPageList<IEnumerable<OrderEntity>>>(dataPageList, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }

        }
    }
}