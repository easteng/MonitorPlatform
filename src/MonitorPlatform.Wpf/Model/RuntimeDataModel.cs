/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： RuntimeDataModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 9:36:58 AM
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
    public class RuntimeDataModel: NotifyBase
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; this.DoNotify(); }
        }

    }
}
