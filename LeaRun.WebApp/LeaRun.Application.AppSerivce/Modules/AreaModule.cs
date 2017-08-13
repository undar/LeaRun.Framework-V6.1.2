using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Entity.SystemManage;
using Nancy.Responses.Negotiation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaRun.Application.AppSerivce.Modules
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.05.19 13:57
    /// 描 述：获取行政区域信息
    /// </summary>
    public class AreaModule : BaseModule
    {
        private AreaBLL areaBLL = new AreaBLL();
        public AreaModule()
            : base("/learun/api")
        {
            Post["/area/list"] = List;
        }
        /// <summary>
        /// 获取区域行政列表列表（省分和城市）
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator List(dynamic _)
        {
            try
            {
                var recdata = this.GetModule<ReceiveModule>();
                bool resValidation = this.DataValidation(recdata.userid, recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail, "后台无登录信息");
                }
                else
                {
                    var data = areaBLL.GetAreaList("0");
                    Dictionary<string, AreaInfo> dAreaName = new Dictionary<string, AreaInfo>();
                    foreach (var item in data)
                    {
                        AreaInfo areaInfoitem = new AreaInfo();
                        areaInfoitem.areaName = item.AreaName;
                        areaInfoitem.children = new Dictionary<string, string>();
                        var itemData = areaBLL.GetAreaList(item.AreaCode);
                        foreach (var item1 in itemData)
                        {
                            if (!areaInfoitem.children.ContainsKey(item1.AreaCode))
                            {
                                areaInfoitem.children.Add(item1.AreaCode, item1.AreaName);
                            }
                        }
                        if (!dAreaName.ContainsKey(item.AreaCode))
                        {
                            dAreaName.Add(item.AreaCode, areaInfoitem);
                        }
                    }
                    return this.SendData<Dictionary<string, AreaInfo>>(dAreaName, recdata.userid, recdata.token, ResponseType.Success);
                }
            }
            catch
            {
                return this.SendData(ResponseType.Fail, "异常");
            }
        }
    }

    public class AreaInfo {
        /// <summary>
        /// 名称
        /// </summary>
        public string areaName { get; set; }

        public Dictionary<string, string> children { get; set; }
    
    }
}