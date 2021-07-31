/**********************************************************************
*******命名空间： ESTHost.Protocol
*******类 名 称： ProtocolModule
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 2:15:10 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using Autofac;
using Autofac.Extensions.DependencyInjection;

using ESTCore.Caching;
using ESTCore.Message;

using ESTHost.Core.Colleaction;

using Microsoft.Extensions.DependencyInjection;

using Silky.Lms.Core.Extensions;
using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ESTHost.ProtocolBase
{
    /// <summary>
    ///  采集协议模块 主要用来注册和发现数据采集协议，每个协议都继承标准的协议接口
    ///  系统根据软件注册的协议进行注册和发现，并执行采集操作等
    /// </summary>
    public class ProtocolModule : LmsModule
    {
        public virtual AppDomain App => AppDomain.CurrentDomain;
        private List<Assembly> protocolAssembly;
        public override Task Initialize(ApplicationContext applicationContext)
        {
            // 获取已有的协议

            return base.Initialize(applicationContext);
        }
        protected override void RegisterServices(ContainerBuilder builder)
        {
            // 获取协议的程序集，并通过反射注册对应组件
            // 注册协议类型，同时注册事件总线用来传输数据
            var services = new ServiceCollection();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t =>
                t.GetInterfaces().Contains(typeof(IBaseProtocol))
                || t.GetInterfaces().Contains(typeof(IEventBus))))
                .ToArray();
            foreach (var item in types)
            {
                if (item.GetInterfaces().Contains(typeof(IBaseProtocol)))
                {
                    // 注册协议
                    var instance = Activator.CreateInstance(item);
                    //根据协议类型名称进行注册
                    builder
                        .RegisterInstance(instance)
                        .SingleInstance()
                        .As<IBaseProtocol>()
                        .Named(item.GetProperty("Name").GetValue(instance).ToString(), typeof(IBaseProtocol));
                }

                if (item.BaseType == typeof(AbstractEventBus))
                {
                    // 注册事件总线，用来传送消息
                    var eventName=item.Name.RemovePostFix(StringComparison.OrdinalIgnoreCase, "Receiver");
                    builder
                        .RegisterType(item)
                        .SingleInstance()
                        .As<IEventBus>().Named(eventName, typeof(IEventBus));
                }
            }
            builder.Populate(services);
            // base.RegisterServices(builder);
        }

        //public virtual List<Assembly> GetAssemblies()
        //{
        //    var assems = new List<Assembly>();
        //    var assemblyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "protocol");
        //    if (!Directory.Exists(assemblyPath))
        //        return null;
        //    var fiels = Directory.GetFiles(assemblyPath);
        //    if (fiels.Any())
        //    {
        //        foreach (var filePath in fiels)
        //        {
        //            var file = new FileInfo(filePath);
        //            if (file.Name.StartsWith("ESTHost.Protocol."))
        //            {
        //                var an = AssemblyName.GetAssemblyName(filePath);
        //                AppDomain.CurrentDomain.Load(an);
        //                //assems.Add();
        //            }
        //        }
        //    }
        //    return assems;
        //}
    }
}
