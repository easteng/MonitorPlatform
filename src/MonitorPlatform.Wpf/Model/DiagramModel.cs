/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： DiagramModel
*******类 说 明： 图纸的model 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/14/2021 9:44:10 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;

namespace MonitorPlatform.Wpf.Model
{
    public class DiagramModel : NotifyBase
    {
        private Guid monitorId { get; set; }
        public Guid MonitorId { get => monitorId; set { monitorId = value; this.DoNotify(); } }
        private MonitorModel monitor { get; set; }
        public MonitorModel Monitor { get => monitor; set { monitor = value; this.DoNotify(); } }
        /// <summary>
        /// 图纸名称
        /// </summary>
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }
        /// <summary>
        /// 描述
        /// </summary>
        private string desc { get; set; }
        public string Desc { get => desc; set { desc = value; this.DoNotify(); } }
        /// <summary>
        /// 图纸的二进制数据
        /// </summary>
        private byte[] data { get; set; }
        public byte[] Data { get => data; set { data = value; this.DoNotify(); } }
        /// <summary>
        /// 图纸的配置项
        /// </summary>

        private IEnumerable<DiagramConfigModel> configs { get; set; }
        public IEnumerable<DiagramConfigModel> Configs { get => configs; set { configs = value; this.DoNotify(); } }
    }
}
