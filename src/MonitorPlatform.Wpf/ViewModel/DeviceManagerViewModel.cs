/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： DeviceManagerViewModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/13/2021 11:29:19 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

using Masuit.Tools.Systems;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class DeviceManagerViewModel: NotifyBase
    {
        public Guid SelectId { get; set;  }
        // 临时选中的值
        public List<Guid> SelectList { get; set; } = new List<Guid>();
        // 当前的模式  是绑定传感器还是绑定客户端
        private bool BindSensor { get;set;  }
        // 设备
        private DeviceModel deviceModel;

        public DeviceModel DeviceModel
        {
            get { return deviceModel; }
            set { deviceModel = value; this.DoNotify(); }
        }
        // 设备列表
        private List<DeviceModel> devices;

        public List<DeviceModel> Devices
        {
            get { return devices; }
            set { devices = value; this.DoNotify(); }
        }
        //设备绑定的传感器列表
        private List<SensorModel> sensors;

        public List<SensorModel> Sensors
        {
            get { return sensors; }
            set { sensors = value; this.DoNotify(); }
        }

        // 设备绑定的采集客户端
        private List<TerminalModel> terminalList;

        public List<TerminalModel> TerminalList
        {
            get { return terminalList; }
            set { terminalList = value; this.DoNotify(); }
        }

        // 设备模式
        private List<ComboxItem> deviceTypes;

        public List<ComboxItem> DeviceTypes
        {
            get { return deviceTypes; }
            set { deviceTypes = value; this.DoNotify(); }
        }
        // 需要绑定的数据源
        private List<TerminalModel> bindModels;

        public List<TerminalModel> BindModels
        {
            get { return bindModels; }
            set { bindModels = value; this.DoNotify(); }
        }

        // 抽屉
        private bool show;
        public bool Show
        {
            get { return show; }
            set { show = value; this.DoNotify(); }
        }

        // 关联的抽屉
        private bool bottomShow;

        public bool BottomShow
        {
            get { return bottomShow; }
            set { bottomShow = value; this.DoNotify(); }
        }

        public ICommand SaveCommand { get { return new CommandBase(SaveAction); } }
        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand SearchCommand { get { return new CommandBase(SearchAction); } }
        //public ICommand DebuggerCommand { get { return new CommandBase(OpenBottomAction); } }
        public ICommand CreateCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        public ICommand BindClientCommand { get { return new CommandBase(BindClientAction); } }
        public ICommand BindDataSaveComand { get { return new CommandBase(SaveBindAction); } }
        readonly IBaseRepository<Device,Guid> deviceRepository;
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        readonly IBaseRepository<DeviceRltTerminal, Guid> rltClientRepository;
        readonly IBaseRepository<Terminal, Guid> terminalRepository;
        public DeviceManagerViewModel()
        {
            DeviceTypes = new List<ComboxItem>();
            deviceRepository = ESTRepository.Builder<Device, Guid>();
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();
            rltClientRepository = ESTRepository.Builder<DeviceRltTerminal, Guid>();
            terminalRepository = ESTRepository.Builder<Terminal, Guid>();
            var dic2 = typeof(DeviceCollectionType).GetDescriptionAndValue();
            foreach (var item in dic2)
            {
                DeviceTypes.Add(new ComboxItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }

            var list= terminalRepository.Where(a=>true).ToList();
            this.TerminalList = ObjectMapper.Map<List<TerminalModel>>(list).CreateIndex();

            this.Refresh();
        }
        // 刷新数据
        private void Refresh(string code = "")
        {
            Task.Run(() =>
            {
                var list = deviceRepository
                .WhereIf(code != "", a => a.Name.Contains(code))
                .Where(a => true)
                .ToList();
                this.Devices = ObjectMapper.Map<List<DeviceModel>>(list).CreateIndex();
                
            });
        }

        // 创建设备
        public void OpenDrawAction(object data)
        {
            this.Show=bool.Parse(data.ToString());
            this.DeviceModel = new DeviceModel();
        }

        // 查询
        private void SearchAction(object obj)
        {
            if (obj is string)
            {
                this.Refresh(obj.ToString());
            }
        }
        // 编辑数据
        public void EditAction(object obj)
        {
            var id = Guid.Parse(obj.ToString());
            var sensor = deviceRepository.Get(id);
            var model = ObjectMapper.Map<DeviceModel>(sensor);
            this.DeviceModel = model;
            this.Show = true;
        }
        // 保存数据
        public void SaveAction(object obj)
        {
            var collectionModel = (DeviceModel)obj;
            var collection = ObjectMapper.Map<Device>(collectionModel);
            if (collection.Id != Guid.Empty)
            {
                deviceRepository.Update(collection);
            }
            else
            {
                deviceRepository.Insert(collection);
            }

            this.Show = false;
            Growl.Info("操作成功");
            this.Refresh();
        }
        // 删除数据
        public void DeleteAction(object obj)
        {
            if (obj != null)
            {
                var id = Guid.Parse(obj.ToString());
                deviceRepository.Delete(a=>a.Id==id);
                this.Refresh();
            }
        }

        // 删除绑定的终端信息
        public void DelectBindData(Guid id)
        {
            rltClientRepository.Delete(id);
            this.QueryBindTerminalAction(this.SelectId);
        }


        // 绑定采集终端操作
        public void BindClientAction(object data)
        {
            if (this.SelectId == Guid.Empty)
            {
                HandyControl.Controls.MessageBox.Show("请先选中设备", "温馨提示");
                return;
            }
            this.BottomShow = true;
            var list = rltClientRepository.Where(a => true).ToList()
              ?.Select(a => a.Terminal)?.ToList();
            this.BindModels = ObjectMapper.Map<List<TerminalModel>>(list);
        }
        // 保存绑定数据
        public void SaveBindAction(object data)
        {
            this.BottomShow = false;
            if (bool.Parse(data.ToString()))
            {
                this.SelectList?
                       .ForEach(a =>
                       {
                           var entity = new DeviceRltTerminal(this.SelectId, a);
                           rltClientRepository.Insert(entity);
                       });
                Growl.Info("操作成功");
                QueryBindTerminalAction(this.SelectId);
            }
        }

        // 选中设备,查询关联的数据
        /// <summary>
        /// 查询绑定的采集终端
        /// </summary>
        /// <param name="data"></param>
        public void QueryBindTerminalAction(object data)
        {
            var id=Guid.Parse(data.ToString());
            if (id != Guid.Empty)
            {
                SelectId = id;
                var clients = deviceRepository.
                    Orm.Select<DeviceRltTerminal, Terminal>()
                    .Where((a, b) => a.DeviceId == id && a.TerminalId == b.Id)
                    .ToList<Terminal>();
                this.BindModels = ObjectMapper.Map<List<TerminalModel>>(clients).CreateIndex();
            }
            this.SelectList.Clear();
        }

        // 设置选中
        public void SetChecked(Guid id)
        {
            // 不存在
            if (!this.SelectList.Any(a => a== id))
            {
                this.SelectList.Add(id);
            }
        }
        // 取消选中
        public void SetUnChecked(Guid id)
        {
            if (this.SelectList.Any(a => a== id))
            {
                this.SelectList.Remove(id);   
            }
        }
    }
}
