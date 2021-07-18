/**********************************************************************
*******命名空间： MonitorPlatform.Wpf
*******类 名 称： MessageConsumer
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 9:25:14 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.MassTransit;
using ESTCore.Message;

using MassTransit;

using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf
{
    public class MessageConsumer : IObserver<ConsumeContext<IBaseMessage>>
    {
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(ConsumeContext<IBaseMessage> value)
        {
            //GlableDelegateHandler.UpdateRuntime?.Invoke(value.Message.Name);
            //throw new NotImplementedException();
        }
    }
}
