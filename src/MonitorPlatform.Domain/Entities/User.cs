///**********************************************************************
//*******命名空间： MonitorPlatform.Domain.Entities
//*******类 名 称： User
//*******类 说 明： 用户表实体
//*******作    者： Easten
//*******机器名称： DESKTOP-EC8U0GP
//*******CLR 版本： 4.0.30319.42000
//*******创建时间： 7/3/2021 9:53:05 PM
//*******联系方式： 1301485237@qq.com
//***********************************************************************
//******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
//***********************************************************************
// */


using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    /// 用户表实体
    /// </summary>
    public class User : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EmailVerified { get; set; }
        public string PasswordVerified { get; set; }
    }
}
