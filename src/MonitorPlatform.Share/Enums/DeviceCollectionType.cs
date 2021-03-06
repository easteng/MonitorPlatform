/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： DeviceCollectionType
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:07:44 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    /// <summary>
    /// 设备采集模式
    /// </summary>
    public class DeviceCollectionType
    {
        public static string TcpServer = "TcpServer";
        public static string ModbusTcp = "ModbusTcp";
        public static string SerialPort = "SerialPort";
        public static string Gprs = "Gprs";
    }
}
