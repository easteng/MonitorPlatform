/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： SensorManagerViewModel
*******类 说 明： 传感器管理
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 10:04:06 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

using MonitorPlatform.Domain.Entities;
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
    public class SensorManagerViewModel : NotifyBase
    {
        private SensorModel sensorModel;

        public SensorModel SensorModel
        {
            get { return sensorModel; }
            set { sensorModel = value; this.DoNotify(); }
        }
        private List<SensorModel> sensorList;

        public List<SensorModel> SensorList
        {
            get { return sensorList; }
            set { sensorList = value; this.DoNotify(); }
        }
        private List<TerminalModel> terminals;
        public List<TerminalModel> Terminals
        {
            get { return terminals; }
            set { terminals = value; this.DoNotify(); }
        }

        private bool show;

        public bool Show
        {
            get { return show; }
            set { show = value; this.DoNotify(); }
        }
        private bool showTerminal;

        public bool ShowTermainal
        {
            get { return showTerminal; }
            set { showTerminal = value;this.DoNotify(); }
        } 

        /// <summary>
        /// 复选列的宽度
        /// </summary>

        private int columnWidth;

        public int ColumnWidth
        {
            get { return columnWidth; }
            set { columnWidth = value; this.DoNotify(); }
        }

        public ICommand SaveCommand { get { return new CommandBase(SaveAction); } }
        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand SearchCommand { get { return new CommandBase(SearchAction); } }
        public ICommand CreateCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        readonly IBaseRepository<DeviceRltTerminal, Guid> deviceRltTerminalRepository;
        readonly IBaseRepository<Terminal, Guid> terminalRepository;
        readonly IBaseRepository<TerminalRltSensor, Guid> terminalTltsensorRepository;
        readonly IBaseRepository<PowerRoom, Guid> powerRepository;

        public SensorManagerViewModel()
        {
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();
            deviceRltTerminalRepository = ESTRepository.Builder<DeviceRltTerminal, Guid>();
            terminalRepository = ESTRepository.Builder<Terminal, Guid>();
            terminalTltsensorRepository = ESTRepository.Builder<TerminalRltSensor, Guid>();
            powerRepository = ESTRepository.Builder<PowerRoom, Guid>();
            this.Refresh();


            var terminals=terminalRepository.Where(a=>true).ToList();
            Terminals = ObjectMapper.Map<List<TerminalModel>>(terminals);
        }

        private void Refresh(string code = "")
        {
            Task.Run(() =>
            {
                var list = sensorRepository
                .WhereIf(code != "", a => a.SensorCode.Contains(code))
                .Where(a => true)
                .ToList();
                this.SensorList = ObjectMapper.Map<List<SensorModel>>(list).CreateIndex();
            });
        }
        /// <summary>
        /// 打开抽屉
        /// </summary>
        /// <param name="data"></param>
        public void OpenDrawAction(object data)
        {
            this.SensorModel=new SensorModel();
            this.Show = bool.Parse(data.ToString());
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchAction(object obj)
        {
            if(obj is string)
            {
                this.Refresh(obj.ToString());
            }
        }
        public void EditAction(object obj)
        {
            var sensor=sensorRepository.Get(Guid.Parse(obj.ToString()));
            var sensorModel = ObjectMapper.Map<SensorModel>(sensor);
            this.SensorModel = sensorModel;
            this.Show = true; 
        }
        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="obj"></param>
        public void SaveAction(object obj)
        {
            
            var sensorModel=(SensorModel)obj;
            if (sensorModel.SensorCode.Length > 10)
            {
                Growl.Error("传感器编码不能超过10位");
                return;
            }
            var sensor=ObjectMapper.Map<Sensor>(sensorModel);
            if (sensor.Id != Guid.Empty)
            {
                sensorRepository.Update(sensor);
            }
            else
            {
                sensorRepository.Insert(sensor); 
            }

            this.Show = false;
            Growl.Info("操作成功");
            this.Refresh();
        }
        public void DeleteAction(object obj)
        {
            if (obj != null)
            {
                var id = Guid.Parse(obj.ToString());
                sensorRepository.Delete(id);
                this.Refresh();
            }
        }


        /// <summary>
        /// 根据配电室id获取下面的采集器
        /// </summary>
        /// <param name="id"></param>
        public void QueryTerminalByPowerId(Guid id )
        {
            var list = powerRepository.Select
                .Where(a => a.Id == id)
                .IncludeMany(a => a.Terminals).ToList()
                .SelectMany(a=>a.Terminals).ToList();
            this.Terminals = ObjectMapper.Map<List<TerminalModel>>(list).CreateIndex();
        }

        /// <summary>
        /// 获取指定采集器中的传感器
        /// </summary>
        /// <param name="id"></param>
        public void GetSensorByTerminal(Guid id)
        {
            var sensor = terminalRepository
                .Select
                .Where(a => a.Id == id)
                .IncludeMany(a=>a.Sensors).ToList()
                .SelectMany(a=>a.Sensors).ToList(); 
            this.SensorList = ObjectMapper.Map<List<SensorModel>>(sensor).CreateIndex();
        }
    }
}
