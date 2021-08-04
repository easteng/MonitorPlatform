/**********************************************************************
*******命名空间： ESTHost.Tools
*******类 名 称： MainViewModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 8/4/2021 10:01:35 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESTHost.Tools
{
    /// <summary>
    /// 视图模型 
    /// </summary>
    public class MainViewModel:NotifyBase
    {
        /// <summary>
        /// 服务列表
        /// </summary>
        private List<ServiceContent> services;

        public List<ServiceContent> Services
        {
            get { return services; }
            set { services = value; }
        }

        /// <summary>
        /// 单个服务
        /// </summary>
        private ServiceContent service;

        public ServiceContent Service
        {
            get { return service; }
            set { service = value; }
        }


        public MainViewModel()
        {
            this.Service = new ServiceContent();
            this.Services = new List<ServiceContent>();
        }

        private void InitService()
        {
            this.Services.Add(new ServiceContent()
            {
                ServerName = "EST.DataCenter",
                ServiceDesc = "测温数据中心服务",
                BinPath = "",
                FileInfos = new List<ServiceContent.FileInfo>()
            }); ;
        }

        private void CreateDataCenter()
        {
            var service = new ServiceContent();
            service.BinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "");
            service.Index = 1;
            service.ServerName = "EST.DataCenter";
            service.ServiceDesc = "温度数据采集中心服务";
            service.Status = ServiceStatus.NoInstalled;
            service.FileInfos = new List<ServiceContent.FileInfo>();



        }

    }
}
