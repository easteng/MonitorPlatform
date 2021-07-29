/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： ModbusBase
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/29/2021 11:17:33 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  modbus 基类，用来统一管理服务端和客户端的详细服务
    /// </summary>
    public abstract class ModbusBase
    {
        /// <summary>
        /// 设备地址位
        /// </summary>
        public byte Station { get; set; } = 0x01;

        /// <summary>
        /// 开始采集数据
        /// </summary>
        public abstract void StartCollection();

        /// <summary>
        /// 停止数据采集
        /// </summary>
        public abstract void StopCollection();

        /// <summary>
        /// 刷新数据缓存  当远程控制刷新数据时调用
        /// </summary>
        public abstract void RefreshChache();

        /// <summary>
        /// 向采集器写入传感器
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns></returns>
        public abstract OperateResult<byte[]> WriteSensors(byte[] sensor);

        /// <summary>
        /// 重启终端
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract OperateResult<byte[]> RestartTerminal(byte[] command);

        /// <summary>
        /// 开始写入，需要暂定指定采集器的读取
        /// </summary>
        /// <param name="terminalId"></param>
        public abstract void BeginWrite(Guid terminalId);
        /// <summary>
        /// 结束写入，开启采集器的读取
        /// </summary>
        /// <param name="terminalId"></param>
        public abstract void EndWrite(Guid terminalId);
    }
}
