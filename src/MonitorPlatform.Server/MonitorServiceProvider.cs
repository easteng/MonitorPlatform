/**********************************************************************
*******命名空间： MonitorPlatform.Server
*******类 名 称： MonitorServiceProvider
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 3:31:33 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using Autofac;

using FreeSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Domain.IServices;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorPlatform.Server
{
    public class MonitorServiceProvider : IMonitorServiceProvider,IHostedService
    {
        readonly IConfiguration _config;
        readonly IUserRepositoryService _userRepository;
        readonly IBaseRepository<User> baseRepository;
        public MonitorServiceProvider(IConfiguration config, IUserRepositoryService userRepository = null, IBaseRepository<User> baseRepository = null)
        {
            _userRepository = userRepository;
            _config = config;
            this.baseRepository = baseRepository;
        }
        public async Task Start()
        {
            // throw new NotImplementedException();
            var list =await _userRepository.GetAllUsersAsync();
            //var list= baseRepository.Where(a=>true).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("数据获取成功");
            //return Task.CompletedTask;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task Stop()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
