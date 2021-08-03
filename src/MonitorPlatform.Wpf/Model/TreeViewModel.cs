/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： TreeViewModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/2/2021 5:02:55 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */

using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    /// <summary>
    ///  数据节点模型
    /// </summary>
    public class TreeViewModel: NotifyBase
    {
        /// <summary>
        /// 节点名称
        /// </summary>
        private string nodeName { get; set; }
        public string NodeName { get => nodeName; set { nodeName = value; this.DoNotify(); } }
        /// <summary>
        /// 节点id
        /// </summary>
        private Guid id { get; set; }
        public Guid Id { get => id; set { id = value; this.DoNotify(); } }

        private Guid parentId { get; set; }
        public Guid ParentId { get => parentId; set { parentId = value; this.DoNotify(); } }
        /// <summary>
        /// 节点类型
        /// </summary>
        private TreeNodeType nodeType { get; set; }
        public TreeNodeType NodeType { get => nodeType; set { nodeType = value; this.DoNotify(); } }
        /// <summary>
        /// 子节点
        /// </summary>
        private List<TreeViewModel> children { get; set; }   
        public List<TreeViewModel> Children { get => children; set { children = value; this.DoNotify(); } }
    }
}
