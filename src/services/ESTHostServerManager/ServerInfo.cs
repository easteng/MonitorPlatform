/**********************************************************************
*******命名空间： ESTHost.ServerManager
*******类 名 称： ServerInfo
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/4/2021 9:53:14 AM
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
    ///  
    /// </summary>
    public class ServerInfo
    {
        public string ServerName { get; set; }
    }

    public enum ServerState
    {
        // 已安装
        Installed,
        UnInstall,// 没有安装
        Runing,// 运行中
        Stoped,// 已停止

    }
}
