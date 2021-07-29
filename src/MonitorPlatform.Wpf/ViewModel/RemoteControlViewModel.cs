using EasyCaching.Core;
using ESTCore.ORM.FreeSql;
using FreeSql;
using MonitorPlatform.Contracts;
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Share.ServerCache;
using MonitorPlatform.Wpf.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.ViewModel
{
    /// <summary>
    /// 远程控制 视图实体
    /// </summary>
    public class RemoteControlViewModel : NotifyBase
    {
        IRedisCachingProvider redisCachingProvider;
        readonly IBaseRepository<Monitor, Guid> monitorRepository;
        readonly IBaseRepository<Terminal, Guid> terminalRepository;
        readonly IBaseRepository<Device, Guid> deviceRepository;
        readonly IBaseRepository<DeviceRltTerminal, Guid> deviceRltRepository;
        readonly IBaseRepository<TerminalRltSensor, Guid> terminalRltSensorRepository;
        public RemoteControlViewModel(IRedisCachingProvider redisCachingProvider = null)
        {
            this.monitorRepository = ESTRepository.Builder<Monitor, Guid>();
            this.deviceRepository = ESTRepository.Builder<Device, Guid>();
            this.deviceRltRepository = ESTRepository.Builder<DeviceRltTerminal, Guid>();
            this.terminalRepository = ESTRepository.Builder<Terminal, Guid>();
            this.terminalRltSensorRepository = ESTRepository.Builder<TerminalRltSensor, Guid>(); ;
            this.redisCachingProvider = redisCachingProvider;
        }

        /// <summary>
        /// 同步更新远程缓存数据，调用redis 相关方法
        /// </summary>
        public void UpdateMonitorData()
        {
            Task.Factory.StartNew(() => {
                // 数据格式： 协议名称：传感器：
                //                    服务信息：
                // todo 
                //[Display(Name = "银澳WTR-31协议")]
                //[Description("银澳WTR-31协议")]
                //WTR_31,
                //[Display(Name = "银澳WTR-20A协议")]
                //[Description("银澳WTR-20A协议")]
                //WTR_20A
                // 根据协议进行分类查询

                //DeviceCacheItem

                // 查找wtr31协议的设备以及站点
                var wtr31Cache = new List<DeviceCacheItem>();
                var device_wtr31 =
                monitorRepository.Orm.
                Select<Monitor, Device>()
                .Where((m, d) => d.PtotocolType == PtotocolType.WTR_31)
                .ToList<Device>();
                device_wtr31?.ForEach(a =>
                {
                    var cache = ObjectMapper.Map<DeviceCacheItem>(a);
                    //var termianl=terminalRepository.Select()
                    //cache.Terminal=
                });



                var wtr31 = new CacheDto();
                wtr31.Key = nameof(PtotocolType.WTR_31);
                wtr31.Terminals = new List<TerminalDto>();

                // 根据设备绑定的终端信息进行查询，对没有绑定设备的终端不进行处理。
               // var devices = deviceRepository.Where(a => true)
               // .ToList();

               // var wtr31Terminals = this.terminalRepository
               //.Where(a => a.Ptotocol == Share.PtotocolType.WTR_31).ToList();
               // wtr31Terminals?.ForEach(a =>
               // {
               //     var tdto = new TerminalDto();
               //     tdto.Sensors=terminalRltSensorRepository
               //     .Orm.Select<Terminal,TerminalRltSensor>()
               //     .Where((t,s)=>t.Id==a.Id&&s.TerminalId==t.Id)
               //     .ToList<Sensor>();
               //     tdto.Terminal = a;
               //     wtr31.Terminals.Add(tdto);
               // });
               // wtr31.Device=









               



            });
        }

        /// <summary>
        /// 重启远程终端服务
        /// </summary>
        public void RestartTerminalServver()
        {
            Task.Factory.StartNew(() => {

                // todo 
            });
        }

        /// <summary>
        /// 远程写入传感器信息
        /// </summary>
        public void WriteSensorData()
        {
            Task.Factory.StartNew(() => { 
            
                // todo 
            });
        }

        /// <summary>
        /// 重启短信服务
        /// </summary>

        public void RestartSmsServer()
        {
            Task.Factory.StartNew(() => {

                // todo 
            });
        }
    }
}
