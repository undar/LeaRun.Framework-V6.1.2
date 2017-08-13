using LeaRun.Application.Busines.AuthorizeManage;
using LeaRun.Application.Busines.SystemManage;
using LeaRun.Application.Code;
using LeaRun.Application.Entity.AuthorizeManage;
using LeaRun.Application.Entity.SystemManage;
using LeaRun.CodeGenerator;
using LeaRun.CodeGenerator.Model;
using LeaRun.CodeGenerator.Template;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace LeaRun.Application.Web.Areas.GeneratorManage.Controllers
{
    
    public class ServiceCodeController : MvcControllerBase
    {
        private ModuleBLL moduleBLL = new ModuleBLL();
        /// <summary>
        /// 代码生成器
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CodeBuilder()
        {
            string OutputDirectory = Server.MapPath("~/Web.config"); ;
            for (int i = 0; i < 2; i++)
                OutputDirectory = OutputDirectory.Substring(0, OutputDirectory.LastIndexOf('\\'));
            ViewBag.OutputDirectory = OutputDirectory;
            ViewBag.UserName = OperatorProvider.Provider.Current().UserName;
            return View();
        }
        /// <summary>
        /// 查看代码
        /// </summary>
        /// <param name="baseInfoJson">基本信息配置Json</param>
        /// <param name="gridInfoJson">表格信息Json</param>
        /// <param name="gridColumnJson">表格字段信息Json</param>
        /// <param name="formInfoJson">表单信息Json</param>
        /// <param name="formFieldJson">表单字段信息Json</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookCode(BaseConfigModel r)
        {
            ServiceClass default_Template = new ServiceClass();
            BaseConfigModel baseConfigModel = r;

   

            var tableFiled = new DataBaseTableBLL().GetTableFiledList(baseConfigModel.DataBaseLinkId, baseConfigModel.DataBaseTableName);
            string entitybuilder = default_Template.EntityBuilder(baseConfigModel, DataHelper.ListToDataTable<DataBaseTableFieldEntity>(tableFiled.ToList()));
            string entitymapbuilder = default_Template.EntityMapBuilder(baseConfigModel);
            string servicebuilder = default_Template.ServiceBuilder(baseConfigModel);
            string iservicebuilder = default_Template.IServiceBuilder(baseConfigModel);
            string businesbuilder = default_Template.BusinesBuilder(baseConfigModel);

            var jsonData = new
            {
                entityCode = entitybuilder,
                entitymapCode = entitymapbuilder,
                serviceCode = servicebuilder,
                iserviceCode = iservicebuilder,
                businesCode = businesbuilder,

            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 创建代码（自动写入VS里面目录）
        /// </summary>
        /// <param name="baseInfoJson">基本信息配置Json</param>
        /// <param name="strCode">生成代码内容</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateCode(BaseConfigModel hi, string strCode)
        {
            BaseConfigModel baseConfigModel = hi;
            CreateCodeFile.CreateServiceExecution(baseConfigModel, Server.UrlDecode(strCode));
            return Success("恭喜您，创建成功！");
        }
    }
}