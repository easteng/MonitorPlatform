/**********************************************************************
*******命名空间： ESTHost.DataCenter
*******类 名 称： CommandRepeater
*******类 说 明： 控制命令转换器
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 4:24:04 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message;
using ESTCore.Message.Handler;

using ESTHost.Core.Command;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataCenter
{
    /// <summary>
    ///  控制命令转换器
    /// </summary>
    public class CommandRepeater:BaseRepeater<ServerCommand>
    {

    }
}
