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

using ESTHost.Core.Server;

using MonitorPlatform.Share.ServerCache;

using Newtonsoft.Json;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

using WatsonTcp;

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
        private TcpClient tcpClient { get; set; }
        private IPEndPoint iPEndPoint {get;set;}
        private WatsonTcpClient  watsonTcpClient { get; set; }  
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
            this.modbus = new ModbusTcpNet();
            this.modbus.ConnectTimeOut = 2000;
            this.modbus.ReceiveTimeOut = 2000;
            this.modbus.SleepTime = 200;
            this.modbus.Station = 1;


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
        public CommLink myLink = new CommLink();
        public TerminalInfo t_info = new TerminalInfo();
        TData last_tdata = null;
        /// <summary>
        /// 创建串口服务器服务
        /// </summary>
        /// <param name="name"></param>
        public void CreateServer(string name)
        {
            this.Name = name;
            this.ServerThread = new Thread(() =>
            {
                WTR20AComm comm = new WTR20AComm();
                comm.t_no = 1;
                comm.com_bandrate = 9600;
                comm.com_check = 0;
                comm.Link = myLink;


                // 设置传感器id
                string str_error = ""; 

                while (this.IsWorking)
                {
                    ////等待Api通信完成
                  //  while (myLink.api_busing)
                   // {
                        //Parallel.ForEach(this.Terminals, b =>
                        //{
                        //    if (b.Enabled)
                        //    {
                        //        this.modbus.Station = (byte)b.Addr;

                        //        var res = this.modbus.Read("00", 8);
                        //        //  Thread.Sleep(2000);
                        //        // this.modbus.ReadFromCoreServer(info);
                        //        //  this.modbus.ReadFromCoreServer(this.modbus.AlienSession.Socket,info, true, true);
                        //        // var result = <CollectionServerCacheItem>();
                        //        //if (result.IsSuccess)
                        //        //{
                        //        //    var operateResult = new OperateResult();
                        //        //   // operateResult.Data = result.Content;
                        //        //    operateResult.DeviceId = this.Server.Id;
                        //        //    operateResult.Terminal = b;
                        //        //    this.eventBus.ReceiverMateData(operateResult);
                        //        //}
                        //        //else
                        //        //{
                        //        //    Console.WriteLine("服务未连接");
                        //        //}
                        //    }
                        //});
                   // }
                    //Thread.Sleep(300);
                    //str_error = "";
                    //TData t_data = null;
                    //lock (myLink.op_lock)
                    //{
                    //    t_data = comm.ReadData(t_info.md_addr,1, ref str_error);
                    //}
                    //if (t_data == null)
                    //{
                    //    last_tdata = null;
                    //    continue;
                    //}

                //    轮询读取线圈数据
                    if (this.Terminals != null && this.Terminals.Any())
                    {
                        Parallel.ForEach(this.Terminals, b =>
                        {
                            if (b.Enabled)
                            {
                                var aaa = "01 03 00 00 00 02 C4 0B";
                                var ccc = StringToHexByte(aaa);
                                watsonTcpClient.Send(aaa);
                               // var result = this.modbus.Read("00", 2);
                                //if (result.IsSuccess)
                                //{
                                //    var operateResult = new OperateResult();
                                //    operateResult.Data = result.Content;
                                //    operateResult.DeviceId = this.Server.Id;
                                //    operateResult.Terminal = b;
                                //    this.eventBus.ReceiverMateData(operateResult);
                                //}
                                //else
                                //{
                                //    Console.WriteLine($"{result.Message}");
                                //}
                            }
                        });
                    }
                    Thread.Sleep(1000);
                }
            });
            this.ServerThread.Name = name;
        }
        public static byte[] StringToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if (hexString.Length % 2 != 0)
                hexString += " ";
            var returnBytes = new byte[hexString.Length / 2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
        private byte[] ReadBuffer()
        {
            byte[] bytes = new byte[tcpClient.Available];
            int read_count = tcpClient.Client.Receive(bytes);
            if (read_count == bytes.Length)
                return bytes;

            byte[] buf = new byte[read_count];
            for (int i = 0; i < read_count; i++)
            {
                buf[i] = bytes[i];
            }
            return buf;
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
                    this.modbus.IpAddress = this.Server.Ip;
                    this.modbus.Port = this.Server.Port;
                    this.modbus.ConnectServer();// 连接服务
                                                //   this.iPEndPoint = new IPEndPoint(IPAddress.Parse(this.Server.Ip), this.Server.Port);
                                                //  tcpClient.Connect(this.iPEndPoint);
                    this.watsonTcpClient = new WatsonTcpClient(this.Server.Ip, this.Server.Port);
                    this.watsonTcpClient.Events.MessageReceived += Events_MessageReceived;
                    this.watsonTcpClient.Events.StreamReceived += Events_StreamReceived;
                    this.watsonTcpClient.Events.ServerConnected += Events_ServerConnected;
                    this.watsonTcpClient.Keepalive.EnableTcpKeepAlives = true;
                    this.watsonTcpClient.Keepalive.TcpKeepAliveInterval = 5;
                    this.watsonTcpClient.Keepalive.TcpKeepAliveRetryCount = 5;
                    this.watsonTcpClient.Connect(); 
                    this.IsConnected = true;
                    Console.WriteLine($"{this.Name}:服务连接成功");
                }
            }
            catch (Exception ex)
            {
                this.IsConnected = false;
                Console.WriteLine($"{this.Name}:服务连接失败,请重试");
                Console.WriteLine(ex);
            }
        }

        private void Events_ServerConnected(object sender, ConnectionEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Events_StreamReceived(object sender, StreamReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Events_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 重新连接modbus 服务器
        /// </summary>
        private void ReConnectServer()
        {
            new Thread(() =>
            {
                while (IsWorking)
                {
                    try
                    {
                        if (!this.IsConnected)
                        {
                            //if (this.modbus != null)
                            //{
                            //    this.modbus.ConnectServer();// 连接服务
                            //}


                            if (watsonTcpClient != null && !watsonTcpClient.Connected)
                            {
                                watsonTcpClient.Connect();
                            }

                            //tcpClient.Connect(this.iPEndPoint);
                            Thread.Sleep(1000);
                            Console.WriteLine("Modbus 服务重连中...");
                        }
                       


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("服务连接失败，正在重新连接");
                    }
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
