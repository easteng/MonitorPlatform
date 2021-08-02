/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： MonitorViewModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/14/2021 9:01:34 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.Message.Client;
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

using Masuit.Tools;
using Masuit.Tools.Systems;

using MonitorPlatform.Contracts;
using MonitorPlatform.Domain;
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Share.CacheItem;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class MonitorViewModel: NotifyBase
    {
        public event EventHandler<EventArgs> ReloadImage;
        public event EventHandler<List<DiagramConfigModel>> InitPoint;

        public Guid MonitorId { get; set; } //站点id
       
        public Guid SelectedTerminalId { get; set; } // 选中的终端id
        // 监测点内容
        private MonitorModel monitorModel;

        private readonly IMessageClientProvider messageClientProvider;

        #region 属性定义


        public Guid regionId { get; set; } //配电室id
        public Guid RegionId
        {
            get { return regionId; }
            set { regionId = value; this.DoNotify(); }
        }
        // 配置项实体
        private ConfigModel configModel;

        public ConfigModel ConfigModel
        {
            get { return configModel; }
            set { configModel = value; this.DoNotify(); }
        }

        // 监测点实体
        public MonitorModel MonitorModel
        {
            get { return monitorModel; }
            set { monitorModel = value; this.DoNotify(); }
        }

        // 监测点树 结构数据
        private List<MonitorModel> monitorModels;
        public List<MonitorModel> MonitorModels
        {
            get { return monitorModels; }
            set { monitorModels = value; this.DoNotify(); }
        }


        //  采集绑定的温度传感器
        private List<SensorModel> sensorList;
        public List<SensorModel> SensorList
        {
            get { return sensorList; }
            set { sensorList = value; this.DoNotify(); }
        }
        // 图纸内容
        private DiagramModel diagramModel;

        public DiagramModel DiagramModel
        {
            get { return diagramModel; }
            set { diagramModel = value; this.DoNotify(); }
        }
        // 图纸属性
        private DiagramConfigModel diagramConfigModel;

        public DiagramConfigModel DiagramConfigModel
        {
            get { return diagramConfigModel; }
            set { diagramConfigModel = value; this.DoNotify(); }
        }

        /// <summary>
        /// 配置属性列表
        /// </summary>
        private List<DiagramConfigModel> diagramConfigModels;

        public List<DiagramConfigModel> DiagramConfigModels
        {
            get { return diagramConfigModels; }
            set { diagramConfigModels = value; this.DoNotify(); }
        }


        // 温度模板样式配置
        private TemplateModel templateModel;

        public TemplateModel TemplateModel
        {
            get { return templateModel; }
            set { templateModel = value; this.DoNotify(); }
        }


        // 左侧抽屉
        private bool leftShow;
        public bool LeftShow
        {
            get { return leftShow; }
            set { leftShow = value; this.DoNotify(); }
        }
        // 右侧抽屉
        private bool rightShow;
        public bool RightShow
        {
            get { return rightShow; }
            set { rightShow = value; this.DoNotify(); }
        }

        // 底部抽屉
        private bool bottomShow;
        public bool BottomShow
        {
            get { return bottomShow; }
            set { bottomShow = value; this.DoNotify(); }
        }

        // 温度模板配置抽屉
        private bool configShow;
        public bool ConfigShow
        {
            get { return configShow; }
            set { configShow = value; this.DoNotify(); }
        } 

        // 顶部传感器抽屉
        private bool sensorShow;
        public bool SensorShow
        {
            get { return sensorShow; }
            set { sensorShow = value; this.DoNotify(); }
        }

        // 顶部采集器抽屉
        private bool terminalShow;
        public bool TerminalShow
        {
            get { return terminalShow; }
            set { terminalShow = value; this.DoNotify(); }
        } 
        
        private bool terminalRltShow;
        public bool TerminalRltShow
        {
            get { return terminalRltShow; }
            set { terminalRltShow = value; this.DoNotify(); }
        }

        // 监测点类型
        private List<ComboxItem> monitorTypes;

        public List<ComboxItem> MonitorTypes
        {
            get { return monitorTypes; }
            set { monitorTypes = value; this.DoNotify(); }
        }

        // 设备信息
        private DeviceModel deviceModel;

        public DeviceModel DeviceModel
        {
            get { return deviceModel; }
            set { deviceModel = value; this.DoNotify(); }
        }

        // 协议类型
        private List<string> protocols;

        public List<string> Protocols
        {
            get { return protocols; }
            set { protocols = value; this.DoNotify(); }
        }

        // 设备采集模式
        // 设备模式
        private List<string> deviceTypes;

        public List<string> DeviceTypes
        {
            get { return deviceTypes; }
            set { deviceTypes = value; this.DoNotify(); }
        }

        /// <summary>
        ///  配电室绑定的传感器信息
        private List<SensorModel> sensorModels;

        public List<SensorModel> SensorModels
        {
            get { return sensorModels; }
            set { sensorModels = value; this.DoNotify(); }
        }
        // 传感器信息
        private SensorModel sensorModel;
        public SensorModel SensorModel
        {
            get { return sensorModel; }
            set { sensorModel = value; this.DoNotify(); }
        }

        private TerminalModel terminalModel;

        public TerminalModel TerminalModel
        {
            get { return terminalModel; }
            set { terminalModel = value; this.DoNotify(); }
        }

        /// <summary>
        /// 终端信息 配电室绑定的终端信息
        /// </summary>
        private List<TerminalModel> terminals;

        public List<TerminalModel> Terminals
        {
            get { return terminals; }
            set { terminals = value; this.DoNotify(); }
        }



        #endregion

        #region 数据库仓储定义

        readonly IBaseRepository<Monitor, Guid> monitorRepositiry;
        readonly IBaseRepository<Diagram, Guid> diagramrRepositiry;
        readonly IBaseRepository<TemplateStyle, Guid> styleRepository;
        readonly IBaseRepository<DiagramConfig, Guid> diagramConfigRepository;
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        readonly IBaseRepository<Device,Guid> deviceRepository;
        readonly IBaseRepository<Protocol,Guid> protocolRepository; // 设备协议
        readonly IBaseRepository<Terminal,Guid> termianlRepository; // 设备协议
        readonly IBaseRepository<TerminalRltSensor,Guid> termianlRltRepository; // 关联的传感器
        #endregion

        #region 命令定义
        public ICommand OpenLeftDrawCommand { get { return new CommandBase(OpenLeftDrawAction); } }
        public ICommand SaveMonitorCommand { get { return new CommandBase(SaveMonitorAction); } }
        public ICommand SaveConfigCommand { get { return new CommandBase(SaveConfigAction); } }
        public ICommand SavePointCommand { get { return new CommandBase(SavePointAction); } }
        public ICommand CloseDrawCommand { get { return new CommandBase(CloseDrawAction); } }
        public ICommand GetConfigCommand { get { return new CommandBase(GetConfigAction); } }
        public ICommand OpenRightDrawCommand { get { return new CommandBase(OpenRightDrawAction); } }
        public ICommand SaveDeviceCommand { get { return new CommandBase(SaveDeviceAction); } }

        #endregion

        public MonitorViewModel()
        {
            this.ConfigModel = new ConfigModel();
            this.MonitorModels = new List<MonitorModel>();
            this.Terminals = new List<TerminalModel>();
            this.SensorModels = new List<SensorModel>();
            this.Protocols = new List<string>();
            this.DeviceTypes = new List<string>();
            this.MonitorModels.Add(new MonitorModel() { Name = "请添顶级监测点" });

            this.messageClientProvider = EngineContext.Current.Resolve<IMessageClientProvider>();

            monitorRepositiry = ESTRepository.Builder<Monitor, Guid>();
            diagramrRepositiry = ESTRepository.Builder<Diagram, Guid>();
            styleRepository = ESTRepository.Builder<TemplateStyle, Guid>();
            diagramConfigRepository = ESTRepository.Builder<DiagramConfig, Guid>();
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();
            deviceRepository = ESTRepository.Builder<Device, Guid>();
            protocolRepository = ESTRepository.Builder<Protocol, Guid>();
            termianlRepository = ESTRepository.Builder<Terminal, Guid>();
            termianlRltRepository = ESTRepository.Builder<TerminalRltSensor, Guid>();
            MonitorTypes = new List<ComboxItem>();
            this.Refresh();
            // 监测点类型
            var dic2 = typeof(StationType).GetDescriptionAndValue();
            foreach (var item in dic2)
            {
                MonitorTypes.Add(new ComboxItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }

            // 绑定设备类型  客户端还是服务端
            DeviceTypes.Add("server");
            DeviceTypes.Add("client");

            // 绑定传感器数据

            //绑定协议类型
            this.Protocols = protocolRepository.Where(a => true).ToList().Select(a => a.Name).ToList();
            
        }

        #region 监测点配置相关
        private void Refresh()
        {
            Task.Run(() =>
            {
                var monitors =
                monitorRepositiry.Where(a => true)
                .ToTreeList();
                if (monitors.Any())
                {
                    this.MonitorModels = ObjectMapper.Map<List<MonitorModel>>(monitors).ToList();
                }
            });
        }
        // 是否添加子节点
        private bool addChild { get; set; }
        public bool AddChild { get => addChild; set { addChild = value; this.DoNotify(); } }
        private bool IsEdit { get; set; } // 是否为编辑
        // 选中被操作的监测点id
        public Guid ActiveMonitorId { get; set; }
        // 打开左侧抽屉
        public void OpenLeftDrawAction(object data)
        {
            IsEdit = false;
            this.LeftShow = true;
            // 先添加顶级监测点
            if (data.ToString() == "top")
            {
                this.MonitorModel = new MonitorModel();
                AddChild = false;
            }
            if (data.ToString() == "addchild")
            {
                // 添加子点
                AddChild = true;
                this.MonitorModel = new MonitorModel();
            }
            else if (data.ToString() == "edit")
            {
                // 编辑节点
                var monitor = monitorRepositiry.Get(ActiveMonitorId);
                this.MonitorModel = ObjectMapper.Map<MonitorModel>(monitor);
                this.IsEdit = true;
            }
        }
        // 打开配置温度模板的抽屉
        public void OpenConfigDrawAction(bool open)
        {
            this.ConfigShow = open;
            // 查询一次当前的配置
            GetTemplateStyle();
        }
        // 获取当前监测点的温度模板
        private void GetTemplateStyle()
        {
            var monitorId = this.ConfigModel.CurrentId;
            var temp = styleRepository.Where(a => a.MonitorId == monitorId).First();
            if (temp != null)
            {
                this.TemplateModel = ObjectMapper.Map<TemplateModel>(temp);
            }
            else
            {
                this.TemplateModel = null;
            }
        }
        // 保存温度模板的配置
        public void SaveConfigAction(object data)
        {

            // 判断配置是否存在
            var temp = styleRepository.Where(a => a.MonitorId == this.ConfigModel.CurrentId).First();
            if (temp != null)
            {
                temp = ObjectMapper.Map<TemplateStyle>(this.TemplateModel);
                styleRepository.Update(temp);
            }
            else
            {
                temp = ObjectMapper.Map<TemplateStyle>(this.TemplateModel);
                temp.MonitorId = this.ConfigModel.CurrentId;
                styleRepository.Insert(temp);
            }
            this.ConfigShow = false;
            Growl.Info("保存成功");
        }
        // 保存监测点数据
        public void SaveMonitorAction(object obj)
        {
            if (AddChild)
            {
                // 添加 子项
                if (this.MonitorModel.DeviceId == Guid.Empty)
                {
                    Growl.Error("请给站点绑定服务设备");
                    return;
                }
                var monitor = ObjectMapper.Map<Monitor>(this.MonitorModel);
                monitor.ParentId = this.ActiveMonitorId;
                monitorRepositiry.Insert(monitor);
            }
            else  // 编辑状态
            if (IsEdit)
            {
                var monitor = ObjectMapper.Map<Monitor>(this.MonitorModel);
                monitorRepositiry.Update(monitor);
            }
            else
            {
                // 添加顶级
                var monitor = ObjectMapper.Map<Monitor>(this.MonitorModel);
                monitorRepositiry.Insert(monitor);
            }

            this.Refresh();
            CloseDrawAction("left");
        }
        // 删除监测点
        public void DeleteMonitorAction(Guid id)
        {
            // 如果有子级，则不能删除
            if (monitorRepositiry.Where(a => a.ParentId == id).Any())
            {
                Growl.Error("该站点下有配电室，无法删除");
                return;
            }
            monitorRepositiry.Delete(id);


            this.Refresh();
            Growl.Success("删除成功");
        }

        // 监测点选中事件
        public void TreeSelected(MonitorModel model)
        {
            this.BottomShow = false;
            this.RightShow = false;
            var typeName = model.Type.GetDisplay();// 获取枚举值对应的字符串表示
            this.ConfigModel.CurrentId = model.Id;
            this.ConfigModel.CurrentMonitor = $"监测点:{model.Name}({typeName})";// 显示的字符串
            this.ConfigModel.StationType = model.Type;
            this.ConfigModel.IsEdit = false;
            this.ConfigModel.CanUpload = model.Type == StationType.Region;// 只有配电室区域才能操作

            if (model.Type == StationType.Region)
            {
                this.RegionId = model.Id;
                // 查询并读取上传的图片资源,绑定图片的名称
                ReadImgdata();
                // 读取当前监测点的温度模板
                GetTemplateStyle();
                // 读取当前监测点的温度点数据
                RefreshDiagramConfigs();

                // 读取传感器
                GetSensorByMonitor(model.Id);

                // 读取采集终端
                GetTerminalByMonitor(model.Id);

                // 如果这是没有选中站点，直接选中了配电室
                if(this.MonitorId==Guid.Empty|| this.DeviceModel == null)
                {
                    var monitor = monitorRepositiry.Find(this.RegionId);
                    this.MonitorId = monitor.ParentId.Value;
                    this.DeviceModel = ObjectMapper.Map<DeviceModel>(deviceRepository.Where(a => a.MonitorId == MonitorId).First());
                }
            }
            else
            {
                this.RegionId = Guid.Empty;
                this.MonitorId = model.Id;
                this.DeviceModel = new DeviceModel();
                // 选中设备
                var device = deviceRepository.Where(a => a.MonitorId == this.MonitorId).First();
                if (device != null)
                {
                    this.DeviceModel = ObjectMapper.Map<DeviceModel>(device);
                }

                // 选中站点时清空传感器和采集器
                this.SensorModels?.Clear();
                this.Terminals?.Clear();
            }
        }
        #endregion

        #region 设备管理相关
        // 保存或更新监测点关联的设备信息
        public void SaveDeviceAction(object data)
        {
            if(this.MonitorId == Guid.Empty)
            {
                Growl.Error("请先选择站点信息");
                return;
            }
            var device = (DeviceModel)data;
            if (device.Name.IsNullOrEmpty())
            {
                Growl.Error("设备设备名称不能为空");
                return;
            }
            if (device.IpAddress.IsNullOrEmpty())
            {
                Growl.Error("服务IP地址不能为空");
                return;
            }

            if (device.Port == 0)
            {
                Growl.Error("服务端口不能为空");
                return;
            }

            // 判断当前监测点是否已经有了设备信息
            var model = deviceRepository.Find(device.Id);
            if (model != null)
            {
                model = ObjectMapper.Map<Device>(device);
                model.MonitorId=this.MonitorId;
                deviceRepository.Update(model);
            }
            else
            {
                model = ObjectMapper.Map<Device>(device);
                model.MonitorId = this.MonitorId;
                deviceRepository.Insert(model);
            }

            Growl.Success("数据保存成功");
        }
       
        #endregion

        #region 配电图管理
        // 读取配电图
        private void ReadImgdata()
        {
            var id = this.ConfigModel.CurrentId;// 监测点id
            var diagram = diagramrRepositiry.Where(a => a.MonitorId == id).First();
            if (diagram != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"file");
                if (!Directory.Exists(filePath))
                {
                    // 不存在就创建
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, $"{diagram.Id}.svg");
                if (File.Exists(filePath))
                {
                    // 图片存在
                    this.ReloadImage?.Invoke(filePath, new EventArgs());
                }
                else
                {
                    // 图片不存在，将数据库中的文件写入到本地
                    var data = diagram.Data;
                    File.WriteAllBytes(filePath, data);
                    this.ReloadImage?.Invoke(this, new EventArgs());
                }
                this.ConfigModel.SelectedFilePath = filePath;
                this.ConfigModel.PicName = diagram.Name;

                this.DiagramModel = ObjectMapper.Map<DiagramModel>(diagram);
            }
        }


        // 打开右侧
        public void OpenRightDrawAction(object data)
        {
            this.RightShow = bool.Parse(data.ToString());
        }

        // 打开底部监测点关联的传感器信息
        public void OpenBottomDrawAction(object data)
        {
            this.BottomShow = true;
        }
        // 查看 监测点的配置
        public void GetConfigAction(object obj)
        {
            this.BottomShow = true;
            var id = this.ConfigModel.CurrentId;
        }
        // 新增图纸，保存数据
        public void SavePicData(string path)
        {
            Task.Run(() => {
                if (File.Exists(path))
                {
                    var data = File.ReadAllBytes(path);
                    // 先查询监测点是否已经绑定图片
                    var diagram = diagramrRepositiry.Where(a => a.MonitorId == this.ConfigModel.CurrentId).First();

                    if (diagram != null)
                    {
                        // 已经存在，只需要更新
                        diagram.Data = data;
                        diagram.Name = this.ConfigModel.PicName;
                        diagramrRepositiry.Update(diagram);
                    }
                    else
                    {
                        diagram = new Diagram();
                        diagram.Data = data;
                        diagram.Name = this.ConfigModel.PicName;
                        diagram.MonitorId = this.ConfigModel.CurrentId;
                        diagramrRepositiry.Insert(diagram);
                        this.DiagramModel = ObjectMapper.Map<DiagramModel>(diagram);
                    }
                    // 同时将数据写到当前目录file 文件夹中
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "file");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    filePath = Path.Combine(filePath, $"{diagram.Id}.svg");
                    File.WriteAllBytes(filePath, data);
                    Growl.Info("操作成功");
                }
            });
        }

        // 点击新增温度点
        public void NewPoint(string name, Point point)
        {
            // 查询当前配电监测点有无图纸配置
            if (!diagramrRepositiry.Where(a => a.MonitorId == this.ConfigModel.CurrentId).Any())
            {
                HandyControl.Controls.MessageBox.Show("请先添加图纸", "温馨提示");
                return;
            }
            if (!this.RightShow)
                this.RightShow = true;
            this.DiagramConfigModel = new DiagramConfigModel(name, point);

        }
        // 点击保存 温度点
        public void SavePointAction()
        {
            if (this.DiagramConfigModel.PointName.IsNullOrEmpty())
            {
                HandyControl.Controls.MessageBox.Show("请配置温度点名称", "温馨提示");
                return;
            }

            this.DiagramConfigModel.IsSave = true;

            var model = diagramConfigRepository
                .Where(a => a.Id == this.DiagramConfigModel.Id)
                .First();

            if (model != null)
            {
                // 存在数据，进行更新
                model = ObjectMapper.Map<DiagramConfig>(this.DiagramConfigModel);
                diagramConfigRepository.Update(model);
            }
            else
            {
                // 不存在数据，进行添加
                model = ObjectMapper.Map<DiagramConfig>(this.DiagramConfigModel);
                model.DiagramId = this.DiagramModel.Id;
                diagramConfigRepository.Insert(model);
            }
            this.RightShow = false;
            Growl.Info("保存成功");
            RefreshDiagramConfigs();
        }
        // 移动温度点，更新坐标位置
        public void UpdatePoint(string name, Point point)
        {
            var model = diagramConfigRepository.Where(a => a.PropName == name).First();
            if (model != null)
            {
                model.PointX = point.X; ;
                model.PointY = point.Y;
                diagramConfigRepository.Update(model);

                this.DiagramConfigModel = ObjectMapper.Map<DiagramConfigModel>(model);
            }
            else
            {
                this.diagramConfigModel.PropName = name;
                this.DiagramConfigModel.PointX = point.X;
                this.DiagramConfigModel.PointY = point.Y;
            }
        }
        /// <summary>
        /// 修改温度数据  绑定温度
        /// </summary>
        /// <param name="model"></param>
        public void UpdatePointConfig(DiagramConfigModel model)
        {
            var entity = ObjectMapper.Map<DiagramConfig>(model);
            diagramConfigRepository.Update(entity);
            RefreshDiagramConfigs();
            Growl.Info("保存成功");
        }
        /// <summary>
        /// 删除温度点数据
        /// </summary>
        /// <param name="name"></param>
        public void DeletePoint(string name)
        {
            diagramConfigRepository.Delete(a => a.PropName == name);
            RefreshDiagramConfigs();
            Growl.Info("删除成功");
        }
        /// <summary>
        /// 刷新指定温度点的温度配置数据
        /// </summary>
        public void RefreshDiagramConfigs()
        {
            var list = diagramrRepositiry
                .Orm
                .Select<Diagram, DiagramConfig>()
                .Where((d, c) => d.MonitorId == this.ConfigModel.CurrentId && c.DiagramId == d.Id)
                .ToList<DiagramConfig>();
            this.DiagramConfigModels = ObjectMapper.Map<List<DiagramConfigModel>>(list).CreateIndex();
            if (!this.RightShow)
            {
                this.InitPoint?.Invoke(this, DiagramConfigModels);
            }
        }

        // 选中传感器，设定传感器信息
        public void SetSensorCode(Guid id)
        {
            var sensor = sensorRepository.Where(a => a.Id == id).First();
            if (sensor != null)
            {
                this.DiagramConfigModel.SensorCode = sensor.SensorCode;
            }
        }
        #endregion

        #region 传感器管理相关
        // 根据配电室查询关联的传感器
        public void GetSensorByMonitor(Guid id)
        {
            var sensors=sensorRepository.Where(a=>a.MonitorId==id).ToList();
            this.SensorModels=ObjectMapper.Map<List<SensorModel>>(sensors).CreateIndex();
        }

        // 添加传感器
        public void CreateSensor(bool edit = false,Guid id=default)
        {
            if (this.RegionId == Guid.Empty)
            {
                Growl.Error("请先选择配电室");
                return;
            }
            this.SensorShow = true;
            if (edit)
            {
                var sensor=this.sensorRepository.Find(id);
                this.SensorModel=ObjectMapper.Map<SensorModel>(sensor); 
            }
            else
            {
                this.SensorModel=new SensorModel();
            }
        }
        // 保存传感器
        public void SaveSensor()
        {
            if (this.sensorModel.SensorCode.IsNullOrEmpty())
            {
                Growl.Error("传感器编码不能为空");
            }
            if (sensorModel.SensorCode.Length > 10)
            {
                Growl.Error("传感器编码不能超过10位");
                return;
            }
            var id = this.SensorModel.Id;
            var sensor = this.sensorRepository.Find(id);
            if(sensor != null)
            {
                // 更新
                sensor.SensorCode = this.SensorModel.SensorCode;    
                sensor.Position = this.SensorModel.Position;
                this.sensorRepository.Update(sensor);   
            }
            else
            {
                sensor = ObjectMapper.Map<Sensor>(this.SensorModel);
                sensor.MonitorId = this.RegionId;
                sensorRepository.Insert(sensor);
            }

            Growl.Success("保存成功");
            this.SensorShow = false;
            this.GetSensorByMonitor(this.RegionId);
        }

        // 删除传感器
        public void DeleteSensor(Guid id)
        {
            sensorRepository.Delete(id);
            Growl.Info("操作成功");
            this.GetSensorByMonitor(this.RegionId);
        }

        #endregion

        #region 终端采集器相关
        
        // 获取配电室绑定的采集器信息
        public void GetTerminalByMonitor(Guid id)
        {
            var terminal = termianlRepository.Where(a => a.MonitorId == id).ToList();
            this.Terminals=ObjectMapper.Map<List<TerminalModel>>(terminal).CreateIndex();
        }
        // 添加采集器
        public void CreateTerminal(bool edit = false, Guid id = default)
        {
            if (this.RegionId == Guid.Empty)
            {
                Growl.Error("请先选择配电室");
                return;
            }
            this.TerminalShow = true;
            if (edit)
            {
                var sensor = this.termianlRepository.Find(id);
                this.TerminalModel = ObjectMapper.Map<TerminalModel>(sensor);
            }
            else
            {
                this.TerminalModel = new TerminalModel();
            }
        }
        // 保存采集器
        public void SaveTerminal()
        {
            if (this.TerminalModel.Name.IsNullOrEmpty())
            {
                Growl.Error("采集器名称不能为空"); return;
            }

            if (this.TerminalModel.Addr.IsNullOrEmpty())
            {
                Growl.Error("采集器地址位不能为空"); return;
            }

            if (!int.TryParse(this.TerminalModel.Addr,out var ad))
            {
                Growl.Error("地址位必须位Int 类型");
                return;
            }

            // 查询当前配电室的采集器地址有无重复

            var terminal = termianlRepository.Where(a => a.Addr == this.TerminalModel.Addr && a.MonitorId == this.RegionId).First();

            if(terminal!= null)
            {
                Growl.Error($"采集器地址{this.TerminalModel.Addr}已经存在");
                return;
            }
            var id = this.TerminalModel.Id;
            terminal = this.termianlRepository.Find(id);
            if (terminal != null)
            {
                // 更新
                terminal = ObjectMapper.Map<Terminal>(this.TerminalModel);
                terminal.MonitorId = this.RegionId;
                this.termianlRepository.Update(terminal);
            }
            else
            {
                terminal = ObjectMapper.Map<Terminal>(this.TerminalModel);
                terminal.MonitorId = this.RegionId;
                termianlRepository.Insert(terminal);
            }

            Growl.Success("保存成功");
            this.TerminalShow = false;
            this.GetTerminalByMonitor(this.RegionId);
        }

        // 删除采集器
        public void DeleteTerminal(Guid id)
        {
            termianlRepository.Delete(id);
            Growl.Info("操作成功");
            this.GetTerminalByMonitor(this.RegionId);
        }
        #endregion

        #region 采集器关联传感器相关
        public void GetTerminalRlt(Guid id)
        {
            this.SelectedTerminalId = id;
            this.TerminalRltShow = true;
            var sensors = this.termianlRltRepository
                .Orm.Select<TerminalRltSensor, Sensor>()
                .Where((t, s) => t.TerminalId == id && t.SensorId == s.Id)
                .ToList((t, s) => s);
            this.SensorList = ObjectMapper.Map<List<SensorModel>>(sensors).CreateIndex();
        }

        public void DeleteTerminalRlt(Guid id)
        {
            termianlRltRepository.Delete(a => a.SensorId==id);
            Growl.Info("删除成功");
        }
        // 保存关联的传感器

        public void SaveRltSensor(List<Guid> sensorIds)
        {
            if (sensorIds.Any())
            {
                sensorIds.ForEach(a =>
                {
                    var model = new TerminalRltSensor()
                    {
                        SensorId = a,
                        TerminalId = this.SelectedTerminalId,
                    };
                    // 判断是否已经存在
                    if (!termianlRltRepository.Where(b => b.SensorId == a && b.TerminalId == this.SelectedTerminalId).Any())
                    {
                        termianlRltRepository.Insert(model);
                    }
                });
                Growl.Info("关联成功");
                GetTerminalRlt(this.SelectedTerminalId);
            }
            //return true;
        }

        #endregion

        // 关闭抽屉
        public void CloseDrawAction(object data)
        {
            if(data.ToString() == "left")
            {
                this.LeftShow = false;
            }else if(data.ToString() == "right")
            {
                this.RightShow = false;
            }else if(data.ToString() == "config")
            {
                this.ConfigShow=false;
            }
            else if (data.ToString() == "sensor")
            {
                this.SensorShow = false; 
            }
            else if (data.ToString() == "terminal")
            {
                this.TerminalShow = false;
            }
            else if (data.ToString() == "terminalRlt")
            {
                this.TerminalRltShow = false;
            }
            else
            {
                this.BottomShow = false;
            }
        }


        #region 缓存管理

        // 更新设备的缓存
        public void UpdateDeviceCache()
        {
            CacheFactory.DeleteCache();

            this.UpdatePtotocolCahce();
            var deviceInfo = new CacheItemDeviceInfo();
            if(this.DeviceModel!=null)
            {
                deviceInfo.AlertValue = this.DeviceModel.AlertValue;
                deviceInfo.WarnValue = this.DeviceModel.WarnValue;
                deviceInfo.TolerantValue = this.DeviceModel.TolerantValue;
                CacheFactory.AddOrUpdateDeviceInfoCache(this.DeviceModel.Id, deviceInfo);
            }

            UpdateDeviceTerminal();
            UpdateSensorInfo();
            Growl.Success("数据同步成功");
        }
        // 更新所有的协议的缓存信息
        private void UpdatePtotocolCahce()
        {
            Task.Run(() =>
            {
                var devices = deviceRepository.Where(a => true)
                   .ToList()
                   .GroupBy(a => a.Ptotocol)
                   .Select(a => new
                   {
                       Protocol = a.Key,
                       Devices = a.ToList()
                   });

                devices?.ForEach(a =>
                {
                    var items = a.Devices
                    .Select(b => new CacheItemDevice()
                    {
                        Protocol = a.Protocol,
                        DeviceId = b.Id,
                        MonitorId = b.MonitorId,
                        IpAddress = b.IpAddress,
                        Port = b.Port,
                        Type=b.Type
                    }).ToList();
                    // 更新缓存
                    CacheFactory.AddOrUpdateDeviceCache(a.Protocol, items);

                });
            });
        }

        // 更新当前设备下的所有采集器信息
        private void UpdateDeviceTerminal()
        {
            Task.Run(() =>
            {
                if (this.DeviceModel != null)
                {
                    // 查询当前设备的站点id，根据站点查采集器
                    var monitorId = this.DeviceModel.MonitorId;
                    // 获取站点下的所有配电室
                    var regions = monitorRepositiry.Where(a => a.ParentId == monitorId).ToList(a => a.Id);
                    if (regions == null) return;
                    var terminals = termianlRepository.Where(a => regions.Contains(a.MonitorId)).ToList();

                    var deviceTerminalCache = new List<CacheItemTerminal>();

                    // 需要计算每个采集中关联了多少个传感器
                    terminals?.ForEach(a =>
                    {
                        var tc = new CacheItemTerminal();
                        tc.Addr = short.Parse(a.Addr);
                        tc.DeviceId = this.DeviceModel.Id;
                        tc.Id = a.Id;
                        // 查找关联的传感器
                        var sensor = termianlRltRepository.Orm.Select<TerminalRltSensor, Sensor>()
                        .Where((t, s) => t.TerminalId == a.Id && t.SensorId == s.Id).ToList<Sensor>();
                        tc.SensorCount = sensor.Count();
                        deviceTerminalCache.Add(tc);

                        // 添加每一个采集的缓存信息
                        CacheFactory.AddOrUpdateTerminalInfoCache(a.Id, tc);

                        var sensorCaches = new List<CacheItemSensor>();
                        // 需要对每一个采集的传感器进行编号
                        var srlt = termianlRltRepository.Orm.Select<TerminalRltSensor, Sensor>()
                        .Where((t, s) => t.TerminalId == a.Id && t.SensorId == s.Id).ToList<TerminalRltSensor>();
                        if (srlt.Any())
                        {
                            var index = 0;
                            foreach (var item in srlt)
                            {
                                var s = new CacheItemSensor();
                                item.SensorNo = ++index;
                                termianlRltRepository.Update(item);
                                s.Id = item.SensorId;
                                s.SensorNo = index;
                                s.SensorCode = sensor?.FirstOrDefault(a => a.Id == item.Id).SensorCode;
                                s.Position = sensor?.FirstOrDefault(a => a.Id == item.Id).Position;
                                sensorCaches.Add(s);
                            }
                        }

                        //添加采集器对应传感器的缓存
                        CacheFactory.AddOrUpdateTerminalSensorCache(a.Id, sensorCaches);
                    });

                    // 添加设备 采集器缓存
                    CacheFactory.AddOrUpdateTerminalCache(this.DeviceModel.Id, deviceTerminalCache);

                }
            });
        }

        // 更新被关联了温度点的传感器缓存
        private void UpdateSensorInfo()
        {
            try
            {
                Task.Run(() => {
                    // 获取图纸配置中传感器编号
                    var sensorCodes = diagramConfigRepository.Where(a => a.SensorCode != "").ToList();
                    sensorCodes?.ForEach(a =>
                    {
                        var s = new CacheItemSensorInfo();
                        s.SensorCode = a.SensorCode;
                        s.PointName = a.PointName;
                        // 获取图纸信息
                        var diagram = diagramrRepositiry.Find(a.DiagramId);
                        // 获取配电室
                        if (diagram == null) return;
                        var region = monitorRepositiry.Find(diagram.MonitorId);
                        // 获取站点
                        if (region == null) return;
                        var station = monitorRepositiry.Find(region.ParentId.Value);
                        if (station == null) return;
                        s.StationName = station?.Name;
                        s.RegionName = region?.Name;
                        CacheFactory.AddOrUpdateSensorInfoCache(a.SensorCode, s);
                    });
                });
            }
            catch (Exception ex)
            {

            }
           
        }

        #endregion

        #region 远程写入数据相关
        public void WriteSensor()
        {
            // 远程写入之前更新一下缓存
            CacheFactory.DeleteCache("Terminal");
            CacheFactory.DeleteCache("Sensor");
            UpdateSensorInfo();
            UpdateDeviceTerminal();

            // 远程写入，需要写入固定的采集器
            var message = new RemoteControlMessage();
            if (this.SelectedTerminalId != Guid.Empty)
            {
                message.TerminalId = this.SelectedTerminalId;
                message.DeviceId = this.DeviceModel.Id;
                message.Ptotocol = this.DeviceModel.Ptotocol;
                message.ControlType = ControlType.Write;
                // 发送命令
                messageClientProvider?.SendMessage(message);
            }
        }
        #endregion
    }
}
