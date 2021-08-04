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

using ESTTool.Excel;

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
using System.Windows.Forms;
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

        // 自定义的样式
        private TemplateModel customTemplateModel;

        public TemplateModel CustomTemplateModel
        {
            get { return customTemplateModel; }
            set { customTemplateModel = value; this.DoNotify(); }
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

        // 树结构定义
        private List<TreeViewModel> treeViewModels;

        public List<TreeViewModel> TreeViewModels
        {
            get { return treeViewModels; }
            set { treeViewModels = value; this.DoNotify(); }
        }
        private TreeViewModel treeViewModel;

        public TreeViewModel TreeViewModel
        {
            get { return treeViewModel; }
            set { treeViewModel = value; this.DoNotify(); }
        }

        #endregion

        #region 数据库仓储定义

        readonly IBaseRepository<Domain.Entities.Monitor, Guid> monitorRepositiry;
        readonly IBaseRepository<Diagram, Guid> diagramrRepositiry;
        readonly IBaseRepository<TemplateStyle, Guid> styleRepository;
        readonly IBaseRepository<DiagramConfig, Guid> diagramConfigRepository;
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        readonly IBaseRepository<Device,Guid> deviceRepository;
        readonly IBaseRepository<Protocol,Guid> protocolRepository; // 设备协议
        readonly IBaseRepository<Terminal,Guid> termianlRepository; // 设备协议
        readonly IBaseRepository<TerminalRltSensor,Guid> termianlRltRepository; // 关联的传感器
        readonly IBaseRepository<Station,Guid> stationRepository; // 站点仓储
        readonly IBaseRepository<PowerRoom,Guid> powerRepository; // 站点仓储


        #endregion

        #region 命令定义
        public ICommand OpenLeftDrawCommand { get { return new CommandBase(OpenLeftDrawAction); } }
        public ICommand SaveConfigCommand { get { return new CommandBase(SaveConfigAction); } }
        public ICommand SavePointCommand { get { return new CommandBase(SavePointAction); } }
        public ICommand CloseDrawCommand { get { return new CommandBase(CloseDrawAction); } }
        public ICommand GetConfigCommand { get { return new CommandBase(GetConfigAction); } }
        public ICommand OpenRightDrawCommand { get { return new CommandBase(OpenRightDrawAction); } }

        #endregion

        public MonitorViewModel()
        {
            // 树结构
            this.TreeViewModels = new List<TreeViewModel>();


            this.ConfigModel = new ConfigModel();
            this.MonitorModels = new List<MonitorModel>();
            this.Terminals = new List<TerminalModel>();
            this.SensorModels = new List<SensorModel>();
            this.Protocols = new List<string>();
            this.DeviceTypes = new List<string>();
            this.DeviceModels=new List<DeviceModel>();
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
            stationRepository = ESTRepository.Builder<Station, Guid>();
            powerRepository = ESTRepository.Builder<PowerRoom, Guid>();
            
            MonitorTypes = new List<ComboxItem>();
            this.BuildTree();
            // 监测点类型
            //GetStationType(0);

            // 绑定设备类型  客户端还是服务端
            DeviceTypes.Add("TcpServer");
            DeviceTypes.Add("ModbusTcp");
            DeviceTypes.Add("SerialPort");
            DeviceTypes.Add("Gprs");

            // 绑定传感器数据

            //绑定协议类型
            this.Protocols = protocolRepository.Where(a => true).ToList().Select(a => a.Name).ToList();
            if (!this.Protocols.Any())
            {
                // 数据为空，手动添加两种默认协议
                this.Protocols.Add("WTR31");
                this.Protocols.Add("WTR20A");
            }
        }

        #region 一、监测站点配置相关

        // 当前选中的树节点
        private TreeViewModel selectedNode;
        public TreeViewModel SelectedTreeNode { get => selectedNode; set{ selectedNode = value;this.DoNotify(); }}
        // 是否添加子节点
        private bool IsEdit { get; set; } // 是否为编辑
        // 选中被操作的监测点id
        public Guid ActiveMonitorId { get; set; }
        // 获取左侧树结构   站点==配电室== 终端
        private void BuildTree()
        {
            Task.Run(() =>
            {
                var stations = stationRepository.Select.IncludeMany(a => a.Rooms, b => b.IncludeMany(c => c.Terminals)).ToList();
                if (stations.Any())
                {
                    //var tree = new TreeViewModel();
                    var tree = new List<TreeViewModel>();
                    stations?.ForEach(a =>
                    {
                        var node1 = new TreeViewModel();
                        node1.NodeName = a.Name;
                        node1.NodeType = TreeNodeType.Station;
                        node1.Id = a.Id;
                        if (a.Rooms.Any())
                        {
                            node1.Children = new List<TreeViewModel>();
                            a.Rooms.ForEach(b =>
                            {
                                var node2 = new TreeViewModel();
                                node2.NodeName = b.Name;
                                node2.NodeType = TreeNodeType.Room;
                                node2.Id = b.Id;
                                node2.ParentId = a.Id;
                                if (b.Terminals.Any())
                                {
                                    node2.Children = new List<TreeViewModel>();
                                    b.Terminals?.ForEach(c =>
                                    {
                                        var node3 = new TreeViewModel();
                                        node3.ParentId = b.Id;
                                        node3.NodeName = c.Name;
                                        node3.NodeType = TreeNodeType.Termianl;
                                        node3.Id = c.Id;
                                        node2.Children.Add(node3);
                                    });
                                }
                                node1.Children.Add(node2);
                            });
                        }
                        tree.Add(node1);
                    });
                   this.TreeViewModels = tree;  
                }
            });
        }
        // 树节点选中事件
        public void TreeSelected(TreeViewModel node)
        {
            this.SelectedTreeNode = node;
            this.BottomShow = false;
            this.RightShow = false;
            // 根据节点类型展示不同的内容
            switch (SelectedTreeNode.NodeType)
            {
                case TreeNodeType.Station:
                    // 选中了站点或者厂区  此时展示设备配置界面，查询该厂区下的设备信息
                    this.GetDeviceList(node.Id);
                    break;
                case TreeNodeType.Room:
                    // 选中了配电室，展示一次图配置相关内容，同时展示采集器，可远程写入数据
                    // TODO
                    // 查询并读取上传的图片资源,绑定图片的名称
                    ReadImgdata();
                    // 读取当前监测点的温度模板
                    GetTemplateStyle();
                    // 读取当前监测点的温度点数据
                    RefreshDiagramConfigs();
                    break;
                case TreeNodeType.Termianl:
                    // 选中了终端，显示传感器配置界面
                    this.GetSensorModels(node.Id);
                    break;
                default:
                    break;

            }
        }

        // 新增树节点
        public void CreateTreeNode(TreeViewModel node=null)
        {
            if (node == null)
            {
                // 新增厂区或者站点信息
                this.CreateStation();
            }else if (node.NodeType == TreeNodeType.Station)
            {
                // 新增配电室
                this.CreatePowerRoom(node.Id);
            }
            else if (node.NodeType == TreeNodeType.Room)
            {
                // 新增配电室
                this.CreateTerminal(node.Id);
            }
            else
            {
                Growl.Warning("无法在终端节点上添加内容");
                return;
            }
        }
        public void EditTreeNode(TreeViewModel node)
        {
            if (node.NodeType==TreeNodeType.Station)
            {
                //编辑 站点
                this.GetStation(node.Id);
            }else if (node.NodeType == TreeNodeType.Room)
            {
                this.GetPowerRoom(node.Id);
            }
            else if (node.NodeType == TreeNodeType.Termianl)
            {
                this.GetTerminal(node.Id);
            }
        }
        public void DeleteTreeNode(TreeViewModel node)
        {
            if (node.Id == Guid.Empty)
            {
                Growl.Error("操作失败");
                return;
            }
            if (node.NodeType == TreeNodeType.Station)
            {
                //编辑 站点
                this.DeleteStation(node.Id);
            }
            else if (node.NodeType == TreeNodeType.Room)
            {
                this.DeletePowerRoom(node.Id);
            }else if(node.NodeType == TreeNodeType.Termianl)
            {
                this.DeleteTerminal(node.Id);
            }
        }
        #region 1、新增厂区或者站点

        /// <summary>
        /// 厂区实体
        /// </summary>
        private StationModel stationModel;

        public StationModel StationModel
        {
            get { return stationModel; }
            set { stationModel = value; }
        }

        // 抽屉状态
        private bool stationDrawShow;

        public bool StationDrawShow
        {
            get { return stationDrawShow; }
            set { stationDrawShow = value; this.DoNotify(); }
        }
        public void GetStation(Guid id)
        {
            var station = stationRepository.Get(id);
            this.StationModel = ObjectMapper.Map<StationModel>(station);
            this.StationDrawShow = true;
        }
        //新增厂区
        private void CreateStation()
        {
            this.StationModel = new StationModel();
            this.StationDrawShow = true;
        }
        // 保存厂区
        public void SaveStation(bool edit=false)
        {
            if (StationModel!=null&& StationModel.Id!=Guid.Empty)
            {
                var station = ObjectMapper.Map<Station>(this.StationModel);
                stationRepository.Update(station);
            }
            else
            {
                var station = ObjectMapper.Map<Station>(this.StationModel);
                stationRepository.Insert(station);
            }
            this.StationDrawShow = false;
            Growl.Success("保存成功");
            this.BuildTree();
        }
        // 删除厂区
        public void DeleteStation(Guid id)
        {
            // 判断是否有配电室
            if (powerRepository.Where(a => a.StationId == id).Any())
            {
                Growl.Error("该站点包含配电室，无法直接删除");
                return;
            }

            stationRepository.Delete(id);
            this.BuildTree();
            Growl.Success("删除成功");
        }


        #endregion

        #region 2、新增配电室

        // 实体
        private PowerRoomModel powerRoom;

        public PowerRoomModel PowerRoom
        {
            get { return powerRoom; }
            set { powerRoom = value;this.DoNotify(); }
        }

        // 抽屉状态
        private bool powerRoomDrawShow;

        public bool PowerRoomDrawShow
        {
            get { return powerRoomDrawShow; }
            set { powerRoomDrawShow = value; this.DoNotify(); }
        }
        public void GetPowerRoom(Guid id)
        {
            var station = powerRepository.Get(id);
            this.PowerRoom = ObjectMapper.Map<PowerRoomModel>(station);
            this.PowerRoomDrawShow = true;
        }
        //新增配电室
        private void CreatePowerRoom(Guid stationId)
        {
            this.PowerRoom = new PowerRoomModel();
            this.PowerRoom.StationId= stationId;    
            this.PowerRoomDrawShow = true;
        }
        // 保存配电室
        public void SavePowerRoom(bool edit = false)
        {
            var id = Guid.Empty;
            if (PowerRoom != null && PowerRoom.Id != Guid.Empty)
            {
                var power = ObjectMapper.Map<PowerRoom>(this.PowerRoom);
                powerRepository.Update(power);
            }
            else
            {
                var power = ObjectMapper.Map<PowerRoom>(this.PowerRoom);
                id = powerRepository.Insert(power).Id;
            }
            this.PowerRoomDrawShow = false;
            Growl.Success("保存成功");
            var node = new TreeViewModel();
            node.ParentId = PowerRoom.StationId;
            node.Id = id;
            node.NodeName = PowerRoom.Name;
            this.BuildTree();
        }
        // 删除配电室
        public void DeletePowerRoom(Guid id)
        {
            if (termianlRepository.Where(a => a.PowerRoomId == id).Any())
            {
                Growl.Error("该站点包含终端信息，无法直接删除");
                return;
            }
            powerRepository.Delete(id);
            this.BuildTree();
            Growl.Success("删除成功");
        }
        #endregion

        #region 3、终端采集器相关

        // 获取配电室绑定的采集器信息
        public void GetTerminal(Guid id)
        {
            var terminal = termianlRepository.Get(id);
            this.TerminalModel = ObjectMapper.Map<TerminalModel>(terminal);
            // 根据采集器查找设备列表
            var devices = stationRepository.Select
                .IncludeMany(a => a.Rooms, b => b.Where(c => c.Id == terminal.PowerRoomId))
                .IncludeMany(a => a.Devices)
                .ToList().SelectMany(a => a.Devices).ToList();
                //termianlRepository.Select.IncludeMany(a => a.PowerRoom.Station.Devices).ToList(a=>a.Device);
            this.DeviceModels = ObjectMapper.Map<List<DeviceModel>>(devices).CreateIndex();
            this.TerminalShow = true;
        }
        // 添加采集器
        public void CreateTerminal(Guid powerId)
        {
            this.TerminalShow = true;
            this.TerminalModel = new TerminalModel();
            this.terminalModel.PowerRoomId = powerId;
            // 根据配电室查询设备列表
            var stations = stationRepository.Select.IncludeMany(a => a.Rooms, b => b.Where(c => c.Id == powerId))
               .IncludeMany(a => a.Devices).ToList().SelectMany(a => a.Devices).ToList();
            this.DeviceModels = ObjectMapper.Map<List<DeviceModel>>(stations).CreateIndex();
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

            if (!int.TryParse(this.TerminalModel.Addr, out var ad))
            {
                Growl.Error("地址位必须位Int 类型");
                return;
            }

            // 查询当前配电室的采集器地址有无重复

           
            if (this.TerminalModel.Id != Guid.Empty)
            {
                // 更新
                var terminal = ObjectMapper.Map<Terminal>(this.TerminalModel);
                this.termianlRepository.Update(terminal);
            }
            else
            {
                var terminal = termianlRepository.Where(a => a.Addr == this.TerminalModel.Addr && a.PowerRoomId == this.TerminalModel.PowerRoomId).First();

                if (terminal != null)
                {
                    Growl.Error($"采集器地址{this.TerminalModel.Addr}已经存在");
                    return;
                }
                terminal = ObjectMapper.Map<Terminal>(this.TerminalModel);
                termianlRepository.Insert(terminal);
            }
            Growl.Success("保存成功");
            this.TerminalShow = false;
            this.BuildTree();
        }

        // 删除采集器
        public void DeleteTerminal(Guid id)
        {
            termianlRepository.Delete(id);
            Growl.Info("操作成功");
            this.BuildTree();
        }
        #endregion

        // 打开左侧抽屉
        public void OpenLeftDrawAction(object data)
        {
            IsEdit = false;
            this.LeftShow = true;
            // 先添加顶级监测点
            if (data.ToString() == "top")
            {
                this.MonitorModel = new MonitorModel();
            }
            if (data.ToString() == "addchild")
            {
                // 添加子点
                // 过滤数据，此时只能显示配电室了
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
            var temp = styleRepository.Where(a => a.PowerRoomId == this.SelectedTreeNode.Id).First();
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
            var temp = styleRepository.Where(a => a.PowerRoomId == this.SelectedTreeNode.Id).First();
            if (temp != null)
            {
                temp = ObjectMapper.Map<TemplateStyle>(this.TemplateModel);
                styleRepository.Update(temp);
            }
            else
            {
                temp = ObjectMapper.Map<TemplateStyle>(this.TemplateModel);
                temp.PowerRoomId = this.SelectedTreeNode.Id;
                styleRepository.Insert(temp);
            }
            this.ConfigShow = false;
            Growl.Info("保存成功");
        }
       
        #endregion

        #region 二、设备管理相关

        private bool deviceDrawShow;

        public bool DeviceDrawShow
        {
            get { return deviceDrawShow; }
            set { deviceDrawShow = value; this.DoNotify(); }
        }
        private DeviceModel deviceModel;
        public DeviceModel DeviceModel
        {
            get { return deviceModel; }
            set { deviceModel = value; this.DoNotify(); }
        }
        private List<DeviceModel> deviceModels;
        public List<DeviceModel> DeviceModels
        {
            get { return deviceModels; }
            set { deviceModels = value; this.DoNotify(); }
        }
        // 获取指定的设备
        public void GetDevice(Guid id)
        {
            var device = deviceRepository.Get(id);
            this.DeviceModel= ObjectMapper.Map<DeviceModel>(device);
            this.DeviceDrawShow = true;
        }
        // 查询设备列表 根据站点名称
        public void GetDeviceList(Guid id)
        {
            var devices = stationRepository.Select
                .Where(a=>a.Id==id)
                .IncludeMany(a => a.Devices)
                .ToList().SelectMany(a => a.Devices).ToList();
            this.DeviceModels = ObjectMapper.Map<List<DeviceModel>>(devices).CreateIndex();
        }
        // 添加一个设备
        public void CreateDevice()
        {
            this.DeviceDrawShow = true;
            this.DeviceModel = new DeviceModel();
            this.DeviceModel.StationId = this.SelectedTreeNode.Id;
        }
        // 保存设备信息
        public void SaveDevice()
        {
            if (DeviceModel.Name.IsNullOrEmpty())
            {
                Growl.Error("设备设备名称不能为空");
                return;
            }
            if (DeviceModel.IpAddress.IsNullOrEmpty())
            {
                Growl.Error("服务IP地址不能为空");
                return;
            }

            if (DeviceModel.Port == 0)
            {
                Growl.Error("服务端口不能为空");
                return;
            }
            if (this.DeviceModel.Id != Guid.Empty)
            {
                var device = ObjectMapper.Map<Device>(this.DeviceModel);
                deviceRepository.Update(device);
            }
            else
            {
                var device = ObjectMapper.Map<Device>(this.DeviceModel);
                deviceRepository.Insert(device);
            }
            this.DeviceDrawShow = false;
            this.GetDeviceList(this.SelectedTreeNode.Id);
            Growl.Success("保存成功");
        }
        // 删除设备信息
        public void DeleteDevice(Guid id)
        {
            deviceRepository.Delete(id);
            Growl.Success("删除成功");
            this.GetDeviceList(this.SelectedTreeNode.Id);
        }
        #endregion

        #region 三、配电图管理相关
        // 读取配电图
        private void ReadImgdata()
        {
            // 获取配电室id
            var powerId = this.SelectedTreeNode.Id;
            var diagram = diagramrRepositiry.Where(a => a.PowerRoomId == powerId).First();
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
                    Task.Run(() =>
                    {
                        var data = diagram.Data;
                        File.WriteAllBytes(filePath, data);
                        this.ReloadImage?.Invoke(this, new EventArgs());
                    });                 
                }
                this.ConfigModel.SelectedFilePath = filePath;
                this.ConfigModel.PicName = diagram.Name;
                this.DiagramModel = ObjectMapper.Map<DiagramModel>(diagram);
            }
            else
            {
                this.ReloadImage?.Invoke(false, new EventArgs());
            }
        }


        // 打开右侧
        public void OpenRightDrawAction(object data)
        {
            this.RightShow = bool.Parse(data.ToString());
        }
        // 查看 监测点的配置
        public void GetConfigAction(object obj)
        {
            this.BottomShow = true;
        }
        // 新增图纸，保存数据
        public void SavePicData(string path)
        {
            Task.Run(() => {
                if (File.Exists(path))
                {
                    var data = File.ReadAllBytes(path);
                    // 先查询监测点是否已经绑定图片
                    var diagram = diagramrRepositiry.Where(a => a.PowerRoomId == this.SelectedTreeNode.Id).First();

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
                        diagram.PowerRoomId = this.SelectedTreeNode.Id;
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
            if (!diagramrRepositiry.Where(a => a.PowerRoomId == this.SelectedTreeNode.Id).Any())
            {
                HandyControl.Controls.MessageBox.Show("请先添加或保存图纸", "温馨提示");
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
                this.DiagramConfigModel.PropName = name;
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
                .Select
                .Where(a => a.PowerRoomId == this.SelectedTreeNode.Id)
                .IncludeMany(a => a.Configs).ToList()
                .SelectMany(a => a.Configs).ToList();
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

        #region 四、传感器管理相关

        // 抽屉
        private bool sensorShow;
        public bool SensorShow
        {
            get { return sensorShow; }
            set { sensorShow = value; this.DoNotify(); }
        }
        // 传感器信息
        private SensorModel sensorModel;
        public SensorModel SensorModel
        {
            get { return sensorModel; }
            set { sensorModel = value; this.DoNotify(); }
        }
        //  采集绑定的温度传感器
        private List<SensorModel> sensorList;
        public List<SensorModel> SensorList
        {
            get { return sensorList; }
            set { sensorList = value; this.DoNotify(); }
        }

        // 获取指定的传感器
        public void GetSensor(Guid id)
        {
            var sensor = sensorRepository.Get(id);
            this.SensorModel = ObjectMapper.Map<SensorModel>(sensor);
            this.SensorShow = true;
        }
        // 获取采集器下的传感器列表
        public void GetSensorModels(Guid id)
        {
            var sensors = termianlRepository.Select.
                Where(a => a.Id == id)
                .IncludeMany(a => a.Sensors).ToList()
                .SelectMany(a => a.Sensors).ToList();
            this.SensorModels = ObjectMapper.Map<List<SensorModel>>(sensors).CreateIndex();
        }
        // 创建一个传感器
        public void CreateSensor()
        {
            this.SensorModel = new SensorModel();
            this.SensorModel.TerminalId = this.SelectedTreeNode.Id;
            this.SensorShow = true;
        }
        // 保存传感器信息
        public void SaveSensor()
        {
            if (this.sensorModel.SensorCode.IsNullOrEmpty())
            {
                Growl.Error("传感器编码不能为空");
                return;
            }
            if (sensorModel.SensorCode.Length > 10)
            {
                Growl.Error("传感器编码不能超过10位");
                return;
            }
            if (this.SensorModel.Id != Guid.Empty)
            {
                var sensor = ObjectMapper.Map<Sensor>(this.SensorModel);
                sensorRepository.Update(sensor);
            }
            else
            {
                var sensor = ObjectMapper.Map<Sensor>(this.SensorModel);
                sensorRepository.Insert(sensor);
            }
            Growl.Success("保存成功");
            this.SensorShow = false;
            this.GetSensorModels(this.SelectedTreeNode.Id);
        }
        // 删除传感器
        public void DeleteSensor(Guid id)
        {
            sensorRepository.Delete(id);
            Growl.Info("操作成功");
            this.GetSensorModels(this.SelectedTreeNode.Id);
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
            else if (data.ToString() == "StationDraw")
            {
                this.StationDrawShow = false;
            }
            else if (data.ToString() == "PowerRoom")
            {
                this.PowerRoomDrawShow = false;
            }
            else if (data.ToString() == "DeviceDraw")
            {
                this.DeviceDrawShow = false;
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

            // 更新选中站点下的设备信息

            var devices=stationRepository.Select
                .Where(a=>a.Id==this.SelectedTreeNode.Id)
                .IncludeMany(a=>a.Devices).ToList()
                .SelectMany(a=> a.Devices).ToList();
            if (devices.Any())
            {
                foreach (var item in devices)
                {
                    var deviceInfo = new CacheItemDeviceInfo();
                    deviceInfo.AlertValue = item.AlertValue;
                    deviceInfo.WarnValue = item.WarnValue;
                    deviceInfo.TolerantValue = item.TolerantValue;
                    CacheFactory.AddOrUpdateDeviceInfoCache(item.Id, deviceInfo);
                }
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
                try
                {
                    // 获取所有的站点中的采集器中关联的设备信息

                    var devices = termianlRepository
                    .Select
                    .Include(a => a.PowerRoom)
                    .Include(a => a.Device)
                    .Where(a => a.PowerRoom.StationId == this.SelectedTreeNode.Id)
                    .ToList(a => a.Device)
                    .Distinct().ToList();

                    if (devices != null && devices.Any())
                    {
                        // 对设备机型分组
                        var groups = devices.GroupBy(a => a.Ptotocol)
                        .Select(a => new
                        {
                            Protocol = a.Key,
                            Devices = a.ToList()
                        });
                        groups?.ForEach(a =>
                        {
                            var items = a.Devices
                           .Select(b => new CacheItemDevice()
                           {
                               Protocol = a.Protocol,
                               DeviceId = b.Id,
                           //  MonitorId = b.MonitorId,
                           IpAddress = b.IpAddress,
                               Port = b.Port,
                               Type = b.Type
                           }).ToList();
                            // 更新缓存
                            CacheFactory.AddOrUpdateDeviceCache(a.Protocol, items);
                        });
                    }
                }
                catch (Exception ex)
                {
                    Growl.Error("数据同步异常");
                }
               
            });
        }

        // 更新更新当前站点下所有采集器和传感器信息
        private void UpdateDeviceTerminal()
        {
            Task.Run(() =>
            {
                var devices = termianlRepository
                .Select
                .Include(a => a.PowerRoom)
                .Include(a => a.Device)
                .Where(a => a.PowerRoom.StationId == this.SelectedTreeNode.Id)
                .ToList(a => a.Device)
                .Distinct().ToList();

                if (devices.Any())
                {
                 
                    foreach (var dev in devices)
                    {
                        var deviceTerminalCache = new List<CacheItemTerminal>();
                        //获取采集器
                        var terminals = termianlRepository.Select.Include(a => a.Device)
                        .Where(a => a.DeviceId == dev.Id).IncludeMany(a=>a.Sensors).ToList();
                        if (terminals.Any())
                        {
                            foreach (var item in terminals)
                            {
                                var tc = new CacheItemTerminal();
                                tc.Addr = short.Parse(item.Addr);
                                tc.DeviceId = dev.Id;
                                tc.Id = item.Id;
                                tc.SensorCount = item.Sensors?.Count()??0;
                                deviceTerminalCache.Add(tc);
                                CacheFactory.AddOrUpdateTerminalInfoCache(item.Id, tc);

                                // 给采集器下的传感器添加缓存
                                if(item.Sensors?.Count() > 0)
                                {
                                    var index = 0;
                                    var sensorCaches = new List<CacheItemSensor>();
                                    foreach (var sensor in item.Sensors)
                                    {
                                        var s = new CacheItemSensor();
                                        sensor.No = ++index;
                                        sensorRepository.Update(sensor);
                                        s.Id = sensor.Id;
                                        s.SensorNo = index;
                                        s.SensorCode = sensor.SensorCode;
                                        s.Position = sensor.Position;
                                        sensorCaches.Add(s);
                                    }
                                    CacheFactory.AddOrUpdateTerminalSensorCache(item.Id, sensorCaches);
                                }
                            }
                        }
                        CacheFactory.AddOrUpdateTerminalCache(dev.Id, deviceTerminalCache);
                    }
                }
            });
        }

        // 更新被关联了温度点的传感器缓存
        private void UpdateSensorInfo()
        {
            try
            {
                Task.Run(() => {

                    var sensorCodes = diagramConfigRepository.Select
                    .Where(a => a.SensorCode != "")
                    .Include(a => a.Diagram)
                    .Include(a => a.Diagram.PowerRoom)
                    .Include(a => a.Diagram.PowerRoom.Station)
                    .Where(a => a.Diagram.PowerRoom.Station.Id==this.SelectedTreeNode.Id)
                    .ToList();
                    sensorCodes?.ForEach(a =>
                    {
                        var s = new CacheItemSensorInfo();
                        s.SensorCode = a.SensorCode;
                        s.PointName = a.PointName;
                        s.StationName = a.Diagram?.PowerRoom?.Station?.Name;
                        s.PowerRoom = a.Diagram?.PowerRoom?.Name;
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
            try
            {
              //  CacheFactory.DeleteCache("Terminal");
             //   CacheFactory.DeleteCache("Sensor");
                UpdateSensorInfo();
                UpdateDeviceTerminal();

                System.Threading.Thread.Sleep(200);
                // 远程写入，需要写入固定的采集器
                var message = new RemoteControlMessage();
                if (this.SelectedTreeNode.Id != Guid.Empty)
                {
                    message.TerminalId = this.SelectedTreeNode.Id;
                    // 获取这个采集器绑定的设备
                    var device = termianlRepository.Select
                        .Where(a => a.Id == this.SelectedTreeNode.Id)
                        .Include(a => a.Device)
                        .First(a => a.Device);
                    if(device != null)
                    {
                        message.DeviceId = device.Id;
                        message.Ptotocol = device.Ptotocol;
                        message.ControlType = ControlType.Write;
                        // 发送命令
                        messageClientProvider?.SendMessage(message);
                    }
                    else
                    {
                        Growl.Error("该采集器没有关联设备，无法远程写入");
                    }


                   
                }
            }
            catch (Exception ex)
            {
                Growl.Error("写入失败");
            }
        }
        #endregion

        #region 导入导出操作
        /// <summary>
        /// 下载传感器导入模板
        /// </summary>
        public void DownloadSensorTemp()
        {
            // 创建文件
            var workbook = ExcelExtension.CreateExcelFile(ExcelFileType.Xlsx);
            // 创建工作表
            var sheet = workbook.CreateSheet("传感器");
            // 定义表头
            var head = new string[] { "序号(整数)", "传感器编号", "安装位置" };
            // 创建第一行
            var row1 = sheet.CreateRow(0);
            // 合并单元格
            ExcelExtension.CellRangeAddress(0, 1, 0, 2);// 合并
            row1.CreateCell(0).SetCellValue("传感器导入模板，说明：前两行不要删除！！！,记得加边框");
            var row2 = sheet.CreateRow(1);
            for (int i = 0; i < head.Length; i++)
            {
                row2.CreateCell(i).SetCellValue(head[i]);
            }
            // 导出文件

            var stream = workbook.ConvertToBytes();
            var dialog = new SaveFileDialog();
            dialog.Filter = "(.xlsx)|*.xlsx";
            dialog.Title = "保存模板文件";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // 保存文件
                var filePath = dialog.FileName;
                FileStream fileStream = File.Create(filePath);
                fileStream.Write(stream);
                fileStream.Close();
                Growl.Success("下载成功");
            }
        }
        #endregion
    }
}
