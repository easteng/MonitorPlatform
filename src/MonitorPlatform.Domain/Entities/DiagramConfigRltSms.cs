/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： SensorRltSms
*******类 说 明： 传感器编号帮绑定短信发送
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/8/2021 12:06:58 AM
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
    public class DiagramConfigRltSms : BaseEntity<Guid>
    {
        public DiagramConfig DiagramConfig { get; set; }
        public Guid DiagramConfigId { get; set; }
        public SmsTemplate SmsTemplate { get; set; }
        public Guid SmsTemplateId { get; set; }
        public string SensorCode { get; set; }
    }
}
