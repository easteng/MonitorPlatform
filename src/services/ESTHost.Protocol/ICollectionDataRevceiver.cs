/**********************************************************************
*******命名空间： ESTHost.ProtocolBase
*******接口名称： ICollectionDataRevceiver
*******接口说明： 采集数据接收
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/31/2021 12:28:07 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Share.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.ProtocolBase
{
    /// <summary>
    /// 各协议采集数据接收
    /// </summary>
    public interface ICollectionRepeater
    {
        abstract Task Receive(DeviceMessage deviceMessage);
    }
}
