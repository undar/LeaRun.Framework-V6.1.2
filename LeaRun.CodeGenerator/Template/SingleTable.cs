using LeaRun.CodeGenerator.Comm;
using LeaRun.CodeGenerator.Model;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LeaRun.CodeGenerator.Template
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.1.15 9:54
    /// 描 述：代码生成模板（单表）
    /// </summary>
    public class SingleTable
    {
        #region 实体类
        /// <summary>
        /// 生成实体类
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <param name="dt">实体字段</param>
        /// <returns></returns>
        public string EntityBuilder(BaseConfigModel baseConfigModel, DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\r\n");
            sb.Append("using LeaRun.Application.Code;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Entity." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.EntityClassName + " : BaseEntity\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 实体成员\r\n");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow rowItem in dt.Rows)
                {
                    string column = rowItem["column"].ToString();
                    string remark = rowItem["remark"].ToString();
                    string datatype = CommHelper.FindModelsType(rowItem["datatype"].ToString());
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        public " + datatype + " " + column + " { get; set; }\r\n");
                }
            }
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 扩展操作\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 新增调用\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        public override void Create()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = Guid.NewGuid().ToString();\r\n");
            sb.Append("            " + IsCreateDate(dt) + "");
            sb.Append("            " + IsCreateUserId(dt) + "");
            sb.Append("            " + IsCreateUserName(dt) + "");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 编辑调用\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
            sb.Append("        public override void Modify(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = keyValue;\r\n");
            sb.Append("            " + IsModifyDate(dt) + "");
            sb.Append("            " + IsModifyUserId(dt) + "");
            sb.Append("            " + IsModifyUserName(dt) + "");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}");
            return sb.ToString();
        }
        public string IsCreateDate(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsCreateUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserId = OperatorProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsCreateUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'CreateUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.CreateUserName = OperatorProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }
        public string IsModifyDate(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyDate'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyDate = DateTime.Now;\r\n";
            }
            return "";
        }
        public string IsModifyUserId(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyUserId'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserId = OperatorProvider.Provider.Current().UserId;\r\n";
            }
            return "";
        }
        public string IsModifyUserName(DataTable dt)
        {
            DataTable newdt = DataHelper.DataFilter(dt, "column = 'ModifyUserName'");
            if (newdt.Rows.Count > 0)
            {
                return "this.ModifyUserName = OperatorProvider.Provider.Current().UserName;\r\n";
            }
            return "";
        }
        #endregion

        #region 实体映射类
        /// <summary>
        /// 生成实体映射类
        /// </summary>
        /// <param name="baseConfigModel">基本信息</param>
        /// <returns></returns>
        public string EntityMapBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Mapping." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// 版 本\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.MapClassName + " : EntityTypeConfiguration<" + baseConfigModel.EntityClassName + ">\r\n");
            sb.Append("    {\r\n");
            sb.Append("        public " + baseConfigModel.MapClassName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            #region 表、主键\r\n");
            sb.Append("            //表\r\n");
            sb.Append("            this.ToTable(\"" + baseConfigModel.DataBaseTableName + "\");\r\n");
            sb.Append("            //主键\r\n");
            sb.Append("            this.HasKey(t => t." + baseConfigModel.DataBaseTablePK + ");\r\n");
            sb.Append("            #endregion\r\n\r\n");

            sb.Append("            #region 配置关系\r\n");
            sb.Append("            #endregion\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 生成服务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string ServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Data.Repository;\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n\r\n");
            sb.Append("using LeaRun.Util;\r\n\r\n");
            sb.Append("using LeaRun.Util.Extension;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Service." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.ServiceClassName + " : RepositoryFactory<" + baseConfigModel.EntityClassName + ">, " + baseConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");

            bool bIsQuery = false;
            StringBuilder sbQuery = new StringBuilder();
            List<GridColumnModel> colModel = baseConfigModel.gridColumnModel;
            if (colModel != null)
            {
                foreach (GridColumnModel entity in colModel)
                {
                    if (entity.query == true)
                    {
                        bIsQuery = true;
                        sbQuery.Append("                    case \"" + entity.name + "\":              //" + entity.label + "\r\n");
                        sbQuery.Append("                        expression = expression.And(t => t." + entity.name + ".Contains(keyword));\r\n");
                        sbQuery.Append("                        break;\r\n");
                    }
                }
            }

            sb.Append("        #region 获取数据\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表</returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");

                //查询条件 
                if (bIsQuery)
                {
                    sb.Append("            var expression = LinqExtensions.True<" + baseConfigModel.EntityClassName + ">();\r\n");
                    sb.Append("            var queryParam = queryJson.ToJObject();\r\n");
                    sb.Append("            //查询条件\r\n");
                    sb.Append("            if (!queryParam[\"condition\"].IsEmpty() && !queryParam[\"keyword\"].IsEmpty())\r\n");
                    sb.Append("            {\r\n");
                    sb.Append("                string condition = queryParam[\"condition\"].ToString();\r\n");
                    sb.Append("                string keyword = queryParam[\"keyword\"].ToString();\r\n");
                    sb.Append("                switch (condition)\r\n");
                    sb.Append("                {\r\n");
                    sb.Append(sbQuery);
                    sb.Append("                    default:\r\n");
                    sb.Append("                        break;\r\n");
                    sb.Append("                }\r\n");
                    sb.Append("            }\r\n");
                    sb.Append("            return this.BaseRepository().FindList(expression,pagination);\r\n");
                }
                else
                {
                    sb.Append("            return this.BaseRepository().FindList(pagination);\r\n");
                }

                sb.Append("        }\r\n");
            }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            //查询条件 
            if (bIsQuery)
            {
                sb.Append("            var expression = LinqExtensions.True<" + baseConfigModel.EntityClassName + ">();\r\n");
                sb.Append("            var queryParam = queryJson.ToJObject();\r\n");
                sb.Append("            //查询条件\r\n");
                sb.Append("            if (!queryParam[\"condition\"].IsEmpty() && !queryParam[\"keyword\"].IsEmpty())\r\n");
                sb.Append("            {\r\n");
                sb.Append("                string condition = queryParam[\"condition\"].ToString();\r\n");
                sb.Append("                string keyword = queryParam[\"keyword\"].ToString();\r\n");
                sb.Append("                switch (condition)\r\n");
                sb.Append("                {\r\n");
                sb.Append(sbQuery);
                sb.Append("                    default:\r\n");
                sb.Append("                        break;\r\n");
                sb.Append("                }\r\n");
                sb.Append("            }\r\n");
                sb.Append("            return this.BaseRepository().IQueryable(expression).ToList();\r\n");
            }
            else
            {
                sb.Append("            return this.BaseRepository().IQueryable().ToList();\r\n");
            }

            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + baseConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return this.BaseRepository().FindEntity(keyValue);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this.BaseRepository().Delete(keyValue);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public void SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            if (!string.IsNullOrEmpty(keyValue))\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity.Modify(keyValue);\r\n");
            sb.Append("                this.BaseRepository().Update(entity);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity.Create();\r\n");
            sb.Append("                this.BaseRepository().Insert(entity);\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 服务接口类
        /// <summary>
        /// 生成服务接口类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IServiceBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.IService." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public interface " + baseConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 获取数据\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表</returns>\r\n");
                sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson);\r\n");
            }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson);\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        " + baseConfigModel.EntityClassName + " GetEntity(string keyValue);\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        void RemoveForm(string keyValue);\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        void SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity);\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 业务类
        /// <summary>
        /// 生成业务类
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string BusinesBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.Service." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Busines." + baseConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.BusinesClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.IServiceClassName + " service = new " + baseConfigModel.ServiceClassName + "();\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表</returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            return service.GetPageList(pagination, queryJson);\r\n");
                sb.Append("        }\r\n");
            }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetList(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetList(queryJson);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + baseConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetEntity(keyValue);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                service.RemoveForm(keyValue);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                throw;\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public void SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                service.SaveForm(keyValue, entity);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                throw;\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");

            return sb.ToString();
        }
        #endregion

        #region 控制器
        /// <summary>
        /// 生成控制器
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string ControllerBuilder(BaseConfigModel baseConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.Busines." + baseConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util;\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Web.Mvc;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Web.Areas." + baseConfigModel.OutputAreas + ".Controllers\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + baseConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + baseConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + baseConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + baseConfigModel.ControllerName + " : MvcControllerBase\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + baseConfigModel.BusinesClassName + " " + baseConfigModel.BusinesClassName.ToLower() + " = new " + baseConfigModel.BusinesClassName + "();\r\n\r\n");

            sb.Append("        #region 视图功能\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 列表页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult " + baseConfigModel.IndexPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 表单页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult " + baseConfigModel.FormPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页参数</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表Json</returns>\r\n");
                sb.Append("        [HttpGet]\r\n");
                sb.Append("        public ActionResult GetPageListJson(Pagination pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            var watch = CommonHelper.TimerStart();\r\n");
                sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetPageList(pagination, queryJson);\r\n");
                sb.Append("            var jsonData = new\r\n");
                sb.Append("            {\r\n");
                sb.Append("                rows = data,\r\n");
                sb.Append("                total = pagination.total,\r\n");
                sb.Append("                page = pagination.page,\r\n");
                sb.Append("                records = pagination.records,\r\n");
                sb.Append("                costtime = CommonHelper.TimerEnd(watch)\r\n");
                sb.Append("            };\r\n");
                sb.Append("            return ToJsonResult(jsonData);\r\n");
                sb.Append("        }\r\n");
            }
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回列表Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult GetListJson(string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetList(queryJson);\r\n");
            sb.Append("            return ToJsonResult(data);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体 \r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns>返回对象Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult GetFormJson(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + baseConfigModel.BusinesClassName.ToLower() + ".GetEntity(keyValue);\r\n");
            sb.Append("            return ToJsonResult(data);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [ValidateAntiForgeryToken]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        public ActionResult RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".RemoveForm(keyValue);\r\n");
            sb.Append("            return Success(\"删除成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [ValidateAntiForgeryToken]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        public ActionResult SaveForm(string keyValue, " + baseConfigModel.EntityClassName + " entity)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            " + baseConfigModel.BusinesClassName.ToLower() + ".SaveForm(keyValue, entity);\r\n");
            sb.Append("            return Success(\"操作成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n");
            sb.Append("    }\r\n");
            sb.Append("}\r\n");
            return sb.ToString();
        }
        #endregion

        #region 列表页
        /// <summary>
        /// 表头显示/隐藏
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public string IsShow_Field(bool field)
        {
            if (field == true)
            {
                return ",hidden: true";
            }
            return "";
        }
        /// <summary>
        /// 生成列表页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string IndexBuilder(BaseConfigModel baseConfigModel)
        {
            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();
            sb.Append("@{;\r\n");
            sb.Append("    ViewBag.Title = \"列表页面\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Index.cshtml\";\r\n");
            sb.Append("}\r\n");
            sb.Append("<script>;\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        InitialPage();\r\n");
            sb.Append("        GetGrid();\r\n");
            sb.Append("    });\r\n");
            sb.Append("    //初始化页面\r\n");
            sb.Append("    function InitialPage() {\r\n");
            sb.Append("        //resize重设布局;\r\n");
            sb.Append("        $(window).resize(function (e) {\r\n");
            sb.Append("            window.setTimeout(function () {\r\n");
            sb.Append("                $('#gridTable').setGridWidth(($('.gridPanel').width()));\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("                $('#gridTable').setGridHeight($(window).height() - 136.5);\r\n");
            }
            else
            {
                sb.Append("                $('#gridTable').setGridHeight($(window).height() - 108.5);\r\n");
            }
            sb.Append("            }, 200);\r\n");
            sb.Append("            e.stopPropagation();\r\n");
            sb.Append("        });\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //加载表格\r\n");
            sb.Append("    function GetGrid() {\r\n");
            sb.Append("        var selectedRowIndex = 0;\r\n");
            sb.Append("        var $gridTable = $('#gridTable');\r\n");
            sb.Append("        $gridTable.jqGrid({\r\n");
            sb.Append("            autowidth: true,\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("            height: $(window).height() - 136.5,\r\n");
                sb.Append("            url: \"../../" + areasUrl + "/GetPageListJson\",\r\n");
            }
            else
            {
                sb.Append("            height: $(window).height() - 108.5,\r\n");
                sb.Append("            url: \"../../" + areasUrl + "/GetListJson\",\r\n");
            }
            sb.Append("            datatype: \"json\",\r\n");
            sb.Append("            colModel: [\r\n");
            bool bIsQuery = false;
            StringBuilder sbQuery = new StringBuilder();
            List<GridColumnModel> colModel = baseConfigModel.gridColumnModel;
            if (colModel != null)
            {
                foreach (GridColumnModel entity in colModel)
                {
                    sb.Append("                { label: '" + entity.label + "', name: '" + entity.name + "', index: '" + entity.name + "', width: " + entity.width + ", align: '" + entity.align + "',sortable: " + entity.sortable.ToString().ToLower() + " " + IsShow_Field(entity.hidden) + " },\r\n");
                    if (entity.query == true)
                    {
                        bIsQuery = true;
                        sbQuery.Append("                            <li><a data-value=\"" + entity.name + "\">" + entity.label + "</a></li>\r\n");
                    }
                }
            }
            sb.Append("            ],\r\n");
           
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("            viewrecords: true,\r\n");
                sb.Append("            rowNum: 30,\r\n");
                sb.Append("            rowList: [30, 50, 100],\r\n");
                sb.Append("            pager: \"#gridPager\",\r\n");
                sb.Append("            sortname: '" + baseConfigModel.DataBaseTablePK + "',\r\n");
                sb.Append("            sortorder: 'desc',\r\n");
                sb.Append("            rownumbers: true,\r\n");
                sb.Append("            shrinkToFit: false,\r\n");
                sb.Append("            gridview: true,\r\n");
            }
            sb.Append("            onSelectRow: function () {\r\n");
            sb.Append("                selectedRowIndex = $('#' + this.id).getGridParam('selrow');\r\n");
            sb.Append("            },\r\n");
            sb.Append("            gridComplete: function () {\r\n");
            sb.Append("                $('#' + this.id).setSelection(selectedRowIndex, false);\r\n");
            sb.Append("            }\r\n");
            sb.Append("        });\r\n");

            //查询 
            if (bIsQuery)
            {
                sb.Append("        //查询条件\r\n");
                sb.Append("        $(\"#queryCondition .dropdown-menu li\").click(function () {\r\n");
                sb.Append("            var text = $(this).find('a').html();\r\n");
                sb.Append("            var value = $(this).find('a').attr('data-value');\r\n");
                sb.Append("            $(\"#queryCondition .dropdown-text\").html(text).attr('data-value', value)\r\n");
                sb.Append("        });\r\n");
                sb.Append("        //查询事件\r\n");
                sb.Append("        $(\"#btn_Search\").click(function () {\r\n");
                sb.Append("            var queryJson = {\r\n");
                sb.Append("                condition: $(\"#queryCondition\").find('.dropdown-text').attr('data-value'),\r\n");
                sb.Append("                keyword: $(\"#txt_Keyword\").val()\r\n");
                sb.Append("            }\r\n");
                sb.Append("            $gridTable.jqGrid('setGridParam', {\r\n");
                sb.Append("                postData: { queryJson: JSON.stringify(queryJson) },\r\n");
                sb.Append("                page: 1\r\n");
                sb.Append("            }).trigger('reloadGrid');\r\n");
                sb.Append("        });\r\n");
                sb.Append("        //查询回车\r\n");
                sb.Append("        $('#txt_Keyword').bind('keypress', function (event) {\r\n");
                sb.Append("            if (event.keyCode == \"13\") {\r\n");
                sb.Append("                $('#btn_Search').trigger(\"click\");\r\n");
                sb.Append("            }\r\n");
                sb.Append("        });\r\n");
            }

            sb.Append("    }\r\n");
            sb.Append("    //新增\r\n");
            sb.Append("    function btn_add() {\r\n");
            sb.Append("        dialogOpen({\r\n");
            sb.Append("            id: 'Form',\r\n");
            sb.Append("            title: '添加" + baseConfigModel.Description + "',\r\n");
            sb.Append("            url: '/" + areasUrl + "/" + baseConfigModel.FormPageName + "',\r\n");
            sb.Append("            width: '" + baseConfigModel.formModel.width + "px',\r\n");
            sb.Append("            height: '" + baseConfigModel.formModel.height + "px',\r\n");
            sb.Append("            callBack: function (iframeId) {\r\n");
            sb.Append("                top.frames[iframeId].AcceptClick();\r\n");
            sb.Append("            }\r\n");
            sb.Append("        });\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //编辑\r\n");
            sb.Append("    function btn_edit() {\r\n");
            sb.Append("        var keyValue = $('#gridTable').jqGridRowValue('" + baseConfigModel.DataBaseTablePK + "');\r\n");
            sb.Append("        if (checkedRow(keyValue)) {\r\n");
            sb.Append("            dialogOpen({\r\n");
            sb.Append("                id: 'Form',\r\n");
            sb.Append("                title: '编辑" + baseConfigModel.Description + "',\r\n");
            sb.Append("                url: '/" + areasUrl + "/" + baseConfigModel.FormPageName + "?keyValue=' + keyValue,\r\n");
            sb.Append("                width: '" + baseConfigModel.formModel.width + "px',\r\n");
            sb.Append("                height: '" + baseConfigModel.formModel.height + "px',\r\n");
            sb.Append("                callBack: function (iframeId) {\r\n");
            sb.Append("                    top.frames[iframeId].AcceptClick();\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //删除\r\n");
            sb.Append("    function btn_delete() {\r\n");
            sb.Append("        var keyValue = $('#gridTable').jqGridRowValue('" + baseConfigModel.DataBaseTablePK + "');\r\n");
            sb.Append("        if (keyValue) {\r\n");
            sb.Append("            $.RemoveForm({\r\n");
            sb.Append("                url: '../../" + areasUrl + "/RemoveForm',\r\n");
            sb.Append("                param: { keyValue: keyValue },\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    $('#gridTable').trigger('reloadGrid');\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        } else {\r\n");
            sb.Append("            dialogMsg('请选择需要删除的" + baseConfigModel.Description + "！', 0);\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("</script>\r\n");

            sb.Append("<div class=\"titlePanel\">\r\n");
            if (bIsQuery)
            {
                sb.Append("    <div class=\"title-search\">\r\n");
                sb.Append("        <table>\r\n");
                sb.Append("            <tr>\r\n");
                //查询
                sb.Append("                <td>\r\n");
                sb.Append("                    <div id=\"queryCondition\" class=\"btn-group\">\r\n");
                sb.Append("                        <a class=\"btn btn-default dropdown-text\" data-toggle=\"dropdown\">选择条件</a>\r\n");
                sb.Append("                        <a class=\"btn btn-default dropdown-toggle\" data-toggle=\"dropdown\"><span class=\"caret\"></span></a>\r\n");
                sb.Append("                        <ul class=\"dropdown-menu\">\r\n");
                sb.Append(sbQuery);
                sb.Append("                        </ul>\r\n");
                sb.Append("                     </div>\r\n");
                sb.Append("                </td>\r\n");

                sb.Append("                <td>\r\n");
                sb.Append("                    <input id=\"txt_Keyword\" type=\"text\" class=\"form-control\" placeholder=\"请输入要查询关键字\" style=\"width: 200px;\" />\r\n");
                sb.Append("                </td>\r\n");
                sb.Append("                <td style=\"padding-left: 5px;\">\r\n");
                sb.Append("                    <a id=\"btn_Search\" class=\"btn btn-primary\"><i class=\"fa fa-search\"></i>&nbsp;查询</a>\r\n");
                sb.Append("                </td>\r\n");
                sb.Append("            </tr>\r\n");
                sb.Append("        </table>\r\n");
                sb.Append("    </div>\r\n");
            }
            sb.Append("    <div class=\"toolbar\">\r\n");
            sb.Append("        <div class=\"btn-group\">\r\n");
            sb.Append("            <a id=\"lr-replace\" class=\"btn btn-default\" onclick=\"reload()\"><i class=\"fa fa-refresh\"></i>★刷新</a>\r\n");
            sb.Append("            <a id=\"lr-add\" class=\"btn btn-default\" onclick=\"btn_add()\"><i class=\"fa fa-plus\"></i>★新增</a>\r\n");
            sb.Append("            <a id=\"lr-edit\" class=\"btn btn-default\" onclick=\"btn_edit()\"><i class=\"fa fa-pencil-square-o\"></i>★编辑</a>\r\n");
            sb.Append("            <a id=\"lr-delete\" class=\"btn btn-default\" onclick=\"btn_delete()\"><i class=\"fa fa-trash-o\"></i>★删除</a>\r\n");
            sb.Append("        </div>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("<div class=\"gridPanel\">\r\n");
            sb.Append("    <table id=\"gridTable\"></table>\r\n");
            if (baseConfigModel.gridModel.IsPage == true)
            {
                sb.Append("    <div id=\"gridPager\"></div>\r\n");
            }
            sb.Append("</div>\r\n");
            return sb.ToString();
        }
        #endregion

        #region 表单页
        /// <summary>
        /// 生成表单页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string FormBuilder(BaseConfigModel baseConfigModel)
        {
            var areasUrl = baseConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(baseConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();
            List<FormFieldModel> fieldModel = baseConfigModel.formFieldModel;
            sb.Append("@{\r\n");
            sb.Append("    ViewBag.Title = \"表单页面\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_Form.cshtml\";\r\n");
            sb.Append("}\r\n");
            sb.Append("<script>\r\n");
            sb.Append("    var keyValue = request('keyValue');\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        initControl();\r\n");
            sb.Append("    });\r\n");
            sb.Append("    //初始化控件\r\n");
            sb.Append("    function initControl() {\r\n");
            //如果下拉框
            foreach (FormFieldModel entity in fieldModel)
            {
                if (entity.controlType=="select")
                {
                    if (entity.controlDataSource == "dataDb")
                    {
                        sb.Append("        $(\"#" + entity.controlId + "\").ComboBox({\r\n");
                        sb.Append("            url: \"../../SystemManage/DataSource/GetTableData?dbLinkId=" + entity.controlDataDb + "&tableName=" + entity.controlDataTable + "\",\r\n");
                        sb.Append("            id: \"" + entity.controlDataFliedId + "\",\r\n");
                        sb.Append("            text: \"" + entity.controlDataFliedText + "\",\r\n");
                        sb.Append("            height: '200px'\r\n");
                        sb.Append("        });\r\n");

                    }
                    else if (entity.controlDataSource == "dataItem")
                    {
                        sb.Append("        $(\"#" + entity.controlId + "\").ComboBox({\r\n");
                        sb.Append("            url: \"../../SystemManage/DataItemDetail/GetDataItemTreeJson?EnCode="+entity.controlDataItemEncodeValue+"\",\r\n");
                        sb.Append("            id: \"value\",\r\n");
                        sb.Append("            text: \"text\",\r\n");
                        sb.Append("            height: '200px'\r\n");
                        sb.Append("        });\r\n");

                    }
                }
            }
            sb.Append("        //获取表单\r\n");
            sb.Append("        if (!!keyValue) {\r\n");
            sb.Append("            $.SetForm({\r\n");
            sb.Append("                url: \"../../" + areasUrl + "/GetFormJson\",\r\n");
            sb.Append("                param: { keyValue: keyValue },\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    $(\"#form1\").SetWebControls(data);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //保存表单;\r\n");
            sb.Append("    function AcceptClick() {\r\n");
            sb.Append("        if (!$('#form1').Validform()) {\r\n");
            sb.Append("            return false;\r\n");
            sb.Append("        }\r\n");
            sb.Append("        var postData = $(\"#form1\").GetWebControls(keyValue);\r\n");
            sb.Append("        $.SaveForm({\r\n");
            sb.Append("            url: \"../../" + areasUrl + "/SaveForm?keyValue=\" + keyValue,\r\n");
            sb.Append("            param: postData,\r\n");
            sb.Append("            loading: \"正在保存数据...\",\r\n");
            sb.Append("            success: function () {\r\n");
            sb.Append("                $.currentIframe().$(\"#gridTable\").trigger(\"reloadGrid\");\r\n");
            sb.Append("            }\r\n");
            sb.Append("        })\r\n");
            sb.Append("    }\r\n");
            sb.Append("</script>\r\n");
            sb.Append("<div style=\"margin-top: 20px; margin-right: 30px;\">\r\n");
            sb.Append("    <table class=\"form\">\r\n        <tr>\r\n");
          
            if (fieldModel != null)
            {
                int clumnIndex = 1;//每行中第几列
                foreach (FormFieldModel entity in fieldModel)
                {
                    if (entity.controlColspan == 1)
                    {
                        sb.Append("            <td class=\"formTitle\">" + entity.controlName + "</td>\r\n");
                        sb.Append("            <td class=\"formValue\" colspan='3'>\r\n                " + CreateControl(entity) + "\r\n            </td>\r\n");
                        clumnIndex = 2;
                    }
                    else
                    {
                        sb.Append("            <td class=\"formTitle\">" + entity.controlName + "</td>\r\n");
                        sb.Append("            <td class=\"formValue\">\r\n                " + CreateControl(entity) + "\r\n            </td>\r\n");
                    }
                    if (baseConfigModel.formModel.FormType == 1)
                    {
                        sb.Append("        </tr>\r\n        <tr>\r\n");
                    }
                    else
                    {
                        if (clumnIndex == 2)
                        {
                            sb.Append("        </tr>\r\n        <tr>\r\n");
                            clumnIndex = 1;
                        }
                        else
                        {
                            clumnIndex = 2;
                        }
                    }
                }
            }
            sb.Remove(sb.Length - 7, 7);
            sb.Append("\r\n");
            sb.Append("    </table>\r\n");
            sb.Append("</div>\r\n");
            return sb.ToString();
        }
        /// <summary>
        /// 生成控件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CreateControl(FormFieldModel entity)
        {
            StringBuilder sbControl = new StringBuilder();
            string ControlName = entity.controlName;                            //属性名称
            string ControlId = entity.controlId;                                //控件Id
            string ControlType = entity.controlType;                            //控件类型
            string validator_html = "";
            if (!string.IsNullOrEmpty(entity.controlValidator.Trim()))
            {
                validator_html = "isvalid=\"yes\" checkexpession=\"" + entity.controlValidator.Trim() + "\"";
            }
            switch (ControlType)
            {
                case "input"://文本框
                    sbControl.Append("<input id=\"" + ControlId + "\" type=\"text\" class=\"form-control\" " + validator_html + " />");
                    break;
                case "select"://下拉框
                    sbControl.Append("<div id=\"" + ControlId + "\" type=\"select\" class=\"ui-select\" " + validator_html + "></div>");
                    break;
                case "datetime"://日期框
                    sbControl.Append("<input id=\"" + ControlId + "\" type=\"text\" class=\"form-control input-datepicker\" onfocus=\"WdatePicker()\" " + validator_html + "/>");
                    break;
                case "textarea"://多行文本框
                    sbControl.Append("<textarea id=\"" + ControlId + "\" class=\"form-control\" " + validator_html + "></textarea>");
                    break;
                default:
                    return "内部错误，配置有错误";
            }
            return sbControl.ToString();
        }
        #endregion
    }
}
