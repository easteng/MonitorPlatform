/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： CollectionServices
*******类 说 明： 采集服务定义
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:51:59 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using EasyCaching.Core;

using ESTCore.Common;
using ESTCore.Common.ModBus;

using MonitorPlatform.Share.ServerCache;

using Newtonsoft.Json;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  采集服务定义
    /// </summary>
    public class CollectionServices : IDisposable
    {
        // 服务名称  一般是站点的id
        public string Name { get; set; }
        /// <summary>
        /// 采集终端列表
        /// </summary>

        public CollectionServerCacheItem Server { get; set; }
        private List<TerminalCacheItem> Terminals { get; set; }
        private ModbusTcpNet modbus { get; set; }
        private bool IsConnected { get; set; } = false;
        /// <summary>
        /// 转换服务线程
        /// </summary>
        private Thread ServerThread { get; set; }

        /// <summary>
        /// 是否正在工作
        /// </summary>
        private bool IsWorking { get; set; }

        private readonly IRedisCachingProvider redisCachingProvider;
        private readonly IEventBus eventBus;
        public CollectionServices()
        {
            this.eventBus = EngineContext.Current.Resolve<IEventBus>();
            this.redisCachingProvider = EngineContext.Current.Resolve<IRedisCachingProvider>();
            // 初始化modbus 客户端
            //this.modbus = new ModbusTcpNet();
            //this.modbus.ConnectTimeOut = 2000;
            //this.modbus.ReceiveTimeOut = 2000;
            //this.modbus.SleepTime = 200;
            //this.modbus.Station = 2;
        }
        /// <summary>
        /// 获取终端的缓存数据
        /// </summary>
        private void GetTerminals()
        {
            try
            {
                //var terminalJsonString=this.redisCachingProvider.StringGet(this.Name);
                //this.Terminals = JsonConvert.DeserializeObject<List<TerminalCacheItem>>(terminalJsonString);
                this.Terminals = new List<TerminalCacheItem>();
                this.Terminals.Add(new TerminalCacheItem() { Name = "111", Addr =1 });
                this.Terminals.Add(new TerminalCacheItem() { Name = "222", Addr = 2 });
            }
            catch (Exception ex)
            {
                // 报错
            }
        }

        /// <summary>
        /// 创建串口服务
        /// </summary>
        /// <param name="item"></param>
        public void CreateServer(CollectionServerCacheItem item)
        {
            this.Server = item;
            this.CreateServer(item.Name);
            this.StartServer();
        }

        /// <summary>
        /// 创建串口服务器服务
        /// </summary>
        /// <param name="name"></param>
        public void CreateServer(string name)
        {
            this.Name = name;
            this.ServerThread = new Thread(() =>
            {

                while (this.IsWorking)
                {
                    // 轮询读取线圈数据
                    if (this.Terminals != null && this.Terminals.Any())
                    {
                        Parallel.ForEach(this.Terminals, b =>
                        {
                            if (b.Enabled)
                            {
                                this.modbus.Station =(byte)b.Addr;

                               // this.modbus.Read("00", 8);
                                Thread.Sleep(2000);
                               // this.modbus.ReadFromCoreServer(info);
                              //  this.modbus.ReadFromCoreServer(this.modbus.AlienSession.Socket,info, true, true);
                               // var result = <CollectionServerCacheItem>();
                                //if (result.IsSuccess)
                                //{
                                //    var operateResult = new OperateResult();
                                //   // operateResult.Data = result.Content;
                                //    operateResult.DeviceId = this.Server.Id;
                                //    operateResult.Terminal = b;
                                //    this.eventBus.ReceiverMateData(operateResult);
                                //}
                                //else
                                //{
                                //    Console.WriteLine("服务未连接");
                                //}
                            }
                        });
                    }
                    Thread.Sleep(1000);
                }
            });
            this.ServerThread.Name = name;
        }
        /// <summary>
        /// 创建串口服务器服务并启动
        /// </summary>
        /// <param name="name"></param>
        public void CreateAndStartServer(string name)
        {
            this.CreateServer(name);
            this.StartServer();
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        public void StartServer()
        {
            if (this.ServerThread != null)
            {
                this.InitModbusClient();
                this.GetTerminals();
                this.IsWorking = true;
                this.ServerThread.Start();
                Console.WriteLine("采集服务已启动");
                this.ReConnectServer(); // 开启重连
            }
        }

        // 初始化modbus 客户端
        private void InitModbusClient()
        {

            try
            {
                if (this.modbus != null)
                {
                    //this.modbus.IpAddress = this.Server.Ip;
                    //this.modbus.Port = this.Server.Port;
                    //this.modbus.ConnectServer();// 连接服务
                    //this.IsConnected = true;
                    Console.WriteLine($"{this.Name}:服务连接成功");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}:服务连接失败,请重试");
                Console.WriteLine(ex);
            }
        }
        /// <summary>
        /// 重新连接modbus 服务器
        /// </summary>
        private void ReConnectServer()
        {
            new Thread(() =>
            {
                try
                {
                    while (!IsConnected)
                    {
                        if (this.modbus != null)
                        {
                            //this.modbus.ConnectServer();// 连接服务
                        }
                        Thread.Sleep(1000);
                        Console.WriteLine("Modbus 服务重连中...");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("服务重连失败");
                }
            }).Start();
        }

        /// <summary>
        /// 重启服务
        /// </summary>
        public Task RestartServer()
        {
            this.GetTerminals();// 获取终端
            return Task.CompletedTask;
        }
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <returns></returns>
        public Task StopServer()
        {
            return Task.CompletedTask;
        }


        // 写入传感器数据




        public void Dispose()
        {
            this.Dispose();
        }
    }
}
