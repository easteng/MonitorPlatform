/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： OperateResult
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 4:32:50 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Common;

using MonitorPlatform.Contracts.ServerCache;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  操作结果
    /// </summary>
    public class OperateResult
    {
        /// <summary>
        /// 返回的数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 采集器信息
        /// </summary>
        public TerminalCacheItem Terminal { get; set; }
        /// <summary>
        /// 当前采集设备的id
        /// </summary>
        public Guid DeviceId { get; set; }

        public override string ToString()
        {
            return this.Data.ToHexString();
        }
    }
}
