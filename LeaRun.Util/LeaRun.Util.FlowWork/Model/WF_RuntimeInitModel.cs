using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaRun.Util.FlowWork
{
    /// <summary>
    /// V0.0.1
    /// Copyright (c) 2013-2016 聚久信息技术有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.04 16:58
    /// 描 述：工作流流程流转初始化模型类
    /// </summary>
    public class WF_RuntimeInitModel
    {
        /// <summary>
        /// GUID
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 工作流模板内容
        /// </summary>
        public string schemeContent { get; set; }
        /// <summary>
        /// 当前运行节点（默认开始节点）
        /// </summary>
        public string currentNodeId { get; set; }
        /// <summary>
        /// 提交的表单数据
        /// </summary>
        public string frmData { get; set; }
        /// <summary>
        /// 上一个节点
        /// </summary>
        public string previousId { get; set; }
    }
}
