using MonitorPlatform.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Contracts
{
    /// <summary>
    /// 缓存内容定义
    /// </summary>
    public class CacheDto
    {
        /// <summary>
        /// 缓存的key值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 同种协议的采集器
        /// </summary>
        public List<TerminalDto> Terminals;
        /// <summary>
        /// 设备
        /// </summary>
        public Device Device{ get; set; }
    }

    public class TerminalDto
    {
        /// <summary>
        /// 终端
        /// </summary>
        public Terminal Terminal { get;set;  }
        /// <summary>
        /// 传感器列表
        /// </summary>
        public List<Sensor> Sensors { get;set; }
    }


}
