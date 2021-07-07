/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： SmsTemplate
*******类 说 明： 短信模板表
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:40:59 PM
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
    /// <summary>
    /// 短信模板id
    /// </summary>
    public class SmsTemplate:BaseEntity<Guid>
    {
        public string Name { get; set;  }
        public string Content { get; set; }
        public string Params { get; set;  }
    }
}
