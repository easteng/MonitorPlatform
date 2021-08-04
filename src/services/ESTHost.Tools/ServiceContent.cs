/**********************************************************************
*******命名空间： ESTHost.Tools
*******类 名 称： ServiceContent
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/4/2021 9:56:49 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Tools
{
    /// <summary>
    ///  服务内容
    /// </summary>
    public class ServiceContent
    {
        public int Index { get; set; }
        // 服务名称
        public string ServerName { get; set; }
        // 服务描述
        public string ServiceDesc { get; set; }
        // 服务状态
        public ServiceStatus Status { get; set; }
        // 文件路径
        public string BinPath { get; set; }
        public List<FileInfo> FileInfos { get; set; }
        public class FileInfo
        {
            public string FileName { get; set; }
            public string Md5 { get; set; }
            public bool CanUpdate { get; set; }
            public bool IsExe { get; set; }
        }
    }
    public enum ServiceStatus
    {
        NoInstalled,
        Runing,
        Stoped,
        CanUpdate
    }
}
