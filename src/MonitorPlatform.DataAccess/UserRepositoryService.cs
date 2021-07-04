/**********************************************************************
*******命名空间： MonitorPlatform.DataAccess
*******类 名 称： UserRepositoryService
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 6:10:45 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Domain.IServices;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.DataAccess
{
    public class UserRepositoryService: IUserRepositoryService
    {
        readonly MonitorPlatformDBContext _dbContext;
        public UserRepositoryService(MonitorPlatformDBContext dbContext = null)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var list=await _dbContext.User.Where(a=>true).ToListAsync();
            return list;
        }
    }
}
