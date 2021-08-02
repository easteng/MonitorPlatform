/**********************************************************************
*******命名空间： MonitorPlatform.Share.Message
*******类 名 称： DeviceMessage
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 5:04:54 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share.Message
{
    /// <summary>
    ///  设备采集的数据消息体
    /// </summary>
    public class DeviceMessage: AbstractMessage
    {
        /// <summary>
        /// 主题信息
        /// </summary>
        public override string Topic { get => MessageTopic.DeviceData; set => base.Topic = value; }
        
        /// <summary>
        /// 所属设备的id
        /// </summary>
        public Guid DeviceId { get; set; }

        public Guid TerminalId { get; set; }
        /// <summary>
        /// 数据协议名称  用来指定是哪种协议
        /// </summary>
        public string Protocol { get; set;  }
        /// <summary>
        /// 物联网数据
        /// </summary>
        public List<IOTMessage> IOTData { get; set; }
        public DeviceMessage() { }
        
        public DeviceMessage(Guid deviceId,Guid terminalId,List<IOTMessage> iotData,string protocol)
        {
            this.DeviceId= deviceId;
            this.TerminalId = terminalId;   
            this.IOTData = iotData; 
            this.Protocol= protocol;
        }
    }
}
