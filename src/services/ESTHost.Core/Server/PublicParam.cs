/**********************************************************************
*******命名空间： ESTHost.WTR20AService.Server
*******类 名 称： PublicParam
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 11:00:20 PM
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
    /// 公共参数
    /// </summary>
    public class PublicParam
    {
        /// <summary>
        /// 是否有电机测温模块
        /// </summary>
        public static bool dj_app = false;

        /// <summary>
        /// 温度持续越限时间-秒
        /// </summary>
        public static int alarm_oversec = 0;

        /// <summary>
        /// 有效的最大温度
        /// </summary>
        public static int valid_max_temp = 300;

        /// <summary>
        /// 通信命令等待返回的超时时间-秒
        /// </summary>
        public static int comm_timeout = 2;

        /// <summary>
        /// 温度突然跳变判断过滤绝对值
        /// </summary>
        public static int check_jump = 20;

        /// <summary>
        /// gprs模式抄收间隔
        /// </summary>
        public static int gprs_interval = 3;
    }
}
