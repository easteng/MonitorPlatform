/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： CollectionServerFactory
*******类 说 明： 采集服务工厂，用来创建采集服务
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:48:35 AM
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
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///   采集服务工厂，用来创建采集服务，一个站点对应一个线程  一个线拥有各自的采集逻辑  并可复用
    ///   通过工厂快速创建采集服务
    /// </summary>
    public class CollectionServerFactory
    {
        private static Dictionary<Guid, ModbusBase> servicesDictionary;
        static CollectionServerFactory()
        {
            servicesDictionary = new Dictionary<Guid, ModbusBase>();
        }
        /// <summary>
        /// 根据协议所对应的设备数量创建数据采集服务服务   
        /// </summary>
        /// <param name="devices">关联当前协议的服务</param>
        /// <param name="name">一般为协议名称，用来决定数据的接收者</param>
        public static void CreateService(List<CacheItemDevice> devices, string name)
        {
            try
            {
                devices?.ForEach(device =>
                {
                    if (!servicesDictionary.TryGetValue(device.DeviceId, out var server))
                    {
                        // 不存在服务，则重新创建
                        if (device.Type == MonitorPlatform.Share.DeviceCollectionType.Server)
                            server = ModbusTcpNet.CreateModbus(device, name);
                        else
                            server = ModbusTcpServer.CreateModbus(device);
                        servicesDictionary.TryAdd(device.DeviceId, server);
                    }
                });
            }
            catch (Exception ex)
            {
                // 采集服务创建异常
                Console.WriteLine("服务创建失败");
            }
        }

        /// <summary>
        /// 开始写入操作
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="terminalId"></param>
        public static void StartWrite(Guid deviceId, Guid terminalId)
        {
            var server = servicesDictionary.GetValueOrDefault(deviceId);
            if (server != null)
            {
                server.BeginWrite(terminalId);
            }
        }

        /// <summary>
        /// 停止写入操作
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="termianlId"></param>
        public static void EndWrite(Guid deviceId, Guid termianlId)
        {
            var server = servicesDictionary.GetValueOrDefault(deviceId);
            if (server != null)
            {
                server.EndWrite(termianlId);
            }
        }
        /// <summary>
        /// 写入传感器信息
        /// </summary>
        /// <param name="deviceId">设备id</param>
        /// <param name="termianlId">终端采集器id</param>
        /// <param name="data">写入传感器完整的报文数据</param>
        /// <returns></returns>
        public static OperateResult<byte[]> WriteSensor(Guid deviceId, byte[] data)
        {
            var server = servicesDictionary.GetValueOrDefault(deviceId);
            if (server != null)
            {
                return server.WriteSensors(data);
            }
            return OperateResult.CreateFailedResult<byte[]>(null);
        }

        public static OperateResult<byte[]> SetSensorCount(Guid deviceId, byte[] data)
        {
            var server = servicesDictionary.GetValueOrDefault(deviceId);
            if (server != null)
            {
                return server.SetSensorCount(data);
            }
            return OperateResult.CreateFailedResult<byte[]>(new OperateResult());
        }

        /// <summary>
        /// 刷新服务缓存数据
        /// </summary>
        /// <param name="deviceId"></param>
        public static void Refresh(Guid deviceId)
        {
            var server = servicesDictionary.GetValueOrDefault(deviceId);
            if (server != null)
            {
                server.RefreshChache();
            }
        }
    }
}
