using System;
using System.IO;
using System.Threading;
using System.Web;
using LeaRun.Application.Busines.PublicInfoManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.PublicInfoManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using LeaRun.Util.WebControl;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace LeaRun.Application.Web.Areas.PublicInfoManage.Controllers
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.12.8 11:31
    /// 描 述：邮件中心
    /// </summary>
    public class EmailController : MvcControllerBase
    {
        private EmailCategoryBLL emailCategoryBLL = new EmailCategoryBLL();
        private EmailContentBLL emailContentBLL = new EmailContentBLL();

        #region 视图功能
        /// <summary>
        /// 邮件中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 邮件分类表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailCategoryForm()
        {
            return View();
        }
        /// <summary>
        /// 写邮件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailWriteForm()
        {
            return View();
        }
        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <returns></returns>

        /// <summary>
        /// 写邮件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileViewForm()
        {
            return View();
        }
        /// <summary>
        /// 查看邮件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EmailLookDetail()
        {
            string keyValue = Request["keyValue"];
            EmailAddresseeEntity emailAddresseeEntity = emailContentBLL.GetAddresseeEntity(keyValue);
            if (emailAddresseeEntity != null)
            {
                ViewBag.contentId = emailAddresseeEntity.ContentId;
            }
            else
            {
                ViewBag.contentId = keyValue;
                keyValue = "";
            }
            ViewBag.keyValue = keyValue;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetCategoryListJson()
        {
            string UserId = OperatorProvider.Provider.Current().UserId;
            var data = emailCategoryBLL.GetList(UserId);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 分类实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetCategoryFormJson(string keyValue)
        {
            var data = emailCategoryBLL.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 统计邮箱数量（未读、星标、草稿、回收、收件、已发）
        /// </summary>
        /// <returns>返回数量</returns>
        public ActionResult GetMailCount()
        {
            string userId = OperatorProvider.Provider.Current().UserId;
            var unreadcount = emailContentBLL.GetUnreadMailCount(userId);
            var asteriskcount = emailContentBLL.GetAsteriskMailCount(userId);
            var sentcount = emailContentBLL.GetSentMailCount(userId);
            var draftcount = emailContentBLL.GetDraftMailCount(userId);
            var recyclecount = emailContentBLL.GetRecycleMailCount(userId);
            var addresseecount = emailContentBLL.GetAddresseeMailCount(userId);
            var count = new
            {
                unread = unreadcount,
                asterisk = asteriskcount,
                draft = draftcount,
                recycle = recyclecount,
                addressee = addresseecount,
                sent = sentcount,
            };
            return Content(count.ToJson());
        }
        /// <summary>
        /// 未读邮件列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetUnreadMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetUnreadMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 星标邮件列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetAsteriskMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetAsteriskMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 草稿箱列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetDraftMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetDraftMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 回收箱列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetRecycleMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetRecycleMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 收件箱列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetAddresseeMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetAddresseeMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 已发送列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns>返回列表Json</returns>
        public ActionResult GetSentMailJson(Pagination pagination, string keyword)
        {
            var watch = CommonHelper.TimerStart();
            string userId = OperatorProvider.Provider.Current().UserId;
            var data = emailContentBLL.GetSentMail(pagination, userId, keyword);
            var JsonData = new
            {
                rows = data,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return Content(JsonData.ToJson());
        }
        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetEmailFormJson(string keyValue)
        {
            var data = emailContentBLL.GetEntity(keyValue);
            string strJson = data.ToJson();
            strJson = strJson.Insert(1, "\"SenderTimeName\":\"" + Time.GetChineseDateTime(data.SenderTime.ToDate()) + "\",");
            return Content(strJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveCategoryForm(string keyValue)
        {
            emailCategoryBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailCategoryEntity">分类实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveCategoryForm(string keyValue, EmailCategoryEntity emailCategoryEntity)
        {
            emailCategoryBLL.SaveForm(keyValue, emailCategoryEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult RemoveEmailForm(string keyValue, string emailType)
        {
            emailContentBLL.RemoveForm(keyValue, emailType);
            return Success("删除成功。");
        }
        /// <summary>
        /// 彻底删除邮件
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Enforce)]
        public ActionResult ThoroughRemoveEmailForm(string keyValue, string emailType)
        {
            emailContentBLL.ThoroughRemoveForm(keyValue, emailType);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存写邮件表单（发送、草稿、编辑）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailContentEntity">邮件信息实体</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄送人</param>
        /// <param name="bccsendIds">密送人</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [ValidateInput(false)]
        public ActionResult SaveEmailForm(string keyValue, EmailContentEntity emailContentEntity, string addresssIds, string copysendIds, string bccsendIds)
        {
            emailContentBLL.SaveForm(keyValue, emailContentEntity, addresssIds, copysendIds, bccsendIds);
            if (emailContentEntity.SendState == 1)
            {
                return Success("邮件发送成功。");
            }
            else
            {
                return Success("存入草稿成功。");
            }
        }
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="IsRead">是否已读：0-未读1-已读</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ReadEmail(string keyValue, int IsRead = 1)
        {
            emailContentBLL.ReadEmail(keyValue, IsRead);
            return Success("操作成功。");
        }
        /// <summary>
        /// 设置邮件星标/取消星标
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="asterisk">星标：0-取消星标1-星标</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SteriskEmail(string keyValue, int asterisk = 1)
        {
            emailContentBLL.SteriskEmail(keyValue, asterisk);
            return Success("操作成功。");
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        /// <returns></returns>
        public ActionResult FileUpload()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //如果没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            string originalName = files[0].FileName;
            string fileGuid = Guid.NewGuid().ToString();
            string uploadDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string fileEextension = Path.GetExtension(files[0].FileName);
            string userId = OperatorProvider.Provider.Current().UserId;
            string virtualPath = string.Format("/Resource/EmailFile/{0}{1}", userId + "_" + fileGuid, fileEextension);
            string fullFileName = Server.MapPath("~" + virtualPath);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);
            string nameSTR = Path.GetFileName(fullFileName);
            var JsonData = new
            {
                Success = true,
                Data = new { originalName = originalName, newName = nameSTR, path = virtualPath, uploadDate = uploadDate },
                SaveName = nameSTR
            };
            return Content(JsonData.ToJson());
        }
        #endregion



        /// <summary>
        /// 下载附件
        /// </summary>
        /// <param name="file"></param>
        [HttpPost]
        public void FileDownload(string file)
        {
            var data = JObject.Parse(Uri.UnescapeDataString(file));
            string filename = data["originalName"].ToString();//返回客户端文件名称
            string filepath = this.Server.MapPath(data["path"].ToString());
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
            //return Content("下载成功");
        }
    }
}
