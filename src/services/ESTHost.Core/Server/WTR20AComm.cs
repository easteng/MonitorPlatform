/**********************************************************************
*******命名空间： ESTHost.WTR20AService.Server
*******类 名 称： WTR20AComm
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:54:01 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Server
{
    public class WTR20AComm
    {
        /// <summary>
        /// 终端编号
        /// </summary>
        public int t_no = 0;

        /// <summary>
        /// 串口方式-波特率
        /// </summary>
        public int com_bandrate = 0;

        /// <summary>
        /// 串口方式-校验方式0无 1奇 2偶
        /// </summary>
        public int com_check = 0;

        /// <summary>
        /// 连接
        /// </summary>
        public CommLink Link = null;

        /// <summary>
        /// 上一次实时数据
        /// </summary>
        private TData last_tdata = null;

        /// <summary>
        /// 设置报警温度
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="alarm1">相对报警温度</param>
        /// <param name="alarm2">绝对报警温度</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool SetAlarm(byte addr, byte alarm1, byte alarm2, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x06;
            send_buf[2] = 0x10;
            send_buf[3] = 0x00;
            send_buf[4] = alarm1;
            send_buf[5] = alarm2;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                bool bReturn = GetSameFrame(send_buf);

                if (!bReturn)
                    str_error = "等待返回超时";
                return bReturn;
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 设置报警温度
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_count">探头数量</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool SetPointCount(byte addr, byte point_count, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x06;
            send_buf[2] = 0x10;
            send_buf[3] = 0x01;
            send_buf[4] = 0x00;
            send_buf[5] = point_count;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                bool bReturn = GetSameFrame(send_buf);

                if (!bReturn)
                    str_error = "等待返回超时";
                return bReturn;
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 终端重启
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool SetRestart(byte addr, ref string str_error)
        {
            byte[] send_buf = new byte[6];

            send_buf[0] = addr;
            send_buf[1] = 0x06;
            send_buf[2] = 0x00;
            send_buf[3] = 0x00;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 4);
            send_buf[4] = (byte)crc_value;
            send_buf[5] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                bool bReturn = GetSameFrame(send_buf);

                if (!bReturn)
                    str_error = "等待返回超时";
                return bReturn;
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 设置探头id
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_no">探头序号</param>
        /// <param name="point_id">探头ID</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool SetPointID(byte addr, byte point_no, UInt32 point_id, ref string str_error)
        {
            byte[] send_buf = new byte[13];

            send_buf[0] = addr;
            send_buf[1] = 0x10;

            ushort jcq_no = (ushort)(0x1000 + point_no * 2);
            send_buf[2] = (byte)(jcq_no >> 8);
            send_buf[3] = (byte)jcq_no;

            send_buf[4] = 0x00;
            send_buf[5] = 0x02;
            send_buf[6] = 0x04;

            send_buf[7] = (byte)point_id;
            send_buf[8] = (byte)(point_id >> 8);
            send_buf[9] = (byte)(point_id >> 16);
            send_buf[10] = (byte)(point_id >> 24);

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 11);
            send_buf[11] = (byte)crc_value;
            send_buf[12] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                bool bReturn = GetFrame_WritePointID(send_buf);

                if (!bReturn)
                    str_error = "等待返回超时";
                return bReturn;
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取实时数据
        /// </summary>
        /// <param name="addr">通信</param>
        /// <param name="point_count"></param>
        /// <param name="str_error"></param>
        /// <returns></returns>
        public TData ReadData(byte addr, ushort point_count, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x00;
            send_buf[3] = 0x00;

            ushort p_count = (ushort)(point_count + 1);
            send_buf[4] = (byte)(p_count >> 8);
            send_buf[5] = (byte)p_count;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return null;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                byte[] frame_buf = GetOneFrame(addr, 0x03);
                if (frame_buf == null)
                {
                    str_error = "等待返回超时";
                    return null;
                }
                else
                {
                    TData t_data = new TData();
                    DateTime d_time = DateTime.Now;//数据时间
                    int nLen = frame_buf[2];
                    for (int i = 0; i < nLen; i += 2)
                    {
                        int p_index = i / 2;
                        PData p_data = new PData();
                        p_data.dev_no = (byte)p_index;
                        p_data.d_time = d_time;
                        p_data.d_temp = (sbyte)frame_buf[3 + i];
                        p_data.d_byte = (ushort)(frame_buf[3 + i] * 0x100 + frame_buf[3 + i + 1]);
                        if (p_index != 0)
                        {
                            byte nstate = frame_buf[3 + i + 1];
                            p_data.frame_no = (byte)(nstate >> 4);
                            p_data.battery_value = (byte)((nstate >> 2) & 0x03);
                            p_data.dev_state = (byte)((nstate >> 1) & 0x01);

                            if (frame_buf[3 + i] == 0xC4)
                                p_data.b_offline = true;//测温点离线
                            else
                                p_data.bValid = true;
                            t_data.pd_list.Add(p_data);
                        }
                        else
                        {
                            t_data.d_temp = p_data.d_temp;//环境温度
                        }
                    }

                    #region 处理数据跳变
                    if (last_tdata != null)
                    {
                        foreach (PData p in t_data.pd_list)
                        {
                            PData p1 = last_tdata.Get_PData(p.dev_no);
                            if (p1 == null)
                                continue;

                            #region 判断跳变是否恢复
                            if (p1.b_jump)
                            {
                                if (p1.frame_no == p.frame_no)
                                {
                                    p.b_jump = true;
                                    p.bValid = false;
                                    p.b_offline = true;
                                    continue;
                                }
                            }
                            else
                            {
                                #region 判断是否产生跳变
                                if (!p.bValid || !p1.bValid)
                                    continue;
                                decimal jump_data = p.d_temp - p1.d_temp;
                                if (jump_data < 0)
                                    jump_data *= -1;
                                if (jump_data >= PublicParam.check_jump)
                                {
                                    p.b_jump = true;
                                    p.bValid = false;
                                    p.b_offline = true;
                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    last_tdata = t_data;
                    #endregion

                    return t_data;
                }
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 读取报警温度参数
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="alarm1">返回相对报警温度</param>
        /// <param name="alarm2">返回绝对报警温度</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool ReadAlarm(byte addr, ref byte alarm1, ref byte alarm2, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x10;
            send_buf[3] = 0x00;
            send_buf[4] = 0x00;
            send_buf[5] = 0x01;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                byte[] frame_buf = GetOneFrame(addr, 0x03);
                if (frame_buf == null)
                {
                    str_error = "等待返回超时";
                    return false;
                }
                else
                {
                    if (frame_buf[2] != 0x02)
                    {
                        str_error = "返回报文内容错误";
                        return false;
                    }

                    alarm1 = frame_buf[3];
                    alarm2 = frame_buf[4];
                    return true;
                }
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取报警温度参数
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_count">返回 探头数量</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool ReadPointCount(byte addr, ref byte point_count, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x10;
            send_buf[3] = 0x01;
            send_buf[4] = 0x00;
            send_buf[5] = 0x01;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                byte[] frame_buf = GetOneFrame(addr, 0x03);
                if (frame_buf == null)
                {
                    str_error = "等待返回超时";
                    return false;
                }
                else
                {
                    if (frame_buf[2] != 0x02)
                    {
                        str_error = "返回报文内容错误";
                        return false;
                    }

                    point_count = frame_buf[4];
                    return true;
                }
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 读取探头ID
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_id">探头序号</param>
        /// <param name="point_id">返回 探头ID</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool ReadPointID(byte addr, byte point_no, ref UInt32 point_id, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;

            ushort jcq_no = (ushort)(0x1000 + point_no * 2);
            send_buf[2] = (byte)(jcq_no >> 8);
            send_buf[3] = (byte)jcq_no;
            send_buf[4] = 0x00;
            send_buf[5] = 0x02;

            ushort crc_value = ClassComputeCRC.getcrc(send_buf, 6);
            send_buf[6] = (byte)crc_value;
            send_buf[7] = (byte)(crc_value >> 8);

            try
            {
                if (!Link.IsOpen)
                {
                    if (!Link.Open())
                    {
                        str_error = "通道打开失败";
                        return false;
                    }
                }

                Link.ClearRecvBuf();
                this.Send(send_buf);
                byte[] frame_buf = GetOneFrame(addr, 0x03);
                if (frame_buf == null)
                {
                    str_error = "等待返回超时";
                    return false;
                }
                else
                {
                    if (frame_buf[2] != 0x04)
                    {
                        str_error = "返回报文内容错误";
                        return false;
                    }

                    point_id = frame_buf[3];
                    point_id += (uint)(frame_buf[4] * 0x100);
                    point_id += (uint)(frame_buf[5] * 0x10000);
                    point_id += (uint)(frame_buf[6] * 0x1000000);
                    return true;
                }
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取一条完整报文
        /// </summary>
        /// <returns></returns>
        private byte[] GetOneFrame(byte addr, byte app_code)
        {
            //Modbus Tcp 模式接收完整帧
            if (Link.comm_type == 4)
                return GetOneFrame_ModbusTcp(addr, app_code);

            List<byte> recv_bytes = new List<byte>();
            DateTime tout = DateTime.Now.AddSeconds(PublicParam.comm_timeout);
            while (tout >= DateTime.Now)
            {
                //if (Link.BytesToRead <= 0)
                //{
                //    System.Threading.Thread.Sleep(50);
                //    continue;
                //}

                recv_bytes.AddRange(Link.Recv());
                while (recv_bytes.Count >= 5)
                {
                    if (recv_bytes[0] != addr || recv_bytes[1] != app_code)//帧头
                    {
                        recv_bytes.RemoveAt(0);
                        continue;
                    }

                    byte nLen = recv_bytes[2];//数据域长度
                    if (recv_bytes.Count < (nLen + 3 + 2))
                        break;

                    ushort crc_value = ClassComputeCRC.getcrc(recv_bytes.ToArray(), nLen + 3);
                    ushort crc_frame = (ushort)(recv_bytes[nLen + 3] + recv_bytes[nLen + 3 + 1] * 0x100);
                    if (crc_value == crc_frame)
                    {
                        byte[] buf_frame = new byte[nLen + 3 + 2];
                        for (int i = 0; i < (nLen + 3 + 2); i++)
                        {
                            buf_frame[i] = recv_bytes[0];
                            recv_bytes.RemoveAt(0);
                        }

                        //日志
                        string strLog = "Recv:";
                        foreach (byte by in buf_frame)
                            strLog += by.ToString("X2") + " ";
                      //  ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

                        //Console.WriteLine(dtu_no + ",接收到一条有效数据" + DateTime.Now.ToString());
                        return buf_frame;
                    }
                    else
                    {
                        recv_bytes.RemoveAt(0);
                        continue;
                    }
                }

            }
            return null;
        }

        /// <summary>
        /// Modbus Tcp模式-接收完整帧
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="app_code"></param>
        /// <returns></returns>
        private byte[] GetOneFrame_ModbusTcp(byte addr, byte app_code)
        {
            List<byte> recv_bytes = new List<byte>();
            DateTime tout = DateTime.Now.AddSeconds(PublicParam.comm_timeout);
            while (tout >= DateTime.Now)
            {
                if (Link.BytesToRead <= 0)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                recv_bytes.AddRange(Link.Recv());
                while (recv_bytes.Count >= 9)
                {
                    //帧头
                    if (recv_bytes[0] != 0 || recv_bytes[1] != 0x12 || recv_bytes[2] != 0x00 || recv_bytes[3] != 0x00)
                    {
                        recv_bytes.RemoveAt(0);
                        continue;
                    }

                    int netLen = recv_bytes[4] * 0x100 + recv_bytes[5];//数据域长度
                    if (recv_bytes.Count < (netLen + 6))
                        break;

                    if (recv_bytes[6] != addr || recv_bytes[7] != app_code)//帧头
                    {
                        recv_bytes.RemoveAt(0);
                        continue;
                    }

                    byte nLen = recv_bytes[8];//数据域长度
                    if (recv_bytes.Count < (6 + nLen + 3))
                        break;

                    byte[] buf_frame = new byte[nLen + 3];
                    for (int i = 0; i < (nLen + 3); i++)
                    {
                        buf_frame[i] = recv_bytes[6 + i];
                    }
                    for (int i = 0; i < (netLen + 6); i++)
                    {
                        recv_bytes.RemoveAt(0);
                    }

                    //日志
                    string strLog = "Recv:";
                    foreach (byte by in buf_frame)
                        strLog += by.ToString("X2") + " ";
                   // ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

                    return buf_frame;
                }

            }
            return null;
        }

        /// <summary>
        /// 获取一条与发送相同的报文
        /// </summary>
        /// <param name="Samebuf">发送报文</param>
        /// <returns></returns>
        private bool GetSameFrame(byte[] Samebuf)
        {
            int s_len = Samebuf.Length;
            if (Link.comm_type == 4)
                s_len -= 2;

            List<byte> recv_bytes = new List<byte>();
            DateTime tout = DateTime.Now.AddSeconds(PublicParam.comm_timeout);
            while (tout >= DateTime.Now)
            {
                if (Link.BytesToRead <= 0)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                recv_bytes.AddRange(Link.Recv());
                while (recv_bytes.Count >= s_len)
                {
                    bool bsame = true;
                    for (int i = 0; i < s_len; i++)
                    {
                        if (recv_bytes[i] != Samebuf[i])
                        {
                            bsame = false;
                            recv_bytes.RemoveAt(0);
                            break;
                        }
                    }

                    //日志
                    string strLog = "Recv:";
                    foreach (byte by in recv_bytes)
                        strLog += by.ToString("X2") + " ";
                    //ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

                    if (bsame)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取写探头ID的返回帧
        /// </summary>
        /// <param name="Sendbuf">发送报文</param>
        /// <returns></returns>
        private bool GetFrame_WritePointID(byte[] Sendbuf)
        {
            if (Link.comm_type == 4)
                return GetFrame_WritePointID_ModbusTcp(Sendbuf);

            List<byte> recv_bytes = new List<byte>();
            DateTime tout = DateTime.Now.AddSeconds(PublicParam.comm_timeout);
            while (tout >= DateTime.Now)
            {
                if (Link.BytesToRead <= 0)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                recv_bytes.AddRange(Link.Recv());
                while (recv_bytes.Count >= 8)
                {
                    bool bsame = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (recv_bytes[i] != Sendbuf[i])
                        {
                            bsame = false;
                            recv_bytes.RemoveAt(0);
                            break;
                        }
                    }

                    if (bsame)
                    {
                        ushort crc_value = ClassComputeCRC.getcrc(recv_bytes.ToArray(), 6);
                        ushort crc_frame = (ushort)(recv_bytes[6] + recv_bytes[7] * 0x100);
                        if (crc_value == crc_frame)
                        {
                            //日志
                            string strLog = "Recv:";
                            foreach (byte by in recv_bytes)
                                strLog += by.ToString("X2") + " ";
                            //ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

                            return true;
                        }
                        else
                            recv_bytes.RemoveAt(0);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取写探头ID的返回帧-ModbusTcp
        /// </summary>
        /// <param name="Sendbuf">发送报文</param>
        /// <returns></returns>
        private bool GetFrame_WritePointID_ModbusTcp(byte[] Sendbuf)
        {
            List<byte> recv_bytes = new List<byte>();
            DateTime tout = DateTime.Now.AddSeconds(PublicParam.comm_timeout);
            while (tout >= DateTime.Now)
            {
                if (Link.BytesToRead <= 0)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

                recv_bytes.AddRange(Link.Recv());
                while (recv_bytes.Count >= 6)
                {
                    bool bsame = true;
                    for (int i = 0; i < 6; i++)
                    {
                        if (recv_bytes[i] != Sendbuf[i])
                        {
                            bsame = false;
                            recv_bytes.RemoveAt(0);
                            break;
                        }
                    }

                    if (bsame)
                    {
                        //日志
                        string strLog = "Recv:";
                        foreach (byte by in recv_bytes)
                            strLog += by.ToString("X2") + " ";
                       // ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="bytes"></param>
        private void Send(byte[] bytes)
        {
            //改变串口波特率
            //if (Link.comm_type == 1 && (Link.com_bandrate != com_bandrate || Link.com_check != com_check))
            //    Link.Modify_ComParame(com_bandrate, com_check);

            Link.Send(bytes);

            //日志
            string strLog = "Send:";
            int nLen = bytes.Length;
            if (Link.comm_type == 4)//Modbus Tcp协议 不发送最后CRC字节
                nLen -= 2;
            for (int i = 0; i < nLen; i++)
                strLog += bytes[i].ToString("X2") + " ";
            Console.WriteLine(strLog);
           // ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);
        }
    }
}
