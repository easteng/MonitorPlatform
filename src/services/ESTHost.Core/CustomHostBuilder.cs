/**********************************************************************
*******命名空间： ESTHost.Core
*******类 名 称： CustomHostBuilder
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 9:46:58 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Core
{
    public class CustomHostBuilder
    {
        public static IHostBuilder CreateDefaultServerHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
                
        }
        public static IHostBuilder CreateWindowsServerHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseWindowsService();
        }
    }
}
