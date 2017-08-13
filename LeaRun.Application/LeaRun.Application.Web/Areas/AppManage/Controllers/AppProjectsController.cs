namespace LeaRun.Application.Web.Areas.AppManage.Controllers
{
    using LeaRun.Application.Busines.AppManage;
    using LeaRun.Application.Entity.AppManage;
    using LeaRun.Application.Web;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ICSharpCode.SharpZipLib.Zip;
    using System.IO;
    using System.Data;
    using System.Text;
    using LeaRun.Util;
    using ICSharpCode.SharpZipLib.Checksums; 
  
    public class AppProjectsController : MvcControllerBase
    {
        private App_ProjectBLL app_projectbll = new App_ProjectBLL();
        private App_TemplatesBLL app_templatebll = new App_TemplatesBLL();

        public ActionResult DownFile(string filename)
        {
            app_projectbll.DownFile(filename);
            return Success("下载成功。");
        }
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            
            var data = this.app_projectbll.GetEntity(keyValue);
            var childData = app_templatebll.GetList(keyValue);
            var jsonData = new
            {
                project = data,
                templates = childData
            };
            return ToJsonResult(jsonData);
        }

        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            IEnumerable<App_ProjectEntity> list = this.app_projectbll.GetList(queryJson);
            return this.ToJsonResult(list);
        }

        [HttpGet]
        public ActionResult Index()
        {
            return base.View();
        }
        public ActionResult DownForm(string keyValue, App_ProjectEntity entity)
        {

            //先创建文件
            List<App_TemplatesEntity> strChildEntitys = entity.F_Templates;
            string filename = "";
            string PageTitle = "";
            string tempFId = "";
            string PageContent = "";
            string FilePath = "";
            StringBuilder sb0 = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            string zipFileName = entity.F_Name + DateTime.Now.ToString("yyyyMMddHHssmm");
            string zipFold = Server.MapPath("/templates/" + zipFileName+"/templates/");
            string zipFoldJs = Server.MapPath("/templates/" + zipFileName + "/js/");
            string zipTemplate = "/templates/" + zipFileName+"/templates/";
            string zipJs = "/templates/" + zipFileName + "/js/";
            string FID = "";
            if (!Directory.Exists(zipFold))
            {
                Directory.CreateDirectory(zipFold);
            }
            if (!Directory.Exists(zipFoldJs))
            {
                Directory.CreateDirectory(zipFoldJs);
            }
            //打包

            foreach (App_TemplatesEntity item in strChildEntitys)
            {
                App_ContentEntity c1 = item.F_Content.ToObject<App_ContentEntity>();
                if (item.F_Type == "Page")
                {
                    if (sb0.ToString() != "")
                    {
                        StringBuilder strHtml = new StringBuilder();
                        using (StreamReader sr = new StreamReader(Server.MapPath("/templates/app/PageTemplate.html"), Encoding.GetEncoding("utf-8")))
                        {
                            strHtml = strHtml.Append(sr.ReadToEnd());
                            sr.Close();
                        }
                        //替换开始
                        strHtml = strHtml.Replace("{PageTitle}", PageTitle);
                        strHtml = strHtml.Replace("{PageContent}", sb0.ToString());
                        //替换结束

                        FilePath = Server.MapPath(zipTemplate + filename + ".html");
                        System.IO.FileInfo finfo = new System.IO.FileInfo(FilePath);
                        using (FileStream fs = finfo.OpenWrite())
                        {
                            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                            //把新的内容写到创建的HTML页面中
                            sw.WriteLine(strHtml);
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    sb0.Remove(0, sb0.Length);
                    FID = item.F_Id;
                    filename = "page" + item.F_Id;
                    filename = filename.Replace("-", "");
                    tempFId = item.F_Id;
                    PageTitle = item.F_Name;
                }
                if (item.F_Type == "Component" && item.F_Value != "lrTab")
                {
                    if (item.F_Parent == tempFId)
                    {
                        if (item.F_Value == "lrHeader")
                        {
                            sb0.Append( "<" + c1.size + " style=\"color:" + c1.color + "; text-align:" + c1.align + ";\">" + c1.text + "</" + c1.size + ">");
                        }
                        else if (item.F_Value == "lrList3")
                        {
                            sb0.Append( "<div class=\"list lr-list-type3\"><ion-item class=\"item item-icon-left\"><i class=\"icon " + c1.icon + " " + c1.color + "\" ></i> <span>" + c1.name + "</span></ion-item></div>");
                        }
                        else if (item.F_Value == "lrList4")
                        {
                            sb0.Append("<div class=\"list lr-list-type3\"><ion-item class=\"item item-icon-left\"><i class=\"icon " + c1.icon + " " + c1.color + "\" ></i> <span>" + c1.name + "</span></ion-item></div>");
                        }
                        else if (item.F_Value=="lrInput")
                        {
                            sb0.Append("<label class=\"item item-input\"><input type=\"text\" placeholder=\"" + c1.placeholder + "\"></label>");
                        }
                        else if (item.F_Value == "lrParagraph")
                        {
                            sb0.Append("<p style=\"color:" + c1.color + ";font-size:" + c1.size + ";text-align:" + c1.align + ";\">" + c1.content + "</p>");
                        }
                        else if (item.F_Value == "lrBtn")
                        {
                            sb0.Append("<button style=\"color:" + c1.color + ";font-size:" + c1.size + ";text-align:" + c1.align + ";font-weight:500;\" class=\"button button-defaultbutton-standardbutton-positive\">" + c1.text + "</button>");
                        }
                      

                    }
                   
                }
                if (item.F_Value=="lrTabs")
                {
                    sb1.Append(".state('tab', {\r\n");
                    sb1.Append("url: '/tab',\r\n");
                    sb1.Append("abstract: true,\r\n");
                    sb1.Append("templateUrl: 'templates/tabs.html',\r\n");
                    sb1.Append("controller: 'lrTabsCtrl'\r\n");
                    sb1.Append("})");
                }
                else if (item.F_Value == "lrTab")
                {

                    sb1.Append(".state('tab"+item.F_Id+"', {\r\n");
                    sb1.Append("url: '/',\r\n");
                    sb1.Append("views: {\r\n");
                    sb1.Append("'tab-home': {\r\n");
                    sb1.Append("templateUrl: 'templates/.html'\r\n");
                    sb1.Append("}\r\n");
                    sb1.Append("}\r\n");
                    sb1.Append("})\r\n");
                    filename = "tabs";
                    if (item.F_Name == "首页")
                    {
                       

                        sb.Append("<ion-tab title=\"首页\" icon-on=\"ion-ios-home\" icon-off=\"ion-ios-home-outline\" href=\"#/tab/DefaultPage\">\r\n");
                        sb.Append("<ion-nav-view name=\"tab-" + item.F_Id + "\"></ion-nav-view>");
                        sb.Append("</ion-tab>\r\n");
                    }
                    else if (item.F_Name == "实例")
                    {
                        sb.Append("<ion-tab title=\"实例\" icon-on=\"ion-ios-book\" icon-off=\"ion-ios-book-outline\" href=\"#/tab/DefaultPage\">\r\n");
                        sb.Append("<ion-nav-view name=\"tab-" + item.F_Id + "\"></ion-nav-view>");
                        sb.Append("</ion-tab>\r\n");
                    }
                    else if (item.F_Name == "通知")
                    {
                        sb.Append("<ion-tab title=\"通知\" icon-on=\"ion-ios-bell\" icon-off=\"ion-ios-bell-outline\" href=\"#/tab/DefaultPage\">\r\n");
                        sb.Append("<ion-nav-view name=\"tab-" + item.F_Id + "\"></ion-nav-view>");
                        sb.Append("</ion-tab>\r\n");
                    }
                    else if (item.F_Name == "我的")
                    {
                        sb.Append("<ion-tab title=\"我的\" icon-on=\"ion-ios-person\" icon-off=\"ion-ios-person-outline\" href=\"#/tab/DefaultPage\">\r\n");
                        sb.Append("<ion-nav-view name=\"tab-" + item.F_Id + "\"></ion-nav-view>");
                        sb.Append("</ion-tab>\r\n");
                    }
                }

            }
          
            StringBuilder strHtml1 = new StringBuilder();
            using (StreamReader sr = new StreamReader(Server.MapPath("/templates/app/tabs.html"), Encoding.GetEncoding("utf-8")))
            {
                strHtml1 = strHtml1.Append(sr.ReadToEnd());
                sr.Close();
            }
            //替换开始

            strHtml1 = strHtml1.Replace("{PageContent}", sb.ToString());
            //替换结束

            FilePath = Server.MapPath(zipTemplate + filename + ".html");
            System.IO.FileInfo finfo1 = new System.IO.FileInfo(FilePath);
            using (FileStream fs = finfo1.OpenWrite())
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //把新的内容写到创建的HTML页面中
                sw.WriteLine(strHtml1);
                sw.Flush();
                sw.Close();
            }
            //JS替换
            //1.
            strHtml1.Remove(0, strHtml1.Length);
            //System.IO.File.Copy(Server.MapPath("/templates/app/learun-app.js"), Server.MapPath(zipJs));

            using (StreamReader sr = new StreamReader(Server.MapPath("/templates/app/learun-app.js"), Encoding.GetEncoding("utf-8")))
            {
                strHtml1 = strHtml1.Append(sr.ReadToEnd());
                sr.Close();
            }
            FilePath = Server.MapPath(zipJs + "learun-app.js");
            finfo1 = new System.IO.FileInfo(FilePath);
            using (FileStream fs = finfo1.OpenWrite())
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //把新的内容写到创建的HTML页面中
                sw.WriteLine(strHtml1);
                sw.Flush();
                sw.Close();
            }
            //2.
            strHtml1.Remove(0, strHtml1.Length);

            using (StreamReader sr = new StreamReader(Server.MapPath("/templates/app/learun-controllers.js"), Encoding.GetEncoding("utf-8")))
            {
                strHtml1 = strHtml1.Append(sr.ReadToEnd());
                sr.Close();
            }
            //替换开始

            strHtml1 = strHtml1.Replace("{FID}", FID);
            //替换结束

            FilePath = Server.MapPath(zipJs + "learun-controllers.js");
            finfo1 = new System.IO.FileInfo(FilePath);
            using (FileStream fs = finfo1.OpenWrite())
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //把新的内容写到创建的HTML页面中
                sw.WriteLine(strHtml1);
                sw.Flush();
                sw.Close();
            }
            //3.
            strHtml1.Remove(0, strHtml1.Length);
            strHtml1 = new StringBuilder();           
            using (StreamReader sr = new StreamReader(Server.MapPath("/templates/app/learun-uirouter.js"), Encoding.GetEncoding("utf-8")))
            {
                strHtml1 = strHtml1.Append(sr.ReadToEnd());
                sr.Close();
            }
            //替换开始

            strHtml1 = strHtml1.Replace("{RouterStr}", sb1.ToString());
            //替换结束

            FilePath = Server.MapPath(zipJs + "learun-uirouter.js");
            finfo1 = new System.IO.FileInfo(FilePath);
            using (FileStream fs = finfo1.OpenWrite())
            {
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                //把新的内容写到创建的HTML页面中
                sw.WriteLine(strHtml1);
                sw.Flush();
                sw.Close();
            }
            ZipDirectory(Server.MapPath("/templates/" + zipFileName), Server.MapPath("/templates/"), "", false);
            var fieldItem = new
            {
                path = zipFileName,
               
            };
            return this.ToJsonResult(fieldItem);

        }
      
         /// <summary>
         /// ZIP：压缩文件夹
         /// add yuangang by 2016-06-13
         /// </summary>
         /// <param name="DirectoryToZip">需要压缩的文件夹（绝对路径）</param>
         /// <param name="ZipedPath">压缩后的文件路径（绝对路径）</param>
         /// <param name="ZipedFileName">压缩后的文件名称（文件名，默认 同源文件夹同名）</param>
         /// <param name="IsEncrypt">是否加密（默认 加密）</param>
         public static void ZipDirectory(string DirectoryToZip, string ZipedPath, string ZipedFileName = "", bool IsEncrypt = true)
         {
             //如果目录不存在，则报错
             if (!System.IO.Directory.Exists(DirectoryToZip))
             {
                 throw new System.IO.FileNotFoundException("指定的目录: " + DirectoryToZip + " 不存在!");
             }
 
             //文件名称（默认同源文件名称相同）
             string ZipFileName = string.IsNullOrEmpty(ZipedFileName) ? ZipedPath + "\\" + new DirectoryInfo(DirectoryToZip).Name + ".zip" : ZipedPath + "\\" + ZipedFileName + ".zip";
 
             using (System.IO.FileStream ZipFile = System.IO.File.Create(ZipFileName))
             {
                 using (ZipOutputStream s = new ZipOutputStream(ZipFile))
                 {
                     if (IsEncrypt)
                     {
                         //压缩文件加密
                         s.Password = "123";
                     }
                     ZipSetp(DirectoryToZip, s, "");
                 }
             }
         }
         /// <summary>
         /// 递归遍历目录
         /// </summary>
         private static void ZipSetp(string strDirectory, ZipOutputStream s, string parentPath)
         {
             if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
             {
                 strDirectory += Path.DirectorySeparatorChar;
             }
             Crc32 crc = new Crc32();
 
             string[] filenames = Directory.GetFileSystemEntries(strDirectory);
 
             foreach (string file in filenames)// 遍历所有的文件和目录
             {
 
                 if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                 {
                     string pPath = parentPath;
                     pPath += file.Substring(file.LastIndexOf("\\") + 1);
                     pPath += "\\";
                     ZipSetp(file, s, pPath);
                 }
 
                 else // 否则直接压缩文件
                 {
                     //打开压缩文件
                     using (FileStream fs =System.IO.File.OpenRead(file))
                     {
 
                         byte[] buffer = new byte[fs.Length];
                         fs.Read(buffer, 0, buffer.Length);
 
                         string fileName = parentPath + file.Substring(file.LastIndexOf("\\") + 1);
                         ZipEntry entry = new ZipEntry(fileName);
 
                         entry.DateTime = DateTime.Now;
                         entry.Size = fs.Length;
 
                         fs.Close();
 
                         crc.Reset();
                         crc.Update(buffer);
 
                         entry.Crc = crc.Value;
                         s.PutNextEntry(entry);
 
                         s.Write(buffer, 0, buffer.Length);
                     }
                 }
             }
         }
   
        [AjaxOnly(false), HttpPost, ValidateAntiForgeryToken]
        public ActionResult RemoveForm(string keyValue)
        {
            this.app_projectbll.RemoveForm(keyValue);
            return this.Success("删除成功。");
        }

        [HttpPost]
        public ActionResult SaveForm(string keyValue, App_ProjectEntity entity)
        {

            List<App_TemplatesEntity> strChildEntitys = entity.F_Templates;
            app_projectbll.SaveForm(keyValue, entity, strChildEntitys);
            
            

            return this.Success("操作成功。");
        }
    }
}

