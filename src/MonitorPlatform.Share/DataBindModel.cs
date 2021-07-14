/**********************************************************************
*******命名空间： MonitorPlatform.Share
*******类 名 称： DataBindModel
*******类 说 明： 数据绑定实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 11:45:29 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Share
{
    public class DataBindModel
    {
        public DataBindModel(Guid id,string name)
        {
            this.Id = id;   
            this.Name = name;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }
}
