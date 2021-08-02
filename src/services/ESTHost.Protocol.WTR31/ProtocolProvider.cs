/**********************************************************************
*******命名空间： ESTHost.Protocol.WTR20A
*******类 名 称： ProtocolProvider
*******类 说 明： 协议提供者
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 2:08:15 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Caching;
using ESTCore.Common;
using ESTCore.Common.ModBus;
using ESTCore.Message.Handler;
using ESTCore.Message.Message;

using ESTHost.Core.Colleaction;
using ESTHost.ProtocolBase;

using Microsoft.Extensions.Logging;

using MonitorPlatform.Contracts;
using MonitorPlatform.Share;

using Silky.Lms.Core;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Protocol.WTR31
{
    /// <summary>
    ///  协议提供者 是该协议启动的入口,必须继承基础协议接口
    /// </summary>
    public class ProtocolProvider : IBaseProtocol
    {
        public string Name { get => "WTR31"; set => throw new NotImplementedException(); }
        private ILogger<ProtocolProvider> _logger;
        private IMessageServerProvider messageProvider;
        private NoticeMessage currentMessage;
        private IRedisCachingProvider redisCachingProvider;
        public async Task ExecuteAsync()
        {
            // 获取缓存数据，执行采集
            var devices = this.redisCachingProvider?.GetDevicesByProtocol(this.Name);
            if (devices != null && devices.Any())
                CollectionServerFactory.CreateService(devices, this.Name);
        }

        public Task StartAsync()
        {
            var serviceProvider = EngineContext.Current;
            this._logger = serviceProvider.Resolve<ILogger<ProtocolProvider>>();
            this.messageProvider = serviceProvider.Resolve<IMessageServerProvider>();
            this.redisCachingProvider = serviceProvider.Resolve<IRedisCachingProvider>();
            this.currentMessage = new NoticeMessage();
            _logger.LogInformation($"{this.Name} 协议已启动");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{this.Name} 协议已关闭");
            return Task.CompletedTask;
        }
        /// <summary>
        /// 写入传感器
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public async Task WriteSensor(Guid deviceId, Guid terminalId)
        {
            // 传感器写入，先停止已有的采集，->写入->重启
            CollectionServerFactory.StartWrite(deviceId, terminalId);

            // 执行操作
            // 获取要写入采集器的传感器信息
            var device = this.redisCachingProvider.GetDeviceInfoCache(deviceId);
            var sensors = this.redisCachingProvider.GetTerminalSensorCache(terminalId);
            var terminal = this.redisCachingProvider.GetTerminalInfo(terminalId);
            // 先设置探头的个数
            if (sensors != null && sensors.Any())
            {
                var command = GetSensorCountCommand((byte)terminal.Addr, (byte)sensors.Count());
                this._logger.LogInformation(command.ToHexString());
                // 先设置传感器个数
                var res = CollectionServerFactory.SetSensorCount(deviceId, command);
                if (res.IsSuccess)
                {
                    this._logger.LogInformation(res.Content.ToHexString());
                    sensors?.ForEach(a =>
                    {
                        var command = GetSensorCommand((byte)terminal.Addr, (byte)a.SensorNo, UInt32.Parse(a.SensorCode));
                        this._logger.LogInformation(command.ToHexString());
                        var call = CollectionServerFactory.WriteSensor(deviceId, command);
                        if (call.IsSuccess)
                        {
                            _logger.LogInformation("写入成功");
                        }
                        else
                        {
                            _logger.LogError("写入失败");
                        }
                        Thread.Sleep(100);
                    });

                    // 发送提示消息
                    currentMessage.Content = $"{device.Name}设备中{terminal.Addr}终端传感器写入成功";
                    currentMessage.Level = NoticeMessageLevel.Success;
                    await this.messageProvider.Publish(MessageTopic.Notice, BaseMessage.CreateMessage(currentMessage));
                }
            }
            // 结束写入
            CollectionServerFactory.EndWrite(deviceId, terminalId);
            //刷新数据
            CollectionServerFactory.Refresh(deviceId);
        }

        public Task Restart(Guid deviceId, Guid terminalId)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 获取发送命令
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="pno"></param>
        /// <param name="pId"></param>
        /// <returns></returns>
        private byte[] GetSensorCommand(byte addr, byte pno, UInt32 pId)
        {
            byte[] send_buf = new byte[13];
            send_buf[0] = addr;
            send_buf[1] = 0x10;

            ushort jcq_no = (ushort)(0x1000 + pno * 2);
            send_buf[2] = (byte)(jcq_no >> 8);
            send_buf[3] = (byte)jcq_no;

            send_buf[4] = 0x00;
            send_buf[5] = 0x02;
            send_buf[6] = 0x04;

            send_buf[7] = (byte)pId;
            send_buf[8] = (byte)(pId >> 8);
            send_buf[9] = (byte)(pId >> 16);
            send_buf[10] = (byte)(pId >> 24);

            return send_buf;
        }
        /// <summary>
        /// 获取写入的传感器条目
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="point_count"></param>
        /// <returns></returns>
        private byte[] GetSensorCountCommand(byte addr, byte point_count)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x06;
            send_buf[2] = 0x10;
            send_buf[3] = 0x01;
            send_buf[4] = 0x00;
            send_buf[5] = point_count;
            return send_buf;
        }
        /// <summary>
        /// 获取写入传感器的编号
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="point_no"></param>
        /// <returns></returns>
        private byte[] GetReadSensorCodeCommand(byte addr, byte point_no)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;

            ushort jcq_no = (ushort)(0x1000 + point_no * 2);
            send_buf[2] = (byte)(jcq_no >> 8);
            send_buf[3] = (byte)jcq_no;
            send_buf[4] = 0x00;
            send_buf[5] = 0x02;
            return send_buf;
        }
        /// <summary>
        /// 由16进制转换10进制的报文信息
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private uint GetSensorCode(byte[] buff)
        {
            uint code = 0;
            code = buff[3];
            code += (uint)(buff[4] * 0x100);
            code += (uint)(buff[5] * 0x10000);
            code += (uint)(buff[6] * 0x1000000);
            return code;
        }
    }
}
