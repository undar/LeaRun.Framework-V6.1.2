using LeaRun.Application.Cache;
using LeaRun.Application.Entity.SystemManage.ViewModel;
using Nancy.Responses.Negotiation;
using System.Collections.Generic;

namespace LeaRun.Application.AppSerivce.Modules
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.12 13:57
    /// 描 述:数据字典接口
    /// </summary>
    public class DataItemNancyModule:BaseModule
    {
        private DataItemCache dataItemCache = new DataItemCache();
        public DataItemNancyModule()
            : base("/learun/api")
        {
            Post["/dataItem/list"] = List;
        }
        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator List(dynamic _)
        {
            var recdata = this.GetModule<ReceiveModule<DataItemQuery>>();
            bool resValidation = this.DataValidation(recdata.userid, recdata.token);
            if (!resValidation)
            {
                return this.SendData(ResponseType.Fail, "无该用户登录信息");
            }
            else
            {
                var data = dataItemCache.GetDataItemList(recdata.data.enCode);
                return this.SendData<IEnumerable<DataItemModel>>(data, recdata.userid, recdata.token, ResponseType.Success);
            }
         
        }
    }
    /// <summary>
    /// 字典列表请求条件
    /// </summary>
    public class DataItemQuery{
        public string enCode { get; set; }
    }
}