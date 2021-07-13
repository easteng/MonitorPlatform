/**********************************************************************
*******命名空间： ESTHost.ServerManager
*******类 名 称： CheckItem
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 5:19:14 PM
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

namespace ESTHost.ServerManager
{
    /// <summary>
    /// 数据监测内容
    /// </summary>
    public class CheckItem
    {
        public string Name { get; set; }
        public StatusType Status { get; set; }

        public string Info { get; set; }
    }

    public enum StatusType
    {
        Error,
        Success
    }
}
