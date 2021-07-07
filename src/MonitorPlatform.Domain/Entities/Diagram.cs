/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Diagram
*******类 说 明： 线路图配置表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:54:59 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
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
    public class Diagram:BaseEntity<Guid>
    {
        public Guid MonitorId { get; set;  }
        public Monitor Monitor { get; set; }   
        /// <summary>
        /// 图纸名称
        /// </summary>
        public string Name { get; set;  }
        /// <summary>
        /// 图纸的二进制数据
        /// </summary>
        public byte[] Data { get; set;  }
    }
}
