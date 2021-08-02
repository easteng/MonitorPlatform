/**********************************************************************
*******命名空间： ESTHost.Protocol
*******接口名称： IBaseProtocol
*******接口说明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 2:06:39 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.ProtocolBase
{
    /// <summary>
    ///  基本协议接口
    /// </summary>
    public interface IBaseProtocol
    {
        abstract string Name { get; set; }

        /// <summary>
        /// 异步执行
        /// </summary>
        /// <returns></returns>
        abstract Task ExecuteAsync();

        /// <summary>
        /// 开始操作
        /// </summary>
        /// <returns></returns>
        abstract Task StartAsync();

        /// <summary>
        /// 结束操作
        /// </summary>
        /// <returns></returns>
        abstract Task StopAsync();
        /// <summary>
        /// 写入传感器
        /// </summary>
        /// <returns></returns>
        abstract Task WriteSensor(Guid deviceId,Guid terminalId);
        /// <summary>
        /// 重启采集
        /// </summary>
        /// <returns></returns>
        abstract Task Restart(Guid deviceId, Guid terminalId);
    }
}
