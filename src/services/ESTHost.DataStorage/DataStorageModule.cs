/**********************************************************************
*******命名空间： ESTHost.DataStorage.Service
*******类 名 称： DataStorageModule
*******类 说 明： 数据存储模块
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 2:48:21 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Caching;
using ESTCore.ORM.FreeSql;

using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.DataStorage.Service
{
    /// <summary>
    ///  数据存储模块
    /// </summary>
    [DependsOn(typeof(FreeSqlModule), typeof(ESTRedisCacheModule))]
    public class DataStorageModule : StartUpModule
    {

    }
}
