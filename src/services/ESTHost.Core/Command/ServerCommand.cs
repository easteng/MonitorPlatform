/**********************************************************************
*******命名空间： ESTHost.Core.Command
*******类 名 称： ServerCommand
*******类 说 明： 服务命令
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 3:46:36 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Message;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core.Command
{
    /// <summary>
    ///  服务命令
    /// </summary>
    public class ServerCommand: AbstractCommand
    {
        public string Name { get; set; }
    }
}
