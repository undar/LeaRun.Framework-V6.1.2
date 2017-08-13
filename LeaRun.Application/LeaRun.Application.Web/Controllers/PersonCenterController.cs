using LeaRun.Application.Busines.BaseManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.BaseManage;
using LeaRun.Util;
using LeaRun.Util.Extension;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LeaRun.Application.Web.Controllers
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2015.11.03 10:58
    /// 描 述：个人中心
    /// </summary>
    public class PersonCenterController : MvcControllerBase
    {
        private UserBLL userBLL = new UserBLL();

        #region 视图功能
        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.userId = OperatorProvider.Provider.Current().UserId;
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(new VerifyCode().GetVerifyCode(), @"image/Gif");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        public ActionResult UploadFile()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
            string FileEextension = Path.GetExtension(files[0].FileName);
            string UserId = OperatorProvider.Provider.Current().UserId;
            string virtualPath = string.Format("/Resource/PhotoFile/{0}{1}", UserId, FileEextension);
            string fullFileName = Server.MapPath("~" + virtualPath);
            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            UserEntity userEntity = new UserEntity();
            userEntity.UserId = OperatorProvider.Provider.Current().UserId;
            userEntity.HeadIcon = virtualPath;
            userBLL.SaveForm(userEntity.UserId, userEntity);
            return Success("上传成功。");
        }
        /// <summary>
        /// 验证旧密码
        /// </summary>
        /// <param name="OldPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ValidationOldPassword(string OldPassword)
        {
            OldPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Md5Helper.MD5(OldPassword, 32).ToLower(), OperatorProvider.Provider.Current().Secretkey).ToLower(), 32).ToLower();
            if (OldPassword != OperatorProvider.Provider.Current().Password)
            {

                return Error("原密码错误，请重新输入");
            }
            else
            {
                return Success("通过信息验证");
            }
        }
        /// <summary>
        /// 提交修改密码
        /// </summary>
        /// <param name="userId">当前用户Id</param>
        /// <param name="password">新密码</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="verifyCode">验证码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SubmitResetPassword(string password, string oldPassword, string verifyCode)
        {
            verifyCode = Md5Helper.MD5(verifyCode.ToLower(), 16);
            if (Session["session_verifycode"].IsEmpty() || verifyCode != Session["session_verifycode"].ToString())
            {
                return Error("验证码错误，请重新输入");
            }
            oldPassword = Md5Helper.MD5(DESEncrypt.Encrypt(oldPassword, OperatorProvider.Provider.Current().Secretkey).ToLower(), 32).ToLower();
            if (oldPassword != OperatorProvider.Provider.Current().Password)
            {
                return Error("原密码错误，请重新输入");
            }
            userBLL.RevisePassword(OperatorProvider.Provider.Current().UserId, password.ToLower());
            Session.Abandon(); Session.Clear();
            return Success("密码修改成功，请牢记新密码。\r 将会自动安全退出。");
        }
        #endregion
    }
}
