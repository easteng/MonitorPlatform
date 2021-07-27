/**********************************************************************
*******命名空间： ESTHost.Core.Colleaction
*******类 名 称： IAbstractEventBus
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/27/2021 1:42:08 PM
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

namespace ESTHost.Core.Colleaction
{
    /// <summary>
    ///  
    /// </summary>
    public abstract class AbstractEventBus : IEventBus
    {
        public abstract Task<bool> ReceiverMateData(OperateResult result);
    }
}
