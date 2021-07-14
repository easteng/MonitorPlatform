/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： MonitorModel
*******类 说 明： 监测点管理实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/14/2021 9:01:49 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
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
    public class MonitorModel : NotifyBase
    {
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }
        private StationType type { get; set; }
        public StationType Type { get => type; set { type = value; this.DoNotify(); } }

        private MonitorModel parent { get; set; }
        public MonitorModel Parent { get => parent; set { parent = value; this.DoNotify(); } }
        private Guid? parentId { get; set; }
        public Guid? ParentId { get => parentId; set { parentId = value; this.DoNotify(); } }
        private List<MonitorModel> children { get; set; }
        public List<MonitorModel> Children { get => children; set { children = value; this.DoNotify(); } }
        public bool IsExpanded { get; set; } = true;
    }
}
