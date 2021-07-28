/**********************************************************************
*******命名空间： ESTHost.Core.Server
*******类 名 称： WTR31Comm
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/28/2021 12:58:44 AM
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
    public class WTR31Comm
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
        /// 读取实时数据
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_count">探头数量</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public TData ReadData(byte addr, ushort point_count, ref string str_error)
        {
            TData t_data = new TData();

            //读取环境温度与湿度
            if (!ReadData_T(addr, ref t_data.d_temp, ref t_data.d_shidu, ref str_error))
                return null;

            //读取探头测温数据
            t_data.pd_list = ReadData_P(addr, point_count, ref str_error);
            if (t_data.pd_list == null)
                return null;

            //读取探头状态
            if (!ReadData_S(addr, point_count, ref t_data, ref str_error))
                return null;

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
                        }
                        continue;
                    }
                    #endregion

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
            }
            last_tdata = t_data;
            #endregion

            return t_data;
        }

        /// <summary>
        /// 读取实时数据-环境数据
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="d_wendu">返回环境温度</param>
        /// <param name="d_shidu">返回环境湿度</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool ReadData_T(byte addr, ref decimal d_wendu, ref decimal d_shidu, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x00;
            send_buf[3] = 0xF0;

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
                        str_error = "返回数据长度错误";
                        return false;
                    }

                    short s_wendu = frame_buf[3];
                    s_wendu <<= 8;
                    s_wendu += frame_buf[4];
                    d_wendu = s_wendu / 10m;

                    short s_shidu = frame_buf[5];
                    s_shidu <<= 8;
                    s_shidu += frame_buf[6];
                    d_shidu = s_shidu / 10m;

                    //d_wendu = (decimal)(frame_buf[3] * 0x100 + frame_buf[4]) / 10m;
                    //d_shidu = (decimal)(frame_buf[5] * 0x100 + frame_buf[6]) / 10m;
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
        /// 读取实时数据-测温点数据
        /// </summary>
        /// <param name="addr">通信</param>
        /// <param name="point_count"></param>
        /// <param name="str_error"></param>
        /// <returns></returns>
        public List<PData> ReadData_P(byte addr, ushort point_count, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x00;
            send_buf[3] = 0x00;

            send_buf[4] = (byte)(point_count >> 8);
            send_buf[5] = (byte)point_count;

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
                    List<PData> p_list = new List<PData>();
                    DateTime d_time = DateTime.Now;//数据时间
                    int nLen = frame_buf[2];
                    for (int i = 0; i < nLen; i += 2)
                    {
                        byte dev_no = (byte)(i / 2 + 1);
                        PData p_data = new PData();
                        p_data.dev_no = dev_no;
                        p_data.d_time = d_time;

                        short s_wendu = frame_buf[3 + i];
                        s_wendu <<= 8;
                        s_wendu += frame_buf[3 + i + 1];
                        p_data.d_temp = s_wendu / 10m;
                        //p_data.d_temp = (decimal)(frame_buf[3 + i] * 0x100 + frame_buf[3 + i + 1]) / 10m;

                        //-55/-60表示探头离线
                        //if (frame_buf[3 + i] == 0xC9 || frame_buf[3 + i] == 0xC4)
                        if (p_data.d_temp < -50)
                            p_data.b_offline = true;
                        else if (p_data.d_temp > 300)
                            p_data.b_offline = true;//测温点离线
                        else
                            p_data.bValid = true;
                        p_list.Add(p_data);
                    }

                    return p_list;
                }
            }
            catch (Exception e1)
            {
                str_error = e1.Message;
                return null;
            }
        }

        /// <summary>
        /// 读取实时数据-测温点状态
        /// </summary>
        /// <param name="addr">通信地址</param>
        /// <param name="point_count">探头数量</param>
        /// <param name="t_data">返回测温点的状态，更新原PData数值</param>
        /// <param name="str_error">返回错误信息</param>
        /// <returns></returns>
        public bool ReadData_S(byte addr, ushort point_count, ref TData t_data, ref string str_error)
        {
            byte[] send_buf = new byte[8];

            send_buf[0] = addr;
            send_buf[1] = 0x03;
            send_buf[2] = 0x01;
            send_buf[3] = 0x00;

            send_buf[4] = (byte)(point_count >> 8);
            send_buf[5] = (byte)point_count;

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
                    DateTime d_time = DateTime.Now;//数据时间
                    int nLen = frame_buf[2];
                    for (int i = 0; i < nLen; i += 2)
                    {
                        byte dev_no = (byte)(i / 2 + 1);
                        PData p_data = t_data.Get_PData(dev_no);
                        if (p_data == null)
                            continue;

                        //帧序号
                        p_data.frame_no = (byte)(frame_buf[3 + i + 1] >> 4);

                        //电池电压
                        p_data.d_battery = frame_buf[3 + i + 1] & 0x0F;
                        p_data.d_battery += 21;
                        p_data.d_battery /= 10;
                    }

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
                if (Link.BytesToRead <= 0)
                {
                    System.Threading.Thread.Sleep(50);
                    continue;
                }

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
                     //   ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

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
                  //  ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);

                    return buf_frame;
                }

            }
            return null;
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
            //ClassWriteLog.WriteCommLog_Terminal(t_no, strLog);
        }
    }
}
