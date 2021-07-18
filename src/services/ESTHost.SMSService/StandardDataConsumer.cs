/**********************************************************************
*******命名空间： ESTHost.SMSService
*******类 名 称： StandardDataConsumer
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/17/2021 5:02:53 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using MassTransit;

using MonitorPlatform.Contracts;

using System;
using System.Threading.Tasks;

namespace ESTHost.SMSService
{
    public class StandardDataConsumer : IConsumer<StandardMessage<IOTData>>
    {
        public Task Consume(ConsumeContext<StandardMessage<IOTData>> context)
        {
            Console.WriteLine("消费到物联网数据");
            return Task.CompletedTask;
        }
    }
}
