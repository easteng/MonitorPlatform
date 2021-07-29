using EasyCaching.Core;
using ESTCore.ORM.FreeSql;
using FreeSql;
using MonitorPlatform.Contracts;
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Share.ServerCache;
using MonitorPlatform.Wpf.Common;

using Newtonsoft.Json;

using Silky.Lms.Core;

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
        readonly IFreeSql fsql;
        public RemoteControlViewModel()
        {
            this.monitorRepository = ESTRepository.Builder<Monitor, Guid>();
            this.deviceRepository = ESTRepository.Builder<Device, Guid>();
            this.deviceRltRepository = ESTRepository.Builder<DeviceRltTerminal, Guid>();
            this.terminalRepository = ESTRepository.Builder<Terminal, Guid>();
            this.terminalRltSensorRepository = ESTRepository.Builder<TerminalRltSensor, Guid>(); ;
            this.redisCachingProvider = EngineContext.Current.Resolve<IRedisCachingProvider>();
            this.fsql = EngineContext.Current.Resolve<IFreeSql>();

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

                var monitors = monitorRepository.Where(a => a.DeviceId != null).ToList<Monitor>()
              .Select(a => a.DeviceId).ToList();
                var devices = deviceRepository.Where(a => monitors.Contains(a.Id)).ToList();


                var wtr31Cache = new List<DeviceCacheItem>();
                var device_wtr31 =
                 devices
                .Where(a=> a.PtotocolType == PtotocolType.WTR_31)
                .ToList<Device>();
                device_wtr31?.ForEach(a =>
                {
                    var cache = ObjectMapper.Map<DeviceCacheItem>(a);
                    var terminal = deviceRltRepository.Orm.Select<DeviceRltTerminal, Terminal>()
                   .Where((d, t) => d.DeviceId == a.Id && d.TerminalId == t.Id).ToList((d, t) => new TerminalCacheItem
                   {
                       Addr = int.Parse(t.Addr),
                       Name = t.Name,
                       AlertValue = t.AlertValue,
                       TolerantValue = t.TolerantValue,
                       WarinValue = t.WarinValue,
                       Id = t.Id
                   });
                    terminal?.ForEach(d =>
                    {
                        var sensor = terminalRltSensorRepository.Orm.Select<TerminalRltSensor, Sensor>()
                        .Where((t, s) => t.TerminalId == d.Id && t.SensorId == s.Id).ToList<Sensor>();
                        d.SensorCount = sensor.Count();
                    });
                    cache.Terminal = terminal;
                    wtr31Cache.Add(cache);
                });

                var wtr20ACache = new List<DeviceCacheItem>();
                var device_20A = devices
                .Where(a => a.PtotocolType == PtotocolType.WTR_20A)
                .ToList();

                device_20A?.ForEach(a =>
                {
                    var cache = ObjectMapper.Map<DeviceCacheItem>(a);
                    var terminal = deviceRltRepository.Orm.Select<DeviceRltTerminal, Terminal>()
                    .Where((d, t) => d.DeviceId == a.Id && d.TerminalId == t.Id).ToList((d,t) =>new TerminalCacheItem { 
                        Addr=int.Parse(t.Addr),
                        Name=t.Name,
                        AlertValue=t.AlertValue,  
                        TolerantValue=t.TolerantValue,
                        WarinValue=t.WarinValue ,
                        Id= t.Id
                    });
                    terminal?.ForEach(d =>
                    {
                        var sensor = terminalRltSensorRepository.Orm.Select<TerminalRltSensor, Sensor>()
                        .Where((t, s) => t.TerminalId == d.Id && t.SensorId == s.Id).ToList<Sensor>();
                        d.SensorCount = sensor.Count();
                    });
                    cache.Terminal = terminal;
                    wtr20ACache.Add(cache);
                });

                // 设备以及协议缓存
                if(wtr20ACache.Any())
                    redisCachingProvider.StringSet("Device:WTR_20A", JsonConvert.SerializeObject(wtr20ACache));
                if (wtr31Cache.Any())
                    redisCachingProvider.StringSet("Device:WTR_31", JsonConvert.SerializeObject(wtr31Cache));

                // 采集器终端
                


                // 终端及传感器缓存
                var terminals = terminalRepository.Where(a => true).ToList();

                terminals?.ForEach(a =>
                {
                    // 查找关联的传感器
                    var sensor=terminalRltSensorRepository.Orm.Select<TerminalRltSensor, Sensor>()
                    .Where((t,s)=>t.TerminalId==a.Id&&t.SensorId==s.Id).ToList<Sensor>();

                    var sensorCaches = new List<SensorCacheItem>();
                    // 需要对每一个采集的传感器进行编号
                    var srlt = terminalRltSensorRepository.Orm.Select<TerminalRltSensor, Sensor>()
                    .Where((t, s) => t.TerminalId == a.Id && t.SensorId == s.Id).ToList<TerminalRltSensor>();
                    if (srlt.Any())
                    {
                        var index = 0;
                        foreach (var item in srlt)
                        {
                            var s = new SensorCacheItem();
                            item.SensorNo = ++index;
                            terminalRltSensorRepository.Update(item);
                            s.SensorNo = index;
                            s.SensorCode = sensor?.FirstOrDefault(a=>a.Id==item.Id).SensorCode;
                            s.Position = sensor?.FirstOrDefault(a => a.Id == item.Id).Position;
                            sensorCaches.Add(s);
                        }
                    }

                    // 添加缓存
                    redisCachingProvider.StringSet($"Terminal:Sensor:{a.Id}", JsonConvert.SerializeObject(sensorCaches));
                    // 把所有的采集器都添加到缓存
                    redisCachingProvider.StringSet($"Terminal:{a.Id}", JsonConvert.SerializeObject(a));
                });
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
