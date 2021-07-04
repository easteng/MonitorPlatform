/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： UserModel
*******类 说 明： 用户实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 10:06:23 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class UserModel:NotifyBase
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; this.DoNotify(); }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; this.DoNotify(); }
        }
    }
}
