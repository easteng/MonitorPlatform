using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MonitorPlatform.Share
{
    /// <summary>
    /// 服务类型
    /// </summary>
    public enum ServerType
    {
        [Display(Name = "数据中心服务")]
        [Description("数据中心服务")]
        DataCenterService,
        [Display(Name = "短信发送服务")]
        [Description("短信发送服务")]
        SmsService,
        [Display(Name = "数据存储服务")]
        [Description("数据存储服务")]
        StorageService,
        [Display(Name = "WTR31数据协议服务")]
        [Description("WTR31数据协议服务")]
        WTR31Service,
        [Display(Name = "WTR20A数据协议")]
        [Description("WTR20A数据协议")]
        WTR20AService,
    }
}
