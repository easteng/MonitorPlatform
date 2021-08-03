/**********************************************************************
*******命名空间： MonitorPlatform.Wpf
*******类 名 称： AutoMapperProfile
*******类 说 明： 实体映射定义
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 5:39:08 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using AutoMapper;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Wpf.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf
{
    public class AutoMapperProfile: Profile
    {
        public MapperConfiguration mapper;
        public AutoMapperProfile()
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserModel>();
                cfg.CreateMap<UserModel, User>();
                cfg.CreateMap<Sensor, SensorModel>();
                cfg.CreateMap<SensorModel, Sensor>();
             
                cfg.CreateMap<Monitor, MonitorModel>();
                cfg.CreateMap<MonitorModel, Monitor>();
                cfg.CreateMap<DiagramConfig, DiagramConfigModel>();
                cfg.CreateMap<DiagramConfigModel, DiagramConfig>();
                cfg.CreateMap<DiagramModel, Diagram>();
                cfg.CreateMap<Diagram, DiagramModel>();
                cfg.CreateMap<TemplateModel, TemplateStyle>();
                cfg.CreateMap<TemplateStyle, TemplateModel>();
                cfg.CreateMap<TemplateModel, TemplateModel>();
                cfg.CreateMap<TerminalModel, Terminal>();
                cfg.CreateMap<Terminal, TerminalModel>();
                cfg.CreateMap<SMSConfigModel, SmsConfig>();
                cfg.CreateMap<SmsConfig, SMSConfigModel>();


                // 站点
                cfg.CreateMap<Station, StationModel>();
                cfg.CreateMap<StationModel, Station>();

                // 配电室
                cfg.CreateMap<PowerRoom, PowerRoomModel>();
                cfg.CreateMap<PowerRoomModel, PowerRoom>();

                // 设备
                cfg.CreateMap<Device, DeviceModel>();
                cfg.CreateMap<DeviceModel, Device>();

            });
        }
    }

    public class ObjectMapper
    {
        static IMapper mapper;
        static ObjectMapper()
        {
            var pro = new AutoMapperProfile();
            mapper = pro.mapper.CreateMapper();
        }
        public static T Map<T>(object obj) where T:class
        {
            return mapper.Map<T>(obj);
        }
    }
}
