/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： PowerRoomModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/2/2021 4:39:44 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    /// <summary>
    ///  配电室实体
    /// </summary>
    public class PowerRoomModel: NotifyBase
    {
        /// <summary>
        /// 配电室名称
        /// </summary>
        private string name { get; set; }
        public string Name { get => name; set { name = value; this.DoNotify(); } }
        /// <summary>
        /// 备注描述
        /// </summary>
        private string desc { get; set; }
        public string Desc { get => desc; set { desc = value; this.DoNotify(); } }

        private Guid stationId { get; set; }
        public Guid StationId { get => stationId; set { stationId = value; this.DoNotify(); } }
        /// <summary>
        /// 站点
        /// </summary>
        private StationModel station { get; set; }
        public StationModel Station { get => station; set { station = value; this.DoNotify(); } }

        /// <summary>
        /// 配电室下的终端
        /// </summary>
        private List<TerminalModel> terminals { get; set; }
        public List<TerminalModel> Terminals { get => terminals; set { terminals = value; this.DoNotify(); } }
    }
}
