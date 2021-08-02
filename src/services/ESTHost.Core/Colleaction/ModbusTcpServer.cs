/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： ModbusTcpServer
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 11:10:11 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Common;

using MonitorPlatform.Share.CacheItem;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  mobus tcp server
    /// </summary>
    public class ModbusTcpServer: ModbusBase
    {
        private ModbusTcpServer() { }
        public static ModbusTcpServer CreateModbus(CacheItemDevice item)
        {
            var tcpNet = new ModbusTcpServer();
            return tcpNet;
        }

        public override void BeginWrite(Guid terminalId)
        {
            throw new NotImplementedException();
        }

        public override void EndWrite(Guid terminalId)
        {
            throw new NotImplementedException();
        }

        public override void RefreshChache()
        {
            throw new NotImplementedException();
        }

        public override OperateResult<byte[]> RestartTerminal(byte[] command)
        {
            throw new NotImplementedException();
        }

        public override OperateResult<byte[]> SetSensorCount(byte[] sensor)
        {
            throw new NotImplementedException();
        }

        public override void StartCollection()
        {
            throw new NotImplementedException();
        }

        public override void StopCollection()
        {
            throw new NotImplementedException();
        }

        public override OperateResult<byte[]> WriteSensors(byte[] sensor)
        {
            throw new NotImplementedException();
        }
    }
}
