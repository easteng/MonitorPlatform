/**********************************************************************
*******命名空间： ESTHost.WTR20AService.Server
*******类 名 称： CommLink
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:55:28 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Server
{
    public class CommLink : IDisposable
    {
        /// <summary>
        /// 操作本串口的锁
        /// </summary>
        public object op_lock = new object();


        /// <summary>
        /// 串口服务器-TCP Server模式 TCP连接
        /// </summary>
        private TcpClient myTcp = null;


        /// <summary>
        /// 通信方式 1：串口 2：tcpserver串口服务器 3：gprs/3g/4g dtu 4：Modbus tcp网关
        /// </summary>
        public int comm_type = 2;

        /// <summary>
        /// 串口号
        /// </summary>
        public int com_no = 0;

        /// <summary>
        /// 串口方式-波特率
        /// </summary>
        public int com_bandrate = 0;

        /// <summary>
        /// 串口方式-校验方式0无 1奇 2偶
        /// </summary>
        public int com_check = 0;

        /// <summary>
        /// 串口服务器-tcpserver模式ip
        /// </summary>
        public string comserver_ip { get; set; } = "192.168.1.254";

        /// <summary>
        /// 串口服务器-tcpserver模式端口
        /// </summary>
        public int comserver_port = 0;

        /// <summary>
        /// gprs地址
        /// </summary>
        public string gprs_addr = "";

        /// <summary>
        /// 是否正在api通信
        /// </summary>
        private bool _api_comming = false;
        /// <summary>
        /// 是否正在api通信
        /// </summary>
        public bool api_comming
        {
            set
            {
                _api_comming = value;
                api_comm_time = DateTime.Now;
            }
        }

        /// <summary>
        /// api最后一次通信时间
        /// </summary>
        public DateTime api_comm_time = DateTime.Now.Date;

        /// <summary>
        /// api是否在通信
        /// </summary>
        public bool api_busing
        {
            get
            {
                if (_api_comming)
                    return true;

                TimeSpan ts = DateTime.Now - api_comm_time;
                if (ts.TotalSeconds <= 2)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// api操作锁
        /// </summary>
        public object api_lock = new object();

        /// <summary>
        /// 打开串口失败次数
        /// </summary>
        private int open_error_count = 0;

        /// <summary>
        /// 连接是否已打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                if (myTcp == null)
                    return false;

                return myTcp.Connected;
            }
        }

        /// <summary>
        /// 多少个待读字节
        /// </summary>
        public int BytesToRead
        {
            get
            {
                if (myTcp == null)
                    return 0;

                return myTcp.Available;
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            Close();

            IPEndPoint localEP = new IPEndPoint(IPAddress.Parse("192.168.1.254"), 30003);
            myTcp = new TcpClient();
            try
            {
                myTcp.Connect(localEP);
                Console.WriteLine("连接服务器" + comserver_ip + ":" + comserver_port + "成功");
                return true;
            }
            catch (Exception e1)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("连接服务器" + comserver_ip + ":" + comserver_port + "失败，" + e1.Message);
            }

            return false;
        }


        /// <summary>
        /// 关闭已有连接
        /// </summary>
        public void Close()
        {
            try
            {
                if (myTcp != null && myTcp.Connected)
                    myTcp.Close();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(byte[] bytes)
        {
            switch (comm_type)
            {
                case 1://串口方式
                    {
                        //myPort.Write(bytes, 0, bytes.Length);
                    }
                    break;
                case 2://tcp串口服务器
                    {
                        myTcp.Client.Send(bytes);
                    }
                    break;
                case 3://gprs网路服务器
                    {
                      //  myNet.Send_dtu(bytes);
                    }
                    break;
                case 4://Modbus Tcp
                    {
                        byte[] s_buf = new byte[6 + bytes.Length - 2];
                        s_buf[0] = 0x00;
                        s_buf[1] = 0x12;
                        s_buf[2] = 0x00;
                        s_buf[3] = 0x00;

                        //数据域长度
                        ushort nLen = (ushort)(bytes.Length - 2);
                        s_buf[4] = (byte)(nLen >> 8);
                        s_buf[5] = (byte)nLen;
                        for (int i = 0; i < nLen; i++)
                            s_buf[6 + i] = bytes[i];

                        myTcp.Client.Send(s_buf);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <returns></returns>
        public byte[] Recv()
        {
            if (myTcp == null)
                return new byte[0];

            if (myTcp.Available == 0)
                return new byte[0];

            byte[] bytes = new byte[myTcp.Available];
            int read_count = myTcp.Client.Receive(bytes);
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
        /// 清空接收缓冲区
        /// </summary>
        public void ClearRecvBuf()
        {
            if (myTcp == null)
                return;
            if (myTcp.Available == 0)
                return;

            byte[] bytes = new byte[myTcp.Available];
            myTcp.Client.Receive(bytes);
        }

        /// <summary>
        /// 销毁资源
        /// </summary>
        public void Dispose()
        {
            Close();
        }
    }
}
