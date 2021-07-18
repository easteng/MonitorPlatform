/**********************************************************************
*******命名空间： ESTHost.DataStorage.Service
*******类 名 称： StandardDataConsumer
*******类 说 明： 标准数据的消费者
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/16/2021 10:20:56 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;

using MassTransit;

using MonitorPlatform.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.StorageService
{
    /// <summary>
    ///  标准数据的消费者
    /// </summary>
    public class StandardDataConsumer : IConsumer<StandardMessage<IOTData>>
    {
        public Task Consume(ConsumeContext<StandardMessage<IOTData>> context)
        {
            Console.WriteLine("消费到物联网数据");
            return Task.CompletedTask;
        }
    }
}
