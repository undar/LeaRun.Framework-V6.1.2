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
    /// 日 期：2016.1.15 9:54
    /// 描 述：表格信息
    /// </summary>
    public class GridModel
    {
        /// <summary>
        /// 工具栏按钮[刷新、新增、编辑、删除]
        /// </summary>
        public List<string> ButtonList { get; set; }
        /// <summary>
        /// 请求地址
        /// </summary>
        public string RequestUrl { get; set; }
        /// <summary>
        /// 是否分页
        /// </summary>
        public bool IsPage { get; set; }
        /// <summary>
        /// 显示行号
        /// </summary>
        public bool Rownumbers { get; set; }
    }
}
