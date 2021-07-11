/**********************************************************************
*******命名空间： MonitorPlatform.DataAccess
*******类 名 称： IMonitorPlatformDBContext
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 6:01:15 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using FreeSql;

using MonitorPlatform.Domain.Entities;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.DataAccess
{
    public class MonitorPlatformDBContext:DbContext
    {
        public DbSet<User> User { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //var freeSql = ServiceLocator.GetService<IFreeSql>();
            //base.OnConfiguring(options);
            //options.UseFreeSql(freeSql);
        }
        protected override void OnModelCreating(ICodeFirst codefirst)
        {
            codefirst.Entity<User>(option =>
            {
                option.ToTable(nameof(User));
            });

            codefirst.SyncStructure<User>();

            base.OnModelCreating(codefirst);
        }
    }
}
