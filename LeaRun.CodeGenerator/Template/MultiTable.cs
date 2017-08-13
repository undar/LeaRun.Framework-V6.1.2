using LeaRun.CodeGenerator.Model;
using LeaRun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.CodeGenerator.Template
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.7.15 9:54
    /// 描 述：代码生成模板（多表）
    /// </summary>
    public class MultiTable
    {
        #region 实体类
        private string DataTypeSqlToNet(string dataType)
        {
            string res = "";
            switch (dataType)
            {
                case "varchar":
                    res = "string";
                    break;
                case "int":
                    res = "int?";
                    break;
                case "datetime":
                    res = "DateTime?";
                    break;
                case "decimal":
                    res = "decimal?";
                    break;
                default:
                    res = "string";
                    break;
            }
            return res;
        }
        /// <summary>
        /// 生成主从表实体类
        /// </summary>
        /// <param name="gridColumnModel"></param>
        /// <returns></returns>
        public string EntityBuilder(MultiTableConfigModel multiTableConfigModel, List<GridColumnModel> gridColumnModel, bool isChildTable)
        {
            string strCreateDate = "";
            string strCreateUserId = "";
            string strCreateUserName = "";
            string strModifyDate = "";
            string strModifyUserId = "";
            string strModifyUserName = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("using System;\r\n");
                sb.Append("using System.ComponentModel.DataAnnotations.Schema;\r\n");
                sb.Append("using LeaRun.Application.Code;\r\n\r\n");


                sb.Append("namespace LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + "\r\n");
                sb.Append("{\r\n");
                sb.Append("    /// <summary>\r\n");
                sb.Append("    /// 版 本\r\n");
                sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
                sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
                sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
                sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
                sb.Append("    /// </summary>\r\n");
                if (isChildTable)
                {
                    sb.Append("    public class " + multiTableConfigModel.ChildTableName + "Entity : BaseEntity\r\n");
                }
                else
                {
                    sb.Append("    public class " + multiTableConfigModel.EntityClassName + " : BaseEntity\r\n");
                }
                sb.Append("    {\r\n");
                sb.Append("        #region 实体成员\r\n");

                //实体字段
                foreach (var item in gridColumnModel)
                {
                    string column = item.name;
                    string remark = item.label;
                    sb.Append("        /// <summary>\r\n");
                    sb.Append("        /// " + remark + "\r\n");
                    sb.Append("        /// </summary>\r\n");
                    sb.Append("        /// <returns></returns>\r\n");
                    sb.Append("        [Column(\"" + column.ToString().ToUpper() + "\")]\r\n");
                    sb.Append("        public " + DataTypeSqlToNet(item.datatype) + " " + column + " { get; set; }\r\n"); //现在默认都是 string

                    #region 判断是否有创建人和修改人字段
                    if (column == "CreateDate")
                    {
                        strCreateDate = "this.CreateDate = DateTime.Now;\r\n";
                    }
                    if (column == "CreateUserId")
                    {
                        strCreateUserId = "this.CreateUserId = OperatorProvider.Provider.Current().UserId;\r\n";
                    }
                    if (column == "CreateUserName")
                    {
                        strCreateUserName = "this.CreateUserName = OperatorProvider.Provider.Current().UserName;\r\n";
                    }
                    if (column == "ModifyDate")
                    {
                        strModifyDate = "this.ModifyDate = DateTime.Now;\r\n";
                    }
                    if (column == "ModifyUserId")
                    {
                        strModifyUserId = "this.ModifyUserId = OperatorProvider.Provider.Current().UserId;\r\n";
                    }
                    if (column == "ModifyUserName")
                    {
                        strModifyUserName = "this.ModifyUserName = OperatorProvider.Provider.Current().UserName;\r\n";
                    }
                    #endregion
                }

                sb.Append("        #endregion\r\n\r\n");
                sb.Append("        #region 扩展操作\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 新增调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        public override void Create()\r\n");
                sb.Append("        {\r\n");
                if (isChildTable)
                {
                    sb.Append("            this." + multiTableConfigModel.ChildTablePk + " = Guid.NewGuid().ToString();\r\n");
                }
                else
                {
                    sb.Append("            this." + multiTableConfigModel.DataBaseTablePK + " = Guid.NewGuid().ToString();\r\n");
                }

                sb.Append("            " + strCreateDate + "");
                sb.Append("            " + strCreateUserId + "");
                sb.Append("            " + strCreateUserName + "");
                sb.Append("        }\r\n");
                sb.Append("        /// <summary>\r\n");
                sb.Append("        /// 编辑调用\r\n");
                sb.Append("        /// </summary>\r\n");
                sb.Append("        /// <param name=\"keyValue\"></param>\r\n");
                sb.Append("        public override void Modify(string keyValue)\r\n");
                sb.Append("        {\r\n");
                if (isChildTable)
                {
                    sb.Append("            this." + multiTableConfigModel.ChildTablePk + " = keyValue;\r\n");
                }
                else
                {
                    sb.Append("            this." + multiTableConfigModel.DataBaseTablePK + " = keyValue;\r\n");
                }
                sb.Append("            " + strModifyDate + "");
                sb.Append("            " + strModifyUserId + "");
                sb.Append("            " + strModifyUserName + "");
                sb.Append("        }\r\n");
                sb.Append("        #endregion\r\n");
                sb.Append("    }\r\n");
                sb.Append("}");
                return sb.ToString();

            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 实体映射类
        /// <summary>
        /// 生成主从表实体映射类
        /// </summary>
        /// <param name="multiTableConfigModel"></param>
        /// <param name="isChildTable"></param>
        /// <returns></returns>
        public string EntityMapBuilder(MultiTableConfigModel multiTableConfigModel, bool isChildTable)
        {
            try
            {
                string mapClassName = multiTableConfigModel.MapClassName;
                string EntityClassName = multiTableConfigModel.EntityClassName;
                string DataBaseTableName = multiTableConfigModel.DataBaseTableName;
                string DataBaseTablePK = multiTableConfigModel.DataBaseTablePK;
                if (isChildTable)
                {
                    mapClassName = multiTableConfigModel.ChildTableName + "Map";
                    EntityClassName = multiTableConfigModel.ChildTableName + "Entity";
                    DataBaseTableName = multiTableConfigModel.ChildTableName;
                    DataBaseTablePK = multiTableConfigModel.ChildTablePk;
                }


                StringBuilder sb = new StringBuilder();
                sb.Append("using LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + ";\r\n");
                sb.Append("using System.Data.Entity.ModelConfiguration;\r\n\r\n");

                sb.Append("namespace LeaRun.Application.Mapping." + multiTableConfigModel.OutputAreas + "\r\n");
                sb.Append("{\r\n");
                sb.Append("    /// <summary>\r\n");
                sb.Append("    /// 版 本\r\n");
                sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
                sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
                sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
                sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
                sb.Append("    /// </summary>\r\n");

                sb.Append("    public class " + mapClassName + " : EntityTypeConfiguration<" + EntityClassName + ">\r\n");

                sb.Append("    {\r\n");
                sb.Append("        public " + mapClassName + "()\r\n");
                sb.Append("        {\r\n");
                sb.Append("            #region 表、主键\r\n");
                sb.Append("            //表\r\n");
                sb.Append("            this.ToTable(\"" + DataBaseTableName + "\");\r\n");
                sb.Append("            //主键\r\n");
                sb.Append("            this.HasKey(t => t." + DataBaseTablePK + ");\r\n");
                sb.Append("            #endregion\r\n\r\n");

                sb.Append("            #region 配置关系\r\n");
                sb.Append("            #endregion\r\n");
                sb.Append("        }\r\n");
                sb.Append("    }\r\n");
                sb.Append("}\r\n");
                return sb.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 服务类
        /// <summary>
        /// 生成服务类
        /// </summary>
        /// <param name="multiTableConfigModel"></param>
        /// <param name="colModel">列数据对象集合</param>
        /// <returns></returns>
        public string ServiceBuilder(MultiTableConfigModel multiTableConfigModel, List<GridColumnModel> colModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Data.Repository;\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Linq;\r\n");
            sb.Append("using LeaRun.Util;\r\n");
            sb.Append("using LeaRun.Util.Extension;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Service." + multiTableConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + multiTableConfigModel.ServiceClassName + " : RepositoryFactory, " + multiTableConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");

            bool bIsQuery = false;
            StringBuilder sbQuery = new StringBuilder();
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
            //获取分页数据
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + multiTableConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");

            //添加查询条件
            if (bIsQuery)
            {
                sb.Append("            var expression = LinqExtensions.True<" + multiTableConfigModel.EntityClassName + ">();\r\n");
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
                sb.Append("            return this.BaseRepository().FindList<" + multiTableConfigModel.EntityClassName + ">(pagination);\r\n");
            }
            sb.Append("        }\r\n");
            //获取实体
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + multiTableConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return this.BaseRepository().FindEntity<" + multiTableConfigModel.EntityClassName + ">(keyValue);\r\n");
            sb.Append("        }\r\n");

            //获取子表详细信息
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取子表详细信息\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public IEnumerable<" + multiTableConfigModel.ChildTableName + "Entity> GetDetails(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return this.BaseRepository().FindList<" + multiTableConfigModel.ChildTableName + "Entity>(\"select * from " + multiTableConfigModel.ChildTableName + " where " + multiTableConfigModel.ChildTableForeignkey + "='\" + keyValue + \"'\");");
            //sb.Append("            return new RepositoryFactory<" + multiTableConfigModel.ChildTableName + "Entity>().BaseRepository().IQueryable(t => t." + multiTableConfigModel.ChildTableForeignkey + ".Equals(keyValue)).ToList();\r\n");
            sb.Append("        }\r\n");

            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 提交数据\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 删除数据\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键</param>\r\n");
            sb.Append("        public void RemoveForm(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                db.Delete<" + multiTableConfigModel.EntityClassName + ">(keyValue);\r\n");
            //20161118 zhaoxf
            sb.Append("                db.Delete<" + multiTableConfigModel.ChildTableName + "Entity>(t => t." + multiTableConfigModel.ChildTablePk + ".Equals(keyValue));\r\n");
            sb.Append("                db.Commit();\r\n");
            sb.Append("            }\r\n");
            sb.Append("            catch (Exception)\r\n");
            sb.Append("            {\r\n");
            sb.Append("                db.Rollback();\r\n");
            sb.Append("                throw;\r\n");
            sb.Append("            }\r\n");
            sb.Append("        }\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"entity\">实体对象</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public void SaveForm(string keyValue, " + multiTableConfigModel.EntityClassName + " entity,List<" + multiTableConfigModel.ChildTableName + "Entity> entryList)\r\n");
            sb.Append("        {\r\n");

            sb.Append("        IRepository db = this.BaseRepository().BeginTrans();\r\n");
            sb.Append("        try\r\n");
            sb.Append("        {\r\n");
            sb.Append("            if (!string.IsNullOrEmpty(keyValue))\r\n");
            sb.Append("            {\r\n");
            sb.Append("                //主表\r\n");
            sb.Append("                entity.Modify(keyValue);\r\n");
            sb.Append("                db.Update(entity);\r\n");
            sb.Append("                //明细\r\n");
            sb.Append("                db.Delete<" + multiTableConfigModel.ChildTableName + "Entity>(t => t." + multiTableConfigModel.ChildTableForeignkey + ".Equals(keyValue));\r\n");
            sb.Append("                foreach (" + multiTableConfigModel.ChildTableName + "Entity item in entryList)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    item.Create();\r\n");
            sb.Append("                    item." + multiTableConfigModel.ChildTableForeignkey + " = entity." + multiTableConfigModel.DataBaseTablePK + ";\r\n");
            sb.Append("                    db.Insert(item);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("            else\r\n");
            sb.Append("            {\r\n");
            sb.Append("                //主表\r\n");
            sb.Append("                entity.Create();\r\n");
            sb.Append("                db.Insert(entity);\r\n");
            sb.Append("                //明细\r\n");
            sb.Append("                foreach (" + multiTableConfigModel.ChildTableName + "Entity item in entryList)\r\n");
            sb.Append("                {\r\n");
            sb.Append("                    item.Create();\r\n");
            sb.Append("                    item." + multiTableConfigModel.ChildTableForeignkey + " = entity." + multiTableConfigModel.DataBaseTablePK + ";\r\n");
            sb.Append("                    db.Insert(item);\r\n");
            sb.Append("                }\r\n");
            sb.Append("            }\r\n");
            sb.Append("            db.Commit();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        catch (Exception)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            db.Rollback();\r\n");
            sb.Append("            throw;\r\n");
            sb.Append("        }\r\n");

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
        /// <param name="multiTableConfigModel"></param>
        /// <returns></returns>
        public string IServiceBuilder(MultiTableConfigModel multiTableConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.IService." + multiTableConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public interface " + multiTableConfigModel.IServiceClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        #region 获取数据\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        IEnumerable<" + multiTableConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson);\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        " + multiTableConfigModel.EntityClassName + " GetEntity(string keyValue);\r\n");

            //获取子表详细信息
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取子表详细信息\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        IEnumerable<" + multiTableConfigModel.ChildTableName + "Entity> GetDetails(string keyValue);\r\n");

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
            sb.Append("        void SaveForm(string keyValue, " + multiTableConfigModel.EntityClassName + " entity,List<" + multiTableConfigModel.ChildTableName + "Entity> entryList);\r\n");
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
        /// <param name="multiTableConfigModel"></param>
        /// <returns></returns>
        public string BusinesBuilder(MultiTableConfigModel multiTableConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.IService." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.Service." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Busines." + multiTableConfigModel.OutputAreas + "\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + multiTableConfigModel.BusinesClassName + "\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + multiTableConfigModel.IServiceClassName + " service = new " + multiTableConfigModel.ServiceClassName + "();\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取列表\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"pagination\">分页</param>\r\n");
            sb.Append("        /// <param name=\"queryJson\">查询参数</param>\r\n");
            sb.Append("        /// <returns>返回分页列表</returns>\r\n");
            sb.Append("        public IEnumerable<" + multiTableConfigModel.EntityClassName + "> GetPageList(Pagination pagination, string queryJson)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetPageList(pagination, queryJson);\r\n");
            sb.Append("        }\r\n");

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public " + multiTableConfigModel.EntityClassName + " GetEntity(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetEntity(keyValue);\r\n");
            sb.Append("        }\r\n");

            //获取子表详细信息
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取子表详细信息\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        public IEnumerable<" + multiTableConfigModel.ChildTableName + "Entity> GetDetails(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return service.GetDetails(keyValue);\r\n");
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
            sb.Append("        public void SaveForm(string keyValue, " + multiTableConfigModel.EntityClassName + " entity,List<" + multiTableConfigModel.ChildTableName + "Entity> entryList)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            try\r\n");
            sb.Append("            {\r\n");
            sb.Append("                service.SaveForm(keyValue, entity, entryList);\r\n");
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
        /// <param name="multiTableConfigModel"></param>
        /// <returns></returns>
        public string ControllerBuilder(MultiTableConfigModel multiTableConfigModel)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("using LeaRun.Application.Entity." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Application.Busines." + multiTableConfigModel.OutputAreas + ";\r\n");
            sb.Append("using LeaRun.Util;\r\n");
            sb.Append("using LeaRun.Util.WebControl;\r\n");
            sb.Append("using System.Collections.Generic;\r\n");
            sb.Append("using System.Web.Mvc;\r\n\r\n");

            sb.Append("namespace LeaRun.Application.Web.Areas." + multiTableConfigModel.OutputAreas + ".Controllers\r\n");
            sb.Append("{\r\n");
            sb.Append("    /// <summary>\r\n");
            sb.Append("    /// V0.0.1\r\n");
            sb.Append("    /// Copyright (c) 2013-2016 聚久信息技术有限公司\r\n");
            sb.Append("    /// 创 建：" + multiTableConfigModel.CreateUser + "\r\n");
            sb.Append("    /// 日 期：" + multiTableConfigModel.CreateDate + "\r\n");
            sb.Append("    /// 描 述：" + multiTableConfigModel.Description + "\r\n");
            sb.Append("    /// </summary>\r\n");
            sb.Append("    public class " + multiTableConfigModel.ControllerName + " : MvcControllerBase\r\n");
            sb.Append("    {\r\n");
            sb.Append("        private " + multiTableConfigModel.BusinesClassName + " " + multiTableConfigModel.BusinesClassName.ToLower() + " = new " + multiTableConfigModel.BusinesClassName + "();\r\n\r\n");

            sb.Append("        #region 视图功能\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 列表页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult " + multiTableConfigModel.IndexPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 表单页面\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult " + multiTableConfigModel.FormPageName + "()\r\n");
            sb.Append("        {\r\n");
            sb.Append("            return View();\r\n");
            sb.Append("        }\r\n");
            sb.Append("        #endregion\r\n\r\n");

            sb.Append("        #region 获取数据\r\n");

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
            sb.Append("            var data = " + multiTableConfigModel.BusinesClassName.ToLower() + ".GetPageList(pagination, queryJson);\r\n");
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

            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取实体 \r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns>返回对象Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult GetFormJson(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + multiTableConfigModel.BusinesClassName.ToLower() + ".GetEntity(keyValue);\r\n");
            sb.Append("            var childData = " + multiTableConfigModel.BusinesClassName.ToLower() + ".GetDetails(keyValue);\r\n");
            sb.Append("            var jsonData = new\r\n");
            sb.Append("            {\r\n");
            sb.Append("                entity = data,\r\n");
            sb.Append("                childEntity = childData\r\n");
            sb.Append("            };\r\n");
            sb.Append("            return ToJsonResult(jsonData);\r\n");
            sb.Append("        }\r\n");

            //获取子表详细信息
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 获取子表详细信息 \r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <returns>返回对象Json</returns>\r\n");
            sb.Append("        [HttpGet]\r\n");
            sb.Append("        public ActionResult GetDetailsJson(string keyValue)\r\n");
            sb.Append("        {\r\n");
            sb.Append("            var data = " + multiTableConfigModel.BusinesClassName.ToLower() + ".GetDetails(keyValue);\r\n");
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
            sb.Append("            " + multiTableConfigModel.BusinesClassName.ToLower() + ".RemoveForm(keyValue);\r\n");
            sb.Append("            return Success(\"删除成功。\");\r\n");
            sb.Append("        }\r\n");
            sb.Append("        /// <summary>\r\n");
            sb.Append("        /// 保存表单（新增、修改）\r\n");
            sb.Append("        /// </summary>\r\n");
            sb.Append("        /// <param name=\"keyValue\">主键值</param>\r\n");
            sb.Append("        /// <param name=\"strEntity\">实体对象</param>\r\n");
            sb.Append("        /// <param name=\"strChildEntitys\">子表对象集</param>\r\n");
            sb.Append("        /// <returns></returns>\r\n");
            sb.Append("        [HttpPost]\r\n");
            sb.Append("        [ValidateAntiForgeryToken]\r\n");
            sb.Append("        [AjaxOnly]\r\n");
            sb.Append("        public ActionResult SaveForm(string keyValue, string strEntity,string strChildEntitys)\r\n");
            sb.Append("        {\r\n");
            //sb.Append("            var entity = strEntity.Replace(\"&nbsp;\",\"\").ToObject<" + multiTableConfigModel.EntityClassName + ">();\r\n");
            sb.Append("            var entity = strEntity.ToObject<" + multiTableConfigModel.EntityClassName + ">();\r\n");
            sb.Append("            List<"+multiTableConfigModel.ChildTableName+"> childEntitys = strChildEntitys.ToList<" + multiTableConfigModel.ChildTableName + "Entity>();\r\n");
            sb.Append("            " + multiTableConfigModel.BusinesClassName.ToLower() + ".SaveForm(keyValue, entity, childEntitys);\r\n");
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
        public string IndexBuilder(MultiTableConfigModel multiTableConfigModel, List<GridColumnModel> gridColumnModel, List<GridColumnModel> gridChildColumnModel)
        {
            var areasUrl = multiTableConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(multiTableConfigModel.ControllerName, 10);
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
            sb.Append("                $('#gridTable').setGridHeight($(window).height() - 136.5);\r\n");
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

            sb.Append("            height: $(window).height() - 136.5,\r\n");
            sb.Append("            url: \"../../" + areasUrl + "/GetPageListJson\",\r\n");

            sb.Append("            datatype: \"json\",\r\n");
            sb.Append("            colModel: [\r\n");

            bool bIsQuery = false;  //是否查询 20161118 zhaoxf
            StringBuilder sbQuery = new StringBuilder();
            List<GridColumnModel> colModel = gridColumnModel;
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

            sb.Append("            viewrecords: true,\r\n");
            sb.Append("            rowNum: 30,\r\n");
            sb.Append("            rowList: [30, 50, 100],\r\n");
            sb.Append("            pager: \"#gridPager\",\r\n");
            sb.Append("            sortname: '" + multiTableConfigModel.DataBaseTablePK + "',\r\n");
            sb.Append("            sortorder: 'desc',\r\n");
            sb.Append("            rownumbers: true,\r\n");
            sb.Append("            shrinkToFit: false,\r\n");
            sb.Append("            gridview: true,\r\n");

            sb.Append("            onSelectRow: function () {\r\n");
            sb.Append("                selectedRowIndex = $('#' + this.id).getGridParam('selrow');\r\n");
            sb.Append("            },\r\n");
            sb.Append("            gridComplete: function () {\r\n");
            sb.Append("                $('#' + this.id).setSelection(selectedRowIndex, false);\r\n");
            sb.Append("            },\r\n");

            sb.Append("            subGrid: true,\r\n");
            sb.Append("            subGridRowExpanded: function (subgrid_id, row_id) {\r\n");
            sb.Append("                var keyValue = $gridTable.jqGrid('getRowData', row_id)['" + multiTableConfigModel.DataBaseTablePK + "'];\r\n");
            sb.Append("                var subgrid_table_id = subgrid_id + \"_t\";\r\n");
            sb.Append("                $(\"#\" + subgrid_id).html(\"<table id='\" + subgrid_table_id + \"'></table>\");\r\n");
            sb.Append("                $(\"#\" + subgrid_table_id).jqGrid({\r\n");
            sb.Append("                    url: \"../../" + areasUrl + "/GetDetailsJson\",\r\n");
            sb.Append("                    postData: { keyValue: keyValue },\r\n");
            sb.Append("                    datatype: \"json\",\r\n");
            sb.Append("                    height: \"100%\",\r\n");
            sb.Append("                    colModel: [\r\n");
            foreach (var item in gridChildColumnModel)
            {
                sb.Append("                { label: '" + item.label + "', name: '" + item.name + "', index: '" + item.name + "', width: " + item.width + ", align: '" + item.align + "',sortable: " + item.sortable.ToString().ToLower() + " " + IsShow_Field(item.hidden) + " },\r\n");
            }
            sb.Append("                    ],\r\n");
            sb.Append("                    caption: \"明细\",\r\n");
            sb.Append("                    rowNum: \"1000\",\r\n");
            sb.Append("                    rownumbers: true,\r\n");
            sb.Append("                    shrinkToFit: false,\r\n");
            sb.Append("                    gridview: true,\r\n");
            sb.Append("                    hidegrid: false\r\n");
            sb.Append("                });\r\n");
            sb.Append("            }\r\n");

            sb.Append("        });\r\n");

            #region 20161117 查询 zhaoxf
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
            #endregion

            sb.Append("    }\r\n");

            sb.Append("    //新增\r\n");
            sb.Append("    function btn_add() {\r\n");
            sb.Append("        dialogOpen({\r\n");
            sb.Append("            id: 'Form',\r\n");
            sb.Append("            title: '添加" + multiTableConfigModel.Description + "',\r\n");
            sb.Append("            url: '/" + areasUrl + "/" + multiTableConfigModel.FormPageName + "',\r\n");
            sb.Append("            width: '1000px',\r\n");
            sb.Append("            height: '750px',\r\n");
            sb.Append("            callBack: function (iframeId) {\r\n");
            sb.Append("                top.frames[iframeId].AcceptClick();\r\n");
            sb.Append("            }\r\n");
            sb.Append("        });\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //编辑\r\n");
            sb.Append("    function btn_edit() {\r\n");
            sb.Append("        var keyValue = $('#gridTable').jqGridRowValue('" + multiTableConfigModel.DataBaseTablePK + "');\r\n");
            sb.Append("        if (checkedRow(keyValue)) {\r\n");
            sb.Append("            dialogOpen({\r\n");
            sb.Append("                id: 'Form',\r\n");
            sb.Append("                title: '编辑" + multiTableConfigModel.Description + "',\r\n");
            sb.Append("                url: '/" + areasUrl + "/" + multiTableConfigModel.FormPageName + "?keyValue=' + keyValue,\r\n");
            sb.Append("                width: '1000px',\r\n");
            sb.Append("                height: '750px',\r\n");
            sb.Append("                callBack: function (iframeId) {\r\n");
            sb.Append("                    top.frames[iframeId].AcceptClick();\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //删除\r\n");
            sb.Append("    function btn_delete() {\r\n");
            sb.Append("        var keyValue = $('#gridTable').jqGridRowValue( '" + multiTableConfigModel.DataBaseTablePK + "');\r\n");
            sb.Append("        if (keyValue) {\r\n");
            sb.Append("            $.RemoveForm({\r\n");
            sb.Append("                url: '../../" + areasUrl + "/RemoveForm',\r\n");
            sb.Append("                param: { keyValue: keyValue },\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    $('#gridTable').trigger('reloadGrid');\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        } else {\r\n");
            sb.Append("            dialogMsg('请选择需要删除的" + multiTableConfigModel.Description + "！', 0);\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");
            sb.Append("    //详细\r\n");
            sb.Append("    function btn_details() {\r\n");
            sb.Append("        var keyValue = $('#gridTable').jqGridRowValue('" + multiTableConfigModel.DataBaseTablePK + "');\r\n");
            sb.Append("        if (checkedRow(keyValue)) {\r\n");
            sb.Append("            dialogOpen({\r\n");
            sb.Append("                id: 'Form',\r\n");
            sb.Append("                title: '详细" + multiTableConfigModel.Description + "',\r\n");
            sb.Append("                url: '/" + areasUrl + "/" + multiTableConfigModel.FormPageName + "?keyValue=' + keyValue+'&isDeltail=true',\r\n");
            sb.Append("                width: '1000px',\r\n");
            sb.Append("                height: '750px',\r\n");
            sb.Append("                btn:null,\r\n");
            sb.Append("                callBack: function (iframeId) {\r\n");
            sb.Append("                    top.frames[iframeId].AcceptClick();\r\n");
            sb.Append("                }\r\n");
            sb.Append("            })\r\n");
            sb.Append("        }\r\n");
            sb.Append("    }\r\n");

            sb.Append("</script>\r\n");
            sb.Append("<div class=\"titlePanel\">\r\n");
            if (bIsQuery)
            {
                sb.Append("    <div class=\"title-search\">\r\n");
                sb.Append("        <table>\r\n");
                sb.Append("            <tr>\r\n");
                //20161117 新增查询 zhaoxf
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
            sb.Append("            <a id=\"lr-replace\" class=\"btn btn-default\" onclick=\"reload()\"><i class=\"fa fa-refresh\"></i>刷新</a>\r\n");
            sb.Append("            <a id=\"lr-add\" class=\"btn btn-default\" onclick=\"btn_add()\"><i class=\"fa fa-plus\"></i>新增</a>\r\n");
            sb.Append("            <a id=\"lr-edit\" class=\"btn btn-default\" onclick=\"btn_edit()\"><i class=\"fa fa-pencil-square-o\"></i>编辑</a>\r\n");
            sb.Append("            <a id=\"lr-delete\" class=\"btn btn-default\" onclick=\"btn_delete()\"><i class=\"fa fa-trash-o\"></i>删除</a>\r\n");
            sb.Append("            <a id=\"lr-details\" class=\"btn btn-default\" onclick=\"btn_details()\"><i class=\"fa fa-list-alt\"></i>详细</a>\r\n");
            sb.Append("        </div>\r\n");
            sb.Append("    </div>\r\n");
            sb.Append("</div>\r\n");
            sb.Append("<div class=\"gridPanel\">\r\n");
            sb.Append("    <table id=\"gridTable\"></table>\r\n");
            sb.Append("    <div id=\"gridPager\"></div>\r\n");

            sb.Append("</div>\r\n");
            return sb.ToString();
        }
        #endregion

        #region 表单页
        /// <summary>
        /// 判断是否允许为空
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string isNullAble(string data)
        {
            if (data == "1")
            {
                return "";
            }
            else
            {
                return "isvalid=\"yes\" checkexpession=\"NotNull\"";
            }
        }
        private string isNullAbleToFont(string data)
        {
            if (data == "1")
            {
                return "";
            }
            else
            {
                return "<font face=\"宋体\">*</font>";
            }
        }
        /// <summary>
        /// 生成表单页
        /// </summary>
        /// <param name="baseConfigModel"></param>
        /// <returns></returns>
        public string FormBuilder(MultiTableConfigModel multiTableConfigModel, List<GridColumnModel> formPrimary, List<GridColumnModel> formDetails)
        {
            var areasUrl = multiTableConfigModel.OutputAreas + "/" + CommonHelper.DelLastLength(multiTableConfigModel.ControllerName, 10);
            StringBuilder sb = new StringBuilder();
            sb.Append("@{;\r\n");
            sb.Append("    ViewBag.Title = \"表单页面\";\r\n");
            sb.Append("    Layout = \"~/Views/Shared/_OrderForm.cshtml\";\r\n");
            sb.Append("}\r\n");
            sb.Append("<script>\r\n");
            sb.Append("    var keyValue = request('keyValue');\r\n");
            sb.Append("    $(function () {\r\n");
            sb.Append("        InitialPage();\r\n");
            sb.Append("        GetOrderEntryGrid();\r\n");
            sb.Append("        initControl();\r\n");
            sb.Append("    });\r\n");

            sb.Append("    //初始化页面\r\n");
            sb.Append("    function InitialPage() {;\r\n");
            sb.Append("        //resize重设(表格、树形)宽高\r\n");
            sb.Append("        $(window).resize(function (e) {\r\n");
            sb.Append("            window.setTimeout(function () {\r\n");
            sb.Append("                $('#gridTable').setGridWidth(($('.gridPanel').width()));\r\n");
            sb.Append("                $('#gridTable').setGridHeight($(window).height() - 240.5);\r\n");
            sb.Append("            }, 200);\r\n");
            sb.Append("            e.stopPropagation();\r\n");
            sb.Append("        });\r\n");
            sb.Append("    }\r\n");

            string ccolModel = "";
            string crowdata = "";
            string crowValue = "";
            string crowGetValue = "";
            string cname = "";
            foreach (var item in formDetails)
            {
                ccolModel += "{ label: '" + item.label + "', name: '" + item.name + "', width: " + item.width + ", align: '" + item.align + "', sortable: false, resizable: false " + IsShow_Field(item.hidden) + " },\r\n";
                crowdata += item.name + ": '<input name=\"" + item.name + "\" type=\"text\" class=\"editable center\" />',\r\n";
                crowValue += "$(this).find('input[name=\"" + item.name + "\"]').val(row." + item.name + ");\r\n";
                crowGetValue += item.name + ": $(this).find('input[name=\"" + item.name + "\"]').val(),\r\n";
                if (!item.hidden && cname == "")
                {
                    cname = item.name;
                }
            }

            sb.Append("    //加载明细表\r\n");
            sb.Append("    function GetOrderEntryGrid() {\r\n");
            sb.Append("        var $grid = $('#gridTable');\r\n");
            sb.Append("        $grid.jqGrid({\r\n");
            sb.Append("            unwritten: false,\r\n");
            sb.Append("            datatype: 'local',\r\n");
            sb.Append("            height: $(window).height() - 240.5,\r\n");
            sb.Append("            autowidth: true,\r\n");
            sb.Append("            colModel: [\r\n");
            //字段数据            
            sb.Append(ccolModel);
            sb.Append("            ],\r\n");
            sb.Append("            pager: false,\r\n");
            sb.Append("            rownumbers: true,\r\n");
            sb.Append("            shrinkToFit: false,\r\n");
            sb.Append("            gridview: true,\r\n");
            sb.Append("            footerrow: false,\r\n");
            sb.Append("            gridComplete: function () {\r\n");
            sb.Append("            }\r\n");
            sb.Append("        });\r\n");
            sb.Append("        //默认添加13行 空行\r\n");
            sb.Append("        for (var i = 1; i < 13; i++) {\r\n");
            sb.Append("            var rowdata = {\r\n");
            sb.Append(crowdata);
            sb.Append("            }\r\n");
            sb.Append("            $grid.jqGrid('addRowData', i, rowdata);\r\n");
            sb.Append("        };\r\n");
            sb.Append("    }\r\n");


            sb.Append("    //初始化控件\r\n");
            sb.Append("    function initControl() {\r\n");
            sb.Append("        //获取表单\r\n");
            sb.Append("        if (!!keyValue) {\r\n");
            sb.Append("            $.SetForm({\r\n");
            sb.Append("                url: \"../../" + areasUrl + "/GetFormJson\",\r\n");
            sb.Append("                param: { keyValue: keyValue },\r\n");
            sb.Append("                success: function (data) {\r\n");
            sb.Append("                    $(\"#form1\").SetWebControls(data.entity);\r\n");

            sb.Append("                    //明细\r\n");
            sb.Append("                    var childEntity = data.childEntity;\r\n");
            sb.Append("                    $('#gridTable').find('[role=row]').each(function (i) {\r\n");
            sb.Append("                        var row = childEntity[i - 1];\r\n");
            sb.Append("                        if (row != undefined) {\r\n");
            sb.Append(crowValue);
            sb.Append("                        }\r\n");
            sb.Append("                    });\r\n");


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
            sb.Append("        var childEntryJson = [];\r\n");
            sb.Append("        $('#gridTable').find('[role=row]').each(function (i) {\r\n");
            sb.Append("        if (!!$(this).find('input[name=\"" + cname + "\"]').val()) {\r\n");
            sb.Append("            childEntryJson.push({\r\n");
            sb.Append(crowGetValue);
            sb.Append("                 });\r\n");
            sb.Append("            }\r\n");
            sb.Append("        });\r\n");

            sb.Append("        $.SaveForm({\r\n");
            sb.Append("            url: \"../../" + areasUrl + "/SaveForm?keyValue=\" + keyValue,\r\n");
            sb.Append("            param: { \"strEntity\": JSON.stringify(postData), \"strChildEntitys\": JSON.stringify(childEntryJson) },\r\n");
            sb.Append("            loading: \"正在保存数据...\",\r\n");
            sb.Append("            success: function () {\r\n");
            sb.Append("                $.currentIframe().$(\"#gridTable\").trigger(\"reloadGrid\");\r\n");
            sb.Append("            }\r\n");
            sb.Append("        })\r\n");
            sb.Append("    }\r\n");
            sb.Append("</script>\r\n");

            sb.Append("<div class=\"bills\" >\r\n");
            sb.Append(" <div style=\"height:190px;overflow-y:auto;margin:10px 10px;\">\r\n");
            sb.Append("    <table class=\"form\" style=\"width: 100%;\">\r\n        <tr>\r\n");
            if (formPrimary != null)
            {
                int tdnum = 0;
                foreach (var item in formPrimary)
                {
                    if (tdnum == 4)
                    {
                        sb.Append("</tr>\r\n<tr>\r\n");
                        tdnum = 0;
                    }
                    sb.Append("<th class=\"formTitle\" style=\"width: 60px;\">" + item.label + isNullAbleToFont(item.isnullable) + "</th>\r\n");
                    sb.Append("<td class=\"formValue\"><input id=\"" + item.name + "\" type=\"text\"  class=\"form-control\" " + isNullAble(item.isnullable) + "></td>\r\n");
                    tdnum++;
                }
            }
            sb.Append("     </tr>\r\n");

            sb.Append("    </table>\r\n");
            sb.Append(" </div>\r\n");
            sb.Append(" <div class=\"gridPanel\" >\r\n");
            sb.Append("     <table id=\"gridTable\" ></table>\r\n");
            sb.Append(" </div>\r\n");
            sb.Append("</div>\r\n");

            sb.Append("<style>\r\n");
            sb.Append("    body {\r\n");
            sb.Append("        margin:0px;\r\n");
            sb.Append("    }\r\n");
            sb.Append("    .bills {\r\n");
            sb.Append("        overflow: hidden;\r\n");
            sb.Append("        border-radius: 0px;\r\n");
            sb.Append("        position: relative;\r\n");
            sb.Append("        background: #FFFFFF;\r\n");
            sb.Append("        border: 0px solid rgb(204, 204, 204);\r\n");
            sb.Append("        box-shadow:none;\r\n");
            sb.Append("        padding: 0px;\r\n");
            sb.Append("    }\r\n");
            sb.Append("    .ui-widget-content {\r\n");
            sb.Append("        border-left: 0px;\r\n");
            sb.Append("        border-right: 0px;\r\n");
            sb.Append("        border-bottom: 0px;\r\n");
            sb.Append("    }\r\n");
            sb.Append("</style>\r\n");
            return sb.ToString();
        }
        #endregion
    }
}
