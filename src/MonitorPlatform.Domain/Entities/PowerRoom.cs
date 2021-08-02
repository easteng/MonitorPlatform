/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： PowerRoom
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/2/2021 3:51:26 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    ///  配电室
    /// </summary>
    public class PowerRoom : BaseEntity<Guid>
    {
        /// <summary>
        /// 配电室名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注描述
        /// </summary>
        public string Desc { get; set; }

        public Guid StationId { get; set; }
        public Station Station { get; set; }

        /// <summary>
        /// 配电室下的终端
        /// </summary>
        public List<Terminal> Terminals { get; set; }
    }
}
