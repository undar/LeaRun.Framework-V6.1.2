using LeaRun.CodeGenerator.Model;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.CodeGenerator
{
    /// <summary>
    /// 版 本 6.3.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.3.1 9:54
    /// 描 述：自动创建代码
    /// </summary>
    public class CreateCodeFile
    {
        /// <summary>
        /// 执行创建文件
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <param name="strCode">生成代码内容</param>
        public static void CreateExecution(BaseConfigModel baseConfigModel, string strCode)
        {
            var strParam = strCode.ToJObject();

            #region 实体类
            string entityCode = strParam["entityCode"].ToString();
            string entityPath = baseConfigModel.OutputEntity + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.EntityClassName + ".cs";
            //if (!System.IO.File.Exists(entityPath))
            //{
                DirFileHelper.CreateFileContent(entityPath, entityCode);
            //}
            #endregion

            #region 映射类
            string entitymapCode = strParam["entitymapCode"].ToString();
            string entitymapPath = baseConfigModel.OutputMap + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.MapClassName + ".cs";
            //if (!System.IO.File.Exists(entitymapPath))
            //{
                DirFileHelper.CreateFileContent(entitymapPath, entitymapCode);
            //}
            #endregion

            #region 服务类
            string serviceCode = strParam["serviceCode"].ToString();
            string servicePath = baseConfigModel.OutputService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ServiceClassName + ".cs";
            //if (!System.IO.File.Exists(servicePath))
            //{
                DirFileHelper.CreateFileContent(servicePath, serviceCode);
            //}
            #endregion

            #region 接口类
            string iserviceCode = strParam["iserviceCode"].ToString();
            string iservicePath = baseConfigModel.OutputIService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.IServiceClassName + ".cs";
            //if (!System.IO.File.Exists(iservicePath))
            //{
                DirFileHelper.CreateFileContent(iservicePath, iserviceCode);
            //}
            #endregion

            #region 业务类
            string businesCode = strParam["businesCode"].ToString();
            string businesPath = baseConfigModel.OutputBusines + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.BusinesClassName + ".cs";
            //if (!System.IO.File.Exists(businesPath))
            //{
                DirFileHelper.CreateFileContent(businesPath, businesCode);
            //}
            #endregion

            #region 控制器
            string controllerCode = strParam["controllerCode"].ToString();
            string controllerPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Controllers\\" + baseConfigModel.ControllerName + ".cs";
            //if (!System.IO.File.Exists(controllerPath))
            //{
                DirFileHelper.CreateFileContent(controllerPath, controllerCode);
            //}
            #endregion

            #region 列表页
            string indexCode = strParam["indexCode"].ToString();
            string indexPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Views\\" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "\\" + baseConfigModel.IndexPageName + ".cshtml";
            //if (!System.IO.File.Exists(indexPath))
            //{
                DirFileHelper.CreateFileContent(indexPath, indexCode.Replace("★", "&nbsp;"));
            //}
            #endregion

            #region 表单页
            string formCode = strParam["formCode"].ToString();
            string formPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Views\\" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "\\" + baseConfigModel.FormPageName + ".cshtml";
            //if (!System.IO.File.Exists(formPath))
            //{
                DirFileHelper.CreateFileContent(formPath, formCode.Replace("★", "&nbsp;"));
            //}
            #endregion
        }
        /// <summary>
        /// 生成服务文件
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <param name="strCode">生成代码内容</param>
        public static void CreateServiceExecution(BaseConfigModel baseConfigModel, string strCode)
        {
            var strParam = strCode.ToJObject();

            #region 实体类
            string entityCode = strParam["entityCode"].ToString();
            string entityPath = baseConfigModel.OutputEntity + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.EntityClassName + ".cs";
            //if (!System.IO.File.Exists(entityPath))
            //{
            DirFileHelper.CreateFileContent(entityPath, entityCode);
            //}
            #endregion

            #region 映射类
            string entitymapCode = strParam["entitymapCode"].ToString();
            string entitymapPath = baseConfigModel.OutputMap + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.MapClassName + ".cs";
            //if (!System.IO.File.Exists(entitymapPath))
            //{
            DirFileHelper.CreateFileContent(entitymapPath, entitymapCode);
            //}
            #endregion

            #region 服务类
            string serviceCode = strParam["serviceCode"].ToString();
            string servicePath = baseConfigModel.OutputService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ServiceClassName + ".cs";
            //if (!System.IO.File.Exists(servicePath))
            //{
            DirFileHelper.CreateFileContent(servicePath, serviceCode);
            //}
            #endregion

            #region 接口类
            string iserviceCode = strParam["iserviceCode"].ToString();
            string iservicePath = baseConfigModel.OutputIService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.IServiceClassName + ".cs";
            //if (!System.IO.File.Exists(iservicePath))
            //{
            DirFileHelper.CreateFileContent(iservicePath, iserviceCode);
            //}
            #endregion

            #region 业务类
            string businesCode = strParam["businesCode"].ToString();
            string businesPath = baseConfigModel.OutputBusines + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.BusinesClassName + ".cs";
            //if (!System.IO.File.Exists(businesPath))
            //{
            DirFileHelper.CreateFileContent(businesPath, businesCode);
            //}
            #endregion

     
        }
        /// <summary>
        /// 执行创建文件(多表)
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <param name="strCode"></param>
        public static void CreateExecution(MultiTableConfigModel baseConfigModel, string strCode)
        {
            var strParam = strCode.ToJObject();

            #region 实体类
            string entityCode = strParam["entityCode"].ToString();
            string entityPath = baseConfigModel.OutputEntity + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.EntityClassName + ".cs";
            //if (!System.IO.File.Exists(entityPath))
            //{
                DirFileHelper.CreateFileContent(entityPath, entityCode);
            //}
            #endregion

            #region 实体子类
            string entityChildCode = strParam["entityChildCode"].ToString();
            string entityChildPath = baseConfigModel.OutputEntity + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ChildTableName + "Entity.cs";
            //if (!System.IO.File.Exists(entityChildPath))
            //{
                DirFileHelper.CreateFileContent(entityChildPath, entityChildCode);
            //}
            #endregion

            #region 映射类
            string entitymapCode = strParam["entitymapCode"].ToString();
            string entitymapPath = baseConfigModel.OutputMap + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.MapClassName + ".cs";
            //if (!System.IO.File.Exists(entitymapPath))
            //{
                DirFileHelper.CreateFileContent(entitymapPath, entitymapCode);
            //}
            #endregion

            #region 映射子类
            string entitymapChildCode = strParam["entitymapChildCode"].ToString();
            string entitymapChildPath = baseConfigModel.OutputMap + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ChildTableName + "Map.cs";
            //if (!System.IO.File.Exists(entitymapChildPath))
            //{
                DirFileHelper.CreateFileContent(entitymapChildPath, entitymapChildCode);
            //}
            #endregion

            #region 服务类
            string serviceCode = strParam["serviceCode"].ToString();
            string servicePath = baseConfigModel.OutputService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.ServiceClassName + ".cs";
            //if (!System.IO.File.Exists(servicePath))
            //{
                DirFileHelper.CreateFileContent(servicePath, serviceCode);
            //}
            #endregion

            #region 接口类
            string iserviceCode = strParam["iserviceCode"].ToString();
            string iservicePath = baseConfigModel.OutputIService + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.IServiceClassName + ".cs";
            //if (!System.IO.File.Exists(iservicePath))
            //{
                DirFileHelper.CreateFileContent(iservicePath, iserviceCode);
            //}
            #endregion

            #region 业务类
            string businesCode = strParam["businesCode"].ToString();
            string businesPath = baseConfigModel.OutputBusines + "\\" + baseConfigModel.OutputAreas + "\\" + baseConfigModel.BusinesClassName + ".cs";
            //if (!System.IO.File.Exists(businesPath))
            //{
                DirFileHelper.CreateFileContent(businesPath, businesCode);
            //}
            #endregion

            #region 控制器
            string controllerCode = strParam["controllerCode"].ToString();
            string controllerPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Controllers\\" + baseConfigModel.ControllerName + ".cs";
            //if (!System.IO.File.Exists(controllerPath))
            //{
                DirFileHelper.CreateFileContent(controllerPath, controllerCode);
            //}
            #endregion

            #region 列表页
            string indexCode = strParam["indexCode"].ToString();
            string indexPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Views\\" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "\\" + baseConfigModel.IndexPageName + ".cshtml";
            //if (!System.IO.File.Exists(indexPath))
            //{
                DirFileHelper.CreateFileContent(indexPath, indexCode.Replace("★", "&nbsp;"));
            //}
            #endregion

            #region 表单页
            string formCode = strParam["formCode"].ToString();
            string formPath = baseConfigModel.OutputController + "\\Areas\\" + baseConfigModel.OutputAreas + "\\Views\\" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10) + "\\" + baseConfigModel.FormPageName + ".cshtml";
            //if (!System.IO.File.Exists(formPath))
            //{
                DirFileHelper.CreateFileContent(formPath, formCode.Replace("★", "&nbsp;"));
            //}
            #endregion
        }
    }
}
