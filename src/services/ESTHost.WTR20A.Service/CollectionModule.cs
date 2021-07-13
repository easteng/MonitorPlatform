/**********************************************************************
*******命名空间： ESTHost.WTR20A.Service
*******类 名 称： CollectionModule
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 2:49:31 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Caching;

using Silky.Lms.Core.Modularity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.WTR20A.Service
{
    [DependsOn(typeof(ESTRedisCacheModule))]
    public class CollectionModule : StartUpModule
    {
    }
}
