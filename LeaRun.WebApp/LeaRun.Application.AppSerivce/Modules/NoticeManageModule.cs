using System.Collections.Generic;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Util;
using LeaRun.Util.WebControl;
using Nancy.Responses.Negotiation;


namespace LeaRun.Application.AppSerivce.Modules
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：李於骏
    /// 日 期：2016.05.16 10:02
    /// 描 述:通知公告管理接口
    /// </summary>
    public class NoticeManageModule : BaseModule
    {
        private NoticeBLL noticebll = new NoticeBLL();
        public NoticeManageModule()
            :base("/learun/api")
        {
            Post["/publicInfoManage/noticeList"] = NoticeList;
        }
        /// <summary>
        /// 获取通知公告信息列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Negotiator NoticeList(dynamic _)
        {
            try
            {
                var watch = CommonHelper.TimerStart();
                var recdata = this.GetModule<ReceiveModule<PaginationModule>>();
                bool resValidation = this.DataValidation(recdata.userid,recdata.token);
                if (!resValidation)
                {
                    return this.SendData(ResponseType.Fail,"无登录信息");
                }
                else
                {
                    Pagination pagination = new Pagination {
                        page = recdata.data.page,
                        rows = recdata.data.rows,
                        sidx = recdata.data.sidx,
                        sord = recdata.data.sord
                    };
                    var data = noticebll.GetPageList(pagination,recdata.data.queryData);
                    DataPageList<IEnumerable<NewsEntity>> dataPageList = new DataPageList<IEnumerable<NewsEntity>>
                    {
                        rows = data,
                        total = pagination.total,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    return this.SendData<DataPageList<IEnumerable<NewsEntity>>>(dataPageList,recdata.userid, recdata.token,ResponseType.Success);
                }
            }
            catch (System.Exception)
            {
                return this.SendData(ResponseType.Fail,"异常");
            }
        }
    }
}