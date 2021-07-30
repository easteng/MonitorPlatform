/**********************************************************************
*******命名空间： ESTHost.Protocol
*******类 名 称： ProtocolFactory
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 3:38:38 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */

using ESTCore.ORM.FreeSql;

using MonitorPlatform.Domain;

using Silky.Lms.Core;

using System;
using System.Threading.Tasks;

namespace ESTHost.ProtocolBase
{
    /// <summary>
    ///  协议工厂，用来创建协议适用
    /// </summary>
    public class ProtocolFactory
    {
        public ProtocolFactory()
        {

        }
        /// <summary>
        /// 启动数据采集
        /// </summary>
        public static async Task StartupProtocolProvider()
        {
            var protocols = EngineContext.Current.ResolveAll<IBaseProtocol>();

            // 向数据库种注册服务类型，默认是没有协议的
            var sqlprovider = ESTRepository.Builder<Protocol, Guid>();

            foreach (var item in protocols)
            {
                if (!sqlprovider.Where(a => a.Name == item.Name).Any())
                {
                    var protocol = new Protocol() { Name = item.Name };
                    await sqlprovider.InsertAsync(protocol);
                }
                //启动服务
                await item.StartAsync();
                await item.ExecuteAsync();
            }
        }
    }
}
