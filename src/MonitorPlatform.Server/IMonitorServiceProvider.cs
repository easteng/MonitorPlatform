/**********************************************************************
*******命名空间： MonitorPlatform.Server
*******接口名称： IMonitorServiceProvider
*******接口说明： 监控服务的接口定义
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 3:30:30 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Server
{
    public interface IMonitorServiceProvider
    {
        Task Start();
        Task Stop();
    }
}
