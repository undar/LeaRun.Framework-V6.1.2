namespace LeaRun.Application.Busines.AppManage
{
    using LeaRun.Application.Entity.AppManage;
    using LeaRun.Application.IService.AppManage;
    using LeaRun.Application.Service.AppManage;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Web;
    public class App_ProjectBLL
    {
        private App_ProjectIService service = new App_ProjectService();

        public App_ProjectEntity GetEntity(string keyValue)
        {
            return this.service.GetEntity(keyValue);
        }
        public void DownFile(string filename)
        {

            FileInfo file = new FileInfo(System.Web.HttpContext.Current.Server.MapPath("/templates/" + filename + ".zip"));//创建一个文件对象  
            System.Web.HttpContext.Current.Response.Clear();//清除所有缓存区的内容  
            System.Web.HttpContext.Current.Response.Charset = "GB2312";//定义输出字符集  
            System.Web.HttpContext.Current.Response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码  
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
            //添加头信息。为“文件下载/另存为”指定默认文件名称  
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", file.Length.ToString());
            //添加头文件，指定文件的大小，让浏览器显示文件下载的速度   
            System.Web.HttpContext.Current.Response.WriteFile(file.FullName);// 把文件流发送到客户端  
            System.Web.HttpContext.Current.Response.End();
        }
        public IEnumerable<App_ProjectEntity> GetList(string queryJson)
        {
            return this.service.GetList(queryJson);
        }

        public void RemoveForm(string keyValue)
        {
            try
            {
                this.service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveForm(string keyValue, App_ProjectEntity entity, List<App_TemplatesEntity> entryList)
        {
            try
            {
                this.service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

