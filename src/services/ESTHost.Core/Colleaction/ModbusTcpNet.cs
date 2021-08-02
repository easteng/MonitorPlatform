/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： TcpNetClient
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 11:09:18 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Common;
using ESTCore.Common.Core.Address;
using ESTCore.Common.ModBus;
using ESTCore.Common.Profinet.Freedom;

using MonitorPlatform.Contracts;
using MonitorPlatform.Share;
using MonitorPlatform.Share.CacheItem;

using Newtonsoft.Json;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  tcp/ip 协议，用于通信串口服务器   常用设备为NP301串口服务
    ///  客户端只发送标准的Modbus协议，并加crc16校验
    ///  由于站点比较多，客户端对应会有多个，为了满足需求，将客户端统一接口进行封装以便使用
    /// </summary>
    public class ModbusTcpNet : ModbusBase, IDisposable
    {
        private CacheItemDevice deviceItem;
        private ModbusTcpNet()
        {
            this.redisCachingProvider = EngineContext.Current.Resolve<IRedisCachingProvider>();
        }
        private Thread mainThread;
        private bool IsWorking; // 是否正在工作
        private bool IsStop;   // 是否停止采集
        private List<CacheItemTerminal> terminals;
        private FreedomTcpNet freedomTcp;
        private readonly IRedisCachingProvider redisCachingProvider;
        private IEventBus eventBus;
        private string protocolName;
        private bool canRead = true;
        public static ModbusTcpNet CreateModbus(CacheItemDevice item, string name)
        {
            var tcpNet = new ModbusTcpNet();
            tcpNet.deviceItem = item;
            tcpNet.protocolName = name;
            tcpNet.CreateTcpNet();
            tcpNet.StartCollection();
            return tcpNet;
        }

        /// <summary>
        /// 创建tcp客户端对象
        /// </summary>

        private void CreateTcpNet()
        {
            // 根据名称获取指定的消息总线地址,同种协议的数据只能自己接收，不能串行
            this.eventBus = EngineContext.Current.ResolveNamed<IEventBus>(this.protocolName);
            this.freedomTcp = new FreedomTcpNet(this.deviceItem.IpAddress, this.deviceItem.Port);
            freedomTcp.SleepTime = 200;
            freedomTcp.ReceiveTimeOut = 500;
        }
        /// <summary>
        /// 开启服务,服务进行采集，根据通道标记来设置是否继续读取数据，长轮询模式
        /// </summary>
        public override void StartCollection()
        {
            this.freedomTcp.ConnectServer(); // 连接服务
            this.terminals = this.redisCachingProvider?.GetTerminalsByDevice(this.deviceItem.DeviceId);
            this.IsWorking = true;
            mainThread = new Thread(() =>
             {
                 while (this.IsWorking)
                 {
                     if (terminals.Any())
                     {
                         if (this.IsStop) continue;
                         // 轮询执行采集命令，执行数据采集
                         Parallel.ForEach(terminals, terminal =>
                         {
                              // 采集器可用是才进行读取
                              if (canRead)
                              {
                                  if (terminal.SensorCount == 0) return;
                                  // 每次循环都要设置地址位，以便计算发送报文
                                  this.Station = (byte)terminal.Addr;
                                 // 创建，进行读取  有几个传感器，就读取多长的数据
                                 // 根据不同协议进行判断，31协议需要读取温度状态，和20a不相同
                                 ushort len = (ushort)terminal.SensorCount;
                                 if(protocolName == "WTR20A")
                                 {
                                     len *= 2;
                                 }
                                 var command = CreateReadRegisterCommand("00", len);
                                 if (command.IsSuccess)
                                 {
                                     var message = this.freedomTcp.Read(command.Content.ToHexString(), 0);
                                     if (message.IsSuccess)
                                     {
                                         if (message.Content.Length < 2) return;
                                         // 对获取到的数据进行校验一下，看是否为当前的采集采集到的数据
                                         var head = message.Content[0];
                                         var func = message.Content[1];
                                         if (head != this.Station && func != ModbusFunctionCode.ReadRegister)
                                             return;
                                         var res = new ReadCallbackMessage();
                                         res.Data = message.Content;
                                         res.DeviceId = this.deviceItem.DeviceId;
                                         res.Terminal = terminal;

                                         if (protocolName == "WTR31")
                                         {
                                             // 读取电池电压
                                             var statecommand = CreateReadStateCommand(this.Station, (ushort)terminal.SensorCount);
                                             var state = freedomTcp.Read(statecommand.Content.ToHexString(), 0);
                                             if(state.IsSuccess)
                                             {
                                                 res.StateData = state.Content;
                                             }
                                         }
                                         this.eventBus.ReceiverMateData(res);
                                        // Thread.Sleep(100);
                                     }
                                 }
                              }
                          });
                     }
                     Thread.Sleep(1000);
                 }
             });
            mainThread.Start();
        }

        /// <summary>
        /// 获取读取温度状态的命令
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="length"></param>
        /// <returns></returns>

        private OperateResult<byte[]> CreateReadStateCommand(byte addr, ushort length)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x01;
            send_buf[3] = 0x00;

            send_buf[4] = (byte)(length >> 8);
            send_buf[5] = (byte)length;

           return OperateResult.CreateSuccessResult<byte[]>(ModbusInfo.PackCommandToRtu(send_buf));
        }


        /// <summary>
        /// 设定读取温度报文的方法
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private OperateResult<byte[]> CreateReadRegisterCommand(string addr, ushort length)
        {
            // 实例化一个modbus 地址
            OperateResult<byte[][]> operateResult1 = ModbusInfo.BuildReadModbusCommand(addr, length, this.Station, true, ModbusFunctionCode.ReadRegister);
            if (!operateResult1.IsSuccess)
                return OperateResult.CreateFailedResult<byte[]>((OperateResult)operateResult1);
            // 添加校验
            var result = OperateResult.CreateSuccessResult<byte[]>(ModbusInfo.PackCommandToRtu(operateResult1.Content[0]));
            return result;
        }


        public override void StopCollection()
        {
            this.IsStop = true;
        }


        /// <summary>
        /// 刷新缓存数据
        /// </summary>
        public override void RefreshChache()
        {
            try
            {
                // 刷新数据
                this.terminals = this.redisCachingProvider?.GetTerminalsByDevice(this.deviceItem.DeviceId);
            }
            catch (Exception)
            {
                Console.WriteLine("数据刷新失败");
            }
        }
        /// <summary>
        /// 当前销毁
        /// </summary>
        public void Dispose()
        {
            this.IsWorking = false;
            this.StopCollection();
            try
            {
                if (this.mainThread != null && this.mainThread.IsAlive)
                {
                    this.mainThread.Abort();
                }
            }
            catch (PlatformNotSupportedException ex)
            {
                Console.WriteLine("服务已销毁！");
            }

        }

        /// <summary>
        /// 写入传感器id
        /// </summary>
        /// <param name="sensor">写入传感器的id号</param>
        /// <returns></returns>
        public override OperateResult<byte[]> WriteSensors(byte[] sensor)
        {
            // 获取校验后的命令
            var command = ModbusInfo.PackCommandToRtu(sensor).ToHexString();
            return this.freedomTcp.Read(command, 0);
        }

        /// <summary>
        /// 重启采集器终端
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override OperateResult<byte[]> RestartTerminal(byte[] data)
        {
            // 获取校验后的命令
            var command = ModbusInfo.PackCommandToRtu(data).ToString();
            return this.freedomTcp.Read(command, 0);
        }

        /// <summary>
        /// 开始数据写入操作
        /// </summary>
        /// <param name="terminalId"></param>
        public override void BeginWrite(Guid terminalId)
        {
            this.canRead = false;
            Thread.Sleep(100);
        }
        /// <summary>
        /// 结束数据写入操作
        /// </summary>
        /// <param name="terminalId"></param>
        public override void EndWrite(Guid terminalId)
        {
            this.canRead = true;
            Thread.Sleep(100);
        }
        /// <summary>
        /// 设置探头个数
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>

        public override OperateResult<byte[]> SetSensorCount(byte[] sensor)
        {
            var command = ModbusInfo.PackCommandToRtu(sensor).ToHexString();
            return this.freedomTcp.Read(command, 0);
        }
    }
}
