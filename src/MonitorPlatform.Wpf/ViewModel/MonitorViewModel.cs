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
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

using Masuit.Tools;
using Masuit.Tools.Systems;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

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
        // 监测点内容
        private MonitorModel monitorModel;

        // 配置项实体
        private ConfigModel configModel;

        public ConfigModel ConfigModel
        {
            get { return configModel; }
            set { configModel = value; this.DoNotify(); }
        }


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


        // 温度传感器
        private List<string> sensorList;
        public List<string> SensorList
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


        // 监测点类型
        private List<ComboxItem> monitorTypes;

        public List<ComboxItem> MonitorTypes
        {
            get { return monitorTypes; }
            set { monitorTypes = value; this.DoNotify(); }
        }

        // 串口服务器
        private List<ComboxItem> deviceList;

        public List<ComboxItem> DeviceList
        {
            get { return deviceList; }
            set { deviceList = value; this.DoNotify(); }
        }

        readonly IBaseRepository<Monitor, Guid> monitorRepositiry;
        readonly IBaseRepository<Diagram, Guid> diagramrRepositiry;
        readonly IBaseRepository<TemplateStyle, Guid> styleRepository;
        readonly IBaseRepository<DiagramConfig, Guid> diagramConfigRepository;
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        readonly IBaseRepository<Device,Guid> deviceRepository;

        public ICommand OpenLeftDrawCommand { get { return new CommandBase(OpenLeftDrawAction); } }
        public ICommand SaveMonitorCommand { get { return new CommandBase(SaveMonitorAction); } }
        public ICommand SaveConfigCommand { get { return new CommandBase(SaveConfigAction); } }
        public ICommand SavePointCommand { get { return new CommandBase(SavePointAction); } }
        public ICommand CloseDrawCommand { get { return new CommandBase(CloseDrawAction); } }
        public ICommand GetConfigCommand { get { return new CommandBase(GetConfigAction); } }
        public ICommand OpenRightDrawCommand { get { return new CommandBase(OpenRightDrawAction); } }
        public MonitorViewModel()
        {
            this.ConfigModel = new ConfigModel();
            this.MonitorModels = new List<MonitorModel>();
            this.DeviceList = new List<ComboxItem>();
            this.MonitorModels.Add(new MonitorModel() { Name = "请添顶级监测点" });
            monitorRepositiry = ESTRepository.Builder<Monitor, Guid>();
            diagramrRepositiry = ESTRepository.Builder<Diagram, Guid>();
            styleRepository = ESTRepository.Builder<TemplateStyle, Guid>();
            diagramConfigRepository = ESTRepository.Builder<DiagramConfig, Guid>();
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();
            deviceRepository = ESTRepository.Builder<Device, Guid>();
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

            // 绑定串口服务器
            var deviceList = deviceRepository.Where(a => true).ToList();

            deviceList?.ForEach(a =>
            {
                var item = new ComboxItem();
                item.Key = a.Name;
                item.Value = a.Id;
                this.DeviceList.Add(item);
            });


            // 绑定传感器数据
           this.SensorList = sensorRepository.Where(a => true)
                .ToList()
                .Select(a => a.SensorCode).ToList();
        }

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
            IsEdit=false;   
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
            else
            {
                this.BottomShow = false;
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
            this.ConfigShow=false;
            Growl.Info("保存成功");
        }
        // 保存监测点数据
        public void SaveMonitorAction(object obj)
        {
            if (AddChild)
            {
                // 添加 子项
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
                if (this.MonitorModel.DeviceId == Guid.Empty)
                {
                    Growl.Error("请给站点绑定服务设备");
                    return;
                }
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
            monitorRepositiry.Delete(id);
            var item= MonitorModels.FirstOrDefault(a=>a.Id == id);
            this.MonitorModels.Remove(item);
            //this.Refresh();
        }

        // 监测点选中事件
        public void TreeSelected(MonitorModel model)
        {
            this.BottomShow = false;
            this.RightShow = false;
            var typeName = model.Type.GetDisplay();// 获取枚举值对应的字符串表示
            this.ConfigModel.CurrentId=model.Id;
            this.ConfigModel.CurrentMonitor = $"监测点:{model.Name}({typeName})";// 显示的字符串
            this.ConfigModel.StationType = model.Type;
            this.ConfigModel.IsEdit = false;
            this.ConfigModel.CanUpload = model.Type == StationType.Region;// 只有配电室区域才能操作

            if(model.Type == StationType.Region)
            {
                // 查询并读取上传的图片资源,绑定图片的名称
                ReadImgdata();
                // 读取当前监测点的温度模板
                GetTemplateStyle();
                // 读取当前监测点的温度点数据
                RefreshDiagramConfigs();
            }
        }

        private void ReadImgdata()
        {
            var id = this.ConfigModel.CurrentId;// 监测点id
            var diagram = diagramrRepositiry.Where(a => a.MonitorId == id).First();
            if(diagram != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"file");
                if (!Directory.Exists(filePath))
                {
                    // 不存在就创建
                    Directory.CreateDirectory(filePath);
                }
                filePath = Path.Combine(filePath, $"\\{diagram.Id}.svg");
                if (File.Exists(filePath))
                {
                    // 图片存在
                    this.ReloadImage?.Invoke(filePath, new EventArgs());
                }
                else
                {
                    // 图片不存在，将数据库中的文件写入到本地
                    var data=diagram.Data;
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
        // 打开底部
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

        // 新增监测点 
        public void CreateMonitorAction(object data)
        {

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
        public void NewPoint(string name,Point point)
        {
            // 查询当前配电监测点有无图纸配置
            if (!diagramrRepositiry.Where(a => a.MonitorId == this.ConfigModel.CurrentId).Any())
            {
                HandyControl.Controls.MessageBox.Show("请先添加图纸", "温馨提示");
                return;
            }
            if (!this.RightShow)
                this.RightShow = true;
            this.DiagramConfigModel = new DiagramConfigModel(name,point);

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
            Growl.Info("保存成功");
            RefreshDiagramConfigs();
        }
        // 移动温度点，更新坐标位置
        public void UpdatePoint(string name, Point point)
        {
            var model = diagramConfigRepository.Where(a => a.PropName == name).First();
            if(model != null)
            {
                model.PointX=point.X; ;
                model.PointY = point.Y;
                diagramConfigRepository.Update(model);

                this.DiagramConfigModel=ObjectMapper.Map<DiagramConfigModel>(model);
            }
            else
            {
                this.diagramConfigModel.PropName = name;
                this.DiagramConfigModel.PointX=point.X;
                this.DiagramConfigModel.PointY = point.Y;
            }
        }
        /// <summary>
        /// 修改温度数据  绑定温度
        /// </summary>
        /// <param name="model"></param>
        public void UpdatePointConfig(DiagramConfigModel model)
        {
            var entity=ObjectMapper.Map<DiagramConfig>(model);
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
            diagramConfigRepository.Delete(a=>a.PropName == name);
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
    }
}
