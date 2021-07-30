/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： Protocol
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 4:30:54 PM
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

namespace MonitorPlatform.Domain
{
    /// <summary>
    ///  数据采集协议实体
    /// </summary>
    public class Protocol:BaseEntity<Guid>
    {
        public string Name { get; set; }
    }
}
