/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： SmsConfig
*******类 说 明： 短信服务配置表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:38:50 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    public class SmsConfig:BaseEntity<Guid>
    {
        /// <summary>
        /// 串口名称
        /// </summary>
        public string  ComName { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public double Baud { get; set; }
    }
}
