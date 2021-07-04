/**********************************************************************
*******命名空间： MonitorPlatform.DataAccess
*******类 名 称： MonitorPlatformModule
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 6:11:45 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using Autofac.Extensions.DependencyInjection;

using Microsoft.Extensions.DependencyInjection;

using MonitorPlatform.Domain.IServices;

using Surging.Core.CPlatform.Module;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.DataAccess
{
    public class MonitorPlatformModule:BusinessModule
    {
        protected override void RegisterBuilder(ContainerBuilderWrapper builder)
        {
            var service = new ServiceCollection();
            service.AddSingleton<MonitorPlatformDBContext>();
            service.AddSingleton<IUserRepositoryService, UserRepositoryService>();
            builder.ContainerBuilder.Populate(service);
            base.RegisterBuilder(builder);
        }
    }
}
