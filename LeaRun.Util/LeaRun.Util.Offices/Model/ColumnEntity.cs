using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.Offices
{
    /// <summary>
    ///版 本 V1.0
    ///Copyright (c) 2010-2015 聚久信息技术有限公司　　　　　　　　　　　　　　　　　　　　　　　　　　
    ///创建人：刘晓雷
    ///日 期：2015/11/25
    ///描 述：Excel导入导出列设置
    /// </summary>
    public class ColumnEntity
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// Excel列名
        /// </summary>
        public string ExcelColumn { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color Background { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public short Point { get; set; }
        /// <summary>
        /// 对齐方式
        ///left 左
        ///center 中间
        ///right 右
        ///fill 填充
        ///justify 两端对齐
        ///centerselection 跨行居中
        ///distributed
        /// </summary>
        public string Alignment { get; set; }
        
    }
}
