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
    public class ServiceClass
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
            sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n");
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
                    sb.Append("        /// <returns></returns>\r\n");//
                    sb.Append("        [Column(\""+column.ToUpper()+"\")]\r\n");
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
            sb.Append("            " + IsCreateDate(dt) + "\r\n");
            sb.Append("            " + IsCreateUserId(dt) + "\r\n");
            sb.Append("            " + IsCreateUserName(dt) + "\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 编辑调用\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
            sb.Append("        public override void Modify(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this." + baseConfigModel.DataBaseTablePK + " = keyValue;\r\n");
            sb.Append("            " + IsModifyDate(dt) + "\r\n");
            sb.Append("            " + IsModifyUserId(dt) + "\r\n");
            sb.Append("            " + IsModifyUserName(dt) + "\r\n");
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
          

            sb.Append("        #region 获取数据\r\n");
           
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表</returns>\r\n");
                sb.Append("        public IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
                sb.Append("        {\r\n");
                sb.Append("            return this.BaseRepository(\"BaseDb\",\"SqlServer\").FindList(pagination);\r\n");
                sb.Append("        }\r\n");
            
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
                sb.Append("            return this.BaseRepository(\"BaseDb\",\"SqlServer\").IQueryable().ToList();\r\n");
            }

            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + baseConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return this.BaseRepository(\"BaseDb\",\"SqlServer\").FindEntity(keyValue);\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            this.BaseRepository(\"BaseDb\",\"SqlServer\").Delete(keyValue);\r\n");
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
            sb.Append("                this.BaseRepository(\"BaseDb\",\"SqlServer\").Update(entity);\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity.Create();\r\n");
            sb.Append("                this.BaseRepository(\"BaseDb\",\"SqlServer\").Insert(entity);\r\n");
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
         
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 获取列表\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
                sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
                sb.Append("        /// <returns>返回分页列表</returns>\r\n");
                sb.Append("        IEnumerable<" + baseConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson);\r\n");
            
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

     
    }
}
