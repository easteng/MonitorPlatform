/**********************************************************************
*******命名空间： ESTHost.Core
*******类 名 称： MessageTopic
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/23/2021 10:37:53 AM
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

namespace ESTHost.Core
{
    /// <summary>
    ///  消息主题
    /// </summary>
    public class MessageTopic
    {
        /// <summary>
        /// 物联网主题
        /// </summary>
        public static readonly string Iot = "IOT";
        /// <summary>
        /// 事件响应主题
        /// </summary>
        public static readonly string Event = "EVENT";

        /// <summary>
        /// 命令操作主题
        /// </summary>
        public static readonly string Command = "COMMAND";

    }
}
