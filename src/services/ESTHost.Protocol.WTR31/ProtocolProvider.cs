/**********************************************************************
*******命名空间： ESTHost.Protocol.WTR20A
*******类 名 称： ProtocolProvider
*******类 说 明： 协议提供者
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 2:08:15 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTHost.ProtocolBase;

using System;

using System.Threading.Tasks;

namespace ESTHost.Protocol.WTR31
{
    /// <summary>
    ///  协议提供者 是该协议启动的入口,必须继承基础协议接口
    /// </summary>
    public class ProtocolProvider : IBaseProtocol
    {
        public string Name { get => "WTR31"; set => throw new NotImplementedException(); }

        public Task ExecuteAsync()
        {
            return Task.CompletedTask;
        }

        public Task StartAsync()
        {
            Console.WriteLine($"{this.Name} 协议已启动");
            return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            Console.WriteLine($"{this.Name} 协议已关闭");
            return Task.CompletedTask;
        }
    }
}
