using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaRun.CodeGenerator.Model
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：undar
    /// 日 期：2016.7.15 9:54
    /// 描 述：多表基本信息配置
    /// </summary>
    public class MultiTableConfigModel
    {
        /// <summary>
        /// 数据库连接Id
        /// </summary>
        public string DataBaseLinkId { get; set; }
        /// <summary>
        /// 数据库表名称
        /// </summary>
        public string DataBaseTableName { get; set; }
        /// <summary>
        /// 数据库表主键
        /// </summary>
        public string DataBaseTablePK { get; set; }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 中文描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 实体类名
        /// </summary>
        public string EntityClassName { get; set; }
        /// <summary>
        /// 映射类名
        /// </summary>
        public string MapClassName { get; set; }
        /// <summary>
        /// 服务类名
        /// </summary>
        public string ServiceClassName { get; set; }
        /// <summary>
        /// 接口类名
        /// </summary>
        public string IServiceClassName { get; set; }
        /// <summary>
        /// 业务类名
        /// </summary>
        public string BusinesClassName { get; set; }
        /// <summary>
        /// 控制器名
        /// </summary>
        public string ControllerName { get; set; }
        /// <summary>
        /// 列表页名
        /// </summary>
        public string IndexPageName { get; set; }
        /// <summary>
        /// 表单页名
        /// </summary>
        public string FormPageName { get; set; }
        /// <summary>
        /// 输出所在区域
        /// </summary>
        public string OutputAreas { get; set; }
        /// <summary>
        /// 实体层输出目录
        /// </summary>
        public string OutputEntity { get; set; }
        /// <summary>
        /// 映射层输出目录
        /// </summary>
        public string OutputMap { get; set; }
        /// <summary>
        /// 服务层输出目录
        /// </summary>
        public string OutputService { get; set; }
        /// <summary>
        /// 接口层输出目录
        /// </summary>
        public string OutputIService { get; set; }
        /// <summary>
        /// 业务层输出目录
        /// </summary>
        public string OutputBusines { get; set; }
        /// <summary>
        /// 应用层输出目录
        /// </summary>
        public string OutputController { get; set; }

        /*子表信息*/
        /// <summary>
        /// 子表表名
        /// </summary>
        public string ChildTableName { get; set; }
        /// <summary>
        /// 子表主键
        /// </summary>
        public string ChildTablePk { get; set; }
        /// <summary>
        /// 子表关联字段名称
        /// </summary>
        public string ChildTableForeignkey { get; set; }
    }
}
