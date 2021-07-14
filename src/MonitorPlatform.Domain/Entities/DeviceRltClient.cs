﻿/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： DeviceRltClient
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/14/2021 12:00:53 AM
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
    public class DeviceRltClient : BaseEntity<Guid>
    {
        public DeviceRltClient() { }
        public DeviceRltClient(Guid deviceId, Guid clientId)
        {
            this.DeviceId = deviceId;
            this.ClientId = clientId;   
        }
        public CollectionClient Client { get; set; }
        public Guid ClientId { get; set; }
        public Device Device { get; set; }
        public Guid DeviceId { get; set; }
    }
}
