/**********************************************************************
*******命名空间： ESTHost.Core.Message
*******类 名 称： IOTMessage
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 11:59:00 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    /// <summary>
    ///  物联网消息
    /// </summary>
    public class IOTMessage: AbstractMessage
    {
        public override string Topic { get => "IOT"; set => base.Topic = value; }

        /// <summary>
        /// 数据编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public double Value { get; set; }
        /// <summary>
        /// 采集数值
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 所属终端
        /// </summary>

        public string Terminal { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
