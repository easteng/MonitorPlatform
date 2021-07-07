/**********************************************************************
*******命名空间： MonitorPlatform.Domain.Entities
*******类 名 称： DataDictionary
*******类 说 明： 数据字典表实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 11:34:27 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Domain.Entity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Domain.Entities
{
    /// <summary>
    /// 数据字典表实体
    /// </summary>
    public class DataDictionary:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Guid? ParentId { get; set; }
        public DataDictionary Parent { get; set; }
    }
}
