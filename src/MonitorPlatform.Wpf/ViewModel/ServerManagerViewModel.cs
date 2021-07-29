/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： ServerManagerViewModel
*******类 说 明： 采集器配置
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:09:35 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Client;
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

using Masuit.Tools.Systems;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class ServerManagerViewModel : NotifyBase
    {
        private TerminalModel terminalModel;

        public TerminalModel TerminalModel
        {
            get { return terminalModel; }
            set { terminalModel = value; this.DoNotify(); }
        }

        private List<TerminalModel> terminalList;

        public List<TerminalModel> TerminalList
        {
            get { return terminalList; }
            set { terminalList = value; this.DoNotify(); }
        }
        private List<ComboxItem> ptotocolList;
        public List<ComboxItem> PtotocolList
        {
            get => ptotocolList;
            set
            {
                ptotocolList=value;
                this.DoNotify();
            }
        }

        //设备绑定的传感器列表
        private List<SensorModel> sensors;

        public List<SensorModel> Sensors
        {
            get { return sensors; }
            set { sensors = value; this.DoNotify(); }
        }
        private bool show;

        public bool Show
        {
            get { return show; }
            set { show = value; this.DoNotify(); }
        } 
        private bool bottomShow;

        public bool BottomShow
        {
            get { return bottomShow; }
            set { bottomShow = value; this.DoNotify(); }
        }

        public ICommand SaveCommand { get { return new CommandBase(SaveAction); } }
        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand SearchCommand { get { return new CommandBase(SearchAction); } }
        public ICommand DebuggerCommand { get { return new CommandBase(OpenBottomAction); } }
        public ICommand CreateCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        readonly IBaseRepository<Terminal, Guid> terminalRepository;
        readonly IBaseRepository<DeviceRltTerminal, Guid> deviceRltRepository;
        readonly IBaseRepository<TerminalRltSensor, Guid> terminalRltSensorRepository;
        readonly IMessageClientProvider messageClientProvider;
           
        private Guid SelectedTerminalId { get; set; }
        public ServerManagerViewModel()
        {
            deviceRltRepository = ESTRepository.Builder<DeviceRltTerminal, Guid>();
            terminalRepository = ESTRepository.Builder<Terminal, Guid>();
            terminalRltSensorRepository = ESTRepository.Builder<TerminalRltSensor, Guid>();
            PtotocolList=new  List<ComboxItem>();
            this.messageClientProvider = EngineContext.Current.Resolve<IMessageClientProvider>();
            var dic1 = typeof(PtotocolType).GetDescriptionAndValue();//协议类型
            var dic2 = typeof(DeviceCollectionType).GetDescriptionAndValue();//采集类型
            foreach (var item in dic1)
            {
                PtotocolList.Add(new ComboxItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
            this.Refresh();
        }
        // 刷新数据
        private void Refresh(string code = "")
        {
            Task.Run(() =>
            {
                var list = terminalRepository
                .WhereIf(code != "", a => a.Name.Contains(code))
                .Where(a => true)
                .ToList();
                this.TerminalList = ObjectMapper.Map<List<TerminalModel>>(list).CreateIndex();
            });
        }

        /// <summary>
        /// 打开抽屉
        /// </summary>
        /// <param name="data"></param>
        public void OpenDrawAction(object data)
        {
            this.TerminalModel = new TerminalModel();
            this.Show = bool.Parse(data.ToString());
        }
        /// <summary>
        /// 打开底部的抽屉
        /// </summary>
        /// <param name="data"></param>
        public void OpenBottomAction(object data)
        {
            this.BottomShow = bool.Parse(data.ToString());
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchAction(object obj)
        {
            if (obj is string)
            {
                this.Refresh(obj.ToString());
            }
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="obj"></param>
        public void EditAction(object obj)
        {
            var sensor = terminalRepository.Get(Guid.Parse(obj.ToString()));
            var model = ObjectMapper.Map<TerminalModel>(sensor);
            this.TerminalModel = model;
            this.Show = true;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="obj"></param>
        public void SaveAction(object obj)
        {

            var collectionModel = (TerminalModel)obj;
            var collection = ObjectMapper.Map<Terminal>(collectionModel);
            if (collection.Id != Guid.Empty)
            {
                terminalRepository.Update(collection);
            }
            else
            {
                terminalRepository.Insert(collection);
            }

            this.Show = false;
            Growl.Info("操作成功");
            this.Refresh();
        }

        /// <summary>
        /// 查询终端采集器关联的传感器
        /// </summary>
        /// <param name="data"></param>
        public void QueryBindSensorAction(object data)
        {
            var id = Guid.Parse(data.ToString());
            this.SelectedTerminalId = id;
            if (id != Guid.Empty)
            {
                var sensors =
                    terminalRltSensorRepository.
                    Orm.Select<TerminalRltSensor, Sensor>()
                    .Where((a, b) => a.TerminalId == id && a.SensorId == b.Id)
                    .ToList<Sensor>();
                this.Sensors = ObjectMapper.Map<List<SensorModel>>(sensors).CreateIndex();
            }
        }

        /// <summary>
        /// 保存关联的传感器信息
        /// </summary>
        /// <param name="sensorIds"></param>
        public bool SaveRelSensor(List<Guid> sensorIds)
        {
            if (sensorIds.Any()){
                sensorIds.ForEach(a =>
                {
                    var model = new TerminalRltSensor()
                    {
                        SensorId = a,
                        TerminalId = this.SelectedTerminalId,
                    };
                    // 判断是否已经存在
                    if (!terminalRltSensorRepository.Where(b => b.SensorId == a&&b.TerminalId==this.SelectedTerminalId).Any())
                    {
                        terminalRltSensorRepository.Insert(model);
                    }
                });
                Growl.Info("关联成功");

                QueryBindSensorAction(this.SelectedTerminalId);
            }
            return true;
        }

        /// <summary>
        /// 删除关联的传感器
        /// </summary>
        /// <param name="id"></param>
        public void DelteRltSensor(Guid id)
        {
            terminalRltSensorRepository.Delete(a=>a.SensorId==id);
            Growl.Info("删除成功");
            QueryBindSensorAction(this.SelectedTerminalId);
        }


        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteAction(object obj)
        {
            if (obj != null)
            {
                var id = Guid.Parse(obj.ToString());
                terminalRepository.Delete(id);
                this.Refresh();
            }
        }

        /// <summary>
        /// 停止远程服务
        /// </summary>
        /// <param name="obj"></param>
        public void StopServerAction(object obj)
        {
            // todo 
        }
        /// <summary>
        /// 远程重启服务
        /// </summary>
        /// <param name="obj"></param>
        public void RestartServerAction(object obj)
        {
            // todo 
        }
        /// <summary>
        /// 挂起服务
        /// </summary>
        /// <param name="obj"></param>
        public void PuceServerAction(object obj)
        {
            // todo 
        }
        /// <summary>
        /// 远程写入传感器
        /// </summary>
        public void WriteSensor(Guid id)
        {
            // 查询当前采集器是否有传感器
            if(terminalRltSensorRepository.Where(a => a.TerminalId == id).Any())
            {
                var terminal=terminalRepository.Get(id);
                // 可以写入,调用消息发送接口
                var message = new RemoteControlMessage();
                message.ControlType = ControlType.Write;
                message.PtotocolType = terminal.Ptotocol;
                // 查找采集器关联的设备
                var device = deviceRltRepository.Orm.Select<DeviceRltTerminal, Device>()
                    .Where((a, b) => a.TerminalId == id && a.DeviceId == b.Id)
                    .ToList((c, d) =>new
                    {
                        DeviceId = d.Id
                    });
                if(device == null)
                {
                    Growl.Warning("当前采集器没有关联设备，不能执行数据写入");
                    return;
                }
                message.DeviceId = device[0].DeviceId;
                message.TerminalId = id;
                messageClientProvider.SendMessage(message);
                Growl.Info("写入命令已下发");
            }
            else
            {
                Growl.Warning("当前采集器没有关联任何传感器，不能执行数据写入");

                return;
            }

        }
    }
}
