/**********************************************************************
*******命名空间： ESTHost.WTR20AService.Server
*******类 名 称： Device
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 10:58:56 PM
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
    /// <summary>
    /// 测温采集器参数
    /// </summary>
    public class TerminalInfo
    {
        /// <summary>
        /// 采集器编号
        /// </summary>
        public int t_no = 0;

        /// <summary>
        /// 采集器名称
        /// </summary>
        public string t_name = "";

        /// <summary>
        /// 通信方式
        /// </summary>
        public byte comm_type = 0;

        /// <summary>
        /// 采集器协议 1：WTR-20A；2：WTR-31；3：WTR-DJ
        /// </summary>
        public byte comm_rule = 0;

        /// <summary>
        /// 采集器RS485通信地址
        /// </summary>
        public byte md_addr = 1;

        /// <summary>
        /// 串口号
        /// </summary>
        public int com_no = 0;

        /// <summary>
        /// gprs模式下通信地址
        /// </summary>
        public string gprs_addr = "";

        /// <summary>
        /// 串口服务器-serverip
        /// </summary>
        public string ts_ip = "";

        /// <summary>
        /// 串口服务器-server端口
        /// </summary>
        public int ts_port = 0;

        /// <summary>
        /// 探头数量
        /// </summary>
        public byte point_count = 0;

        /// <summary>
        /// 测温点档案列表
        /// </summary>
        public List<PointInfo> point_list = new List<PointInfo>();

        /// <summary>
        /// 根据序号获取PointInfo
        /// </summary>
        /// <param name="dev_no"></param>
        /// <returns></returns>
        public PointInfo Get_PInfo(int dev_no)
        {
            if (point_list == null)
                return null;

            foreach (PointInfo p in point_list)
            {
                if (p.p_no == dev_no)
                    return p;
            }

            return null;
        }

        public PointInfo Get_PInfo_index(int dev_index)
        {
            if (point_list == null)
                return null;

            foreach (PointInfo p in point_list)
            {
                if (p.p_index == dev_index)
                    return p;
            }

            return null;
        }

        /// <summary>
        /// 关联的电机组档案
        /// </summary>
        public DianjiInfo dianji = new DianjiInfo();
    }

    /// <summary>
    /// 电机组档案
    /// </summary>
    public class DianjiInfo
    {
        /// <summary>
        /// 所属采集器编号
        /// </summary>
        public int t_no = 0;

        /// <summary>
        /// 电机编号
        /// </summary>
        public int dj_no = 0;

        /// <summary>
        /// 电机组名称
        /// </summary>
        public string dj_name = "";

        /// <summary>
        /// 安装位置
        /// </summary>
        public string location = "";

        /// <summary>
        /// 预警温度
        /// </summary>
        public decimal alarm_yj = 0.0m;

        /// <summary>
        /// 急停温度
        /// </summary>
        public decimal alarm_jt = 0.0m;

        /// <summary>
        /// 十一路测温/震动接入标记
        /// </summary>
        public byte[] join_sign = new byte[11];

        /// <summary>
        /// 预警关联的继电器编号（数据库编号）
        /// </summary>
        public int yj_j_no = 0;

        /// <summary>
        /// 预警关联的继电器出口索引
        /// </summary>
        public int yj_j_index = 0;

        /// <summary>
        /// 急停关联的继电器编号
        /// </summary>
        public int jt_j_no = 0;

        /// <summary>
        /// 急停关联的继电器出口索引
        /// </summary>
        public int jt_j_index = 0;

        /// <summary>
        /// 日极值数据是否有效
        /// </summary>
        public bool b_daymax = false;

        /// <summary>
        /// 温度日极值
        /// </summary>
        public decimal d_temp_daymax = 0.0m;

        /// <summary>
        /// 震动日极值
        /// </summary>
        public decimal d_zd_daymax = 0.0m;

        /// <summary>
        /// 温度日极值统计时间
        /// </summary>
        public DateTime date_daymax = DateTime.Now;

        /// <summary>
        /// 最后预警状态 0正常 1预警 -最后的报警判断状态
        /// </summary>
        public byte am_yujing = 0;

        /// <summary>
        /// 最后急停状态 0正常 1急停 -最后的报警判断状态
        /// </summary>
        public byte am_jiting = 0;
    }

    /// <summary>
    /// 测温点参数
    /// </summary>
    public class PointInfo
    {
        /// <summary>
        /// 所属采集器编号
        /// </summary>
        public int t_no = 0;

        /// <summary>
        /// 测温点在数据库中的编号
        /// </summary>
        public int p_no = 0;

        /// <summary>
        /// 测温点名称
        /// </summary>
        public string p_name = "";

        /// <summary>
        /// 探头在采集器里的序号
        /// </summary>
        public int p_index = 0;

        /// <summary>
        /// 测温点id，用于无线通信的地址
        /// </summary>
        public uint p_id = 0;

        /// <summary>
        /// 安装位置
        /// </summary>
        public string location = "";

        /// <summary>
        /// 相对环境报警温度（用于通信服务器判断）
        /// </summary>
        public decimal alarm1 = 0.0m;

        /// <summary>
        /// 相对环境报警恢复温度（用于通信服务器判断）
        /// </summary>
        public decimal re_alarm1
        {
            get
            {
                return (alarm1 - 2);
            }
        }

        /// <summary>
        /// 绝对报警温度（用于通信服务器判断）
        /// </summary>
        public decimal alarm2 = 0.0m;

        /// <summary>
        /// 绝对报警恢复温度（用于通信服务器判断）
        /// </summary>
        public decimal re_alarm2
        {
            get
            {
                return (alarm2 - 2);
            }
        }

        /// <summary>
        /// 日极值数据是否有效
        /// </summary>
        public bool b_daymax = false;

        /// <summary>
        /// 温度日极值
        /// </summary>
        public decimal d_temp_daymax = 0.0m;

        /// <summary>
        /// 温度日极值统计时间
        /// </summary>
        public DateTime date_daymax = DateTime.Now;

        /// <summary>
        /// 最后相对报警温度判断状态 0正常 1报警
        /// </summary>
        public byte am_xiangdui = 0;

        /// <summary>
        /// 最后绝对报警温度判断状态 0正常 1报警
        /// </summary>
        public byte am_juedui = 0;

        /// <summary>
        /// 相对值报警-发现越限
        /// </summary>
        public bool am_xd_ing = false;

        /// <summary>
        /// 绝对值报警-发现越限
        /// </summary>
        public bool am_jd_ing = false;

        /// <summary>
        /// 相对值报警-首次发现时间
        /// </summary>
        public DateTime am_xd_over_time = DateTime.Now;

        /// <summary>
        /// 绝对值报警-首次发现时间
        /// </summary>
        public DateTime am_jd_over_time = DateTime.Now;

        /// <summary>
        /// 数据是否入库
        /// </summary>
        public bool b_savedb = true;
    }

    /// <summary>
    /// 继电器/控制器信息
    /// </summary>
    public class CtrlInfo
    {
        /// <summary>
        /// 控制器编号
        /// </summary>
        public int j_no = 0;

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string j_name = "";

        /// <summary>
        /// 通信协议1：FT-RS-1600
        /// </summary>
        public int comm_rule = 0;

        /// <summary>
        /// 通信地址
        /// </summary>
        public byte md_addr = 0;

        /// <summary>
        /// 通信方式
        /// </summary>
        public byte comm_type = 0;

        /// <summary>
        /// 串口号
        /// </summary>
        public int com_no = 0;

        /// <summary>
        /// gprs 地址
        /// </summary>
        public string gprs_addr = "";

        /// <summary>
        /// 串口服务器地址 ip
        /// </summary>
        public string ts_ip = "";

        /// <summary>
        /// 串口服务器 端口
        /// </summary>
        public int ts_port = 0;

        /// <summary>
        /// 安装位置
        /// </summary>
        public string location = "";
    }

    /// <summary>
    /// 采集测温数据
    /// </summary>
    public class TData
    {
        /// <summary>
        /// dtu编号
        /// </summary>
        public string dtu_no = "";

        /// <summary>
        /// 环境温度
        /// </summary>
        public decimal d_temp = 0;

        /// <summary>
        /// 环境湿度
        /// </summary>
        public decimal d_shidu = 0;

        /// <summary>
        /// 测量点数据
        /// </summary>
        public List<PData> pd_list = new List<PData>();

        /// <summary>
        /// 根据序号获取PData
        /// </summary>
        /// <param name="dev_no"></param>
        /// <returns></returns>
        public PData Get_PData(int dev_no)
        {
            if (pd_list == null)
                return null;

            foreach (PData p in pd_list)
            {
                if (p.dev_no == dev_no)
                    return p;
            }

            return null;
        }
    }

    /// <summary>
    /// 测温点数据
    /// </summary>
    public class PData
    {
        /// <summary>
        /// 测温点序号（在采集器中的位置）
        /// </summary>
        public byte dev_no = 0;

        /// <summary>
        /// 当前温度
        /// </summary>
        public decimal d_temp = 0.0m;

        /// <summary>
        /// 电池值 0不足 1低 2中 3高  支持：电机测温
        /// </summary>
        public byte battery_value = 0xFF;

        /// <summary>
        /// 电池状态（高、中、低、不足、空白；空白表示采集环境温度）
        /// </summary>
        public string battery_state_remark
        {
            get
            {
                switch (battery_value)
                {
                    case 0:
                        return "未知";
                    case 1:
                        return "低";
                    case 2:
                        return "中";
                    case 3:
                        return "高";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 电池电压 支持：WRT-31版本
        /// </summary>
        public decimal d_battery = 0.0m;

        /// <summary>
        /// 探头状态（0正常 1故障）
        /// </summary>
        public byte dev_state = 0;

        /// <summary>
        /// 探头状态描述
        /// </summary>
        public string dev_state_remark
        {
            get
            {
                if (dev_state == 0)
                    return "正常";
                else
                    return "故障";
            }
        }

        /// <summary>
        /// 探头报警状态
        /// </summary>
        public bool alarm_state = false;

        /// <summary>
        /// 报警状态描述
        /// </summary>
        public string alarm_state_remark
        {
            get
            {
                if (alarm_state == true)
                    return "报警中...";
                else
                    return "";
            }
        }

        /// <summary>
        /// 数据时间
        /// </summary>
        public DateTime d_time = DateTime.Now;

        /// <summary>
        /// 原始数值
        /// </summary>
        public ushort d_byte = 0;

        /// <summary>
        /// 模块帧序号
        /// </summary>
        public byte frame_no = 0xFF;//0xFF为无效编号

        /// <summary>
        /// 是否离线
        /// </summary>
        public bool b_offline = false;

        /// <summary>
        /// 是否离线 byte表示方式
        /// </summary>
        public byte n_offline
        {
            get
            {
                if (b_offline)
                    return 1;
                else
                    return 0;
            }
        }

        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool bValid { get; set; }

        /// <summary>
        /// 数据是否发生了跳变
        /// </summary>
        public bool b_jump = false;
    }
}
