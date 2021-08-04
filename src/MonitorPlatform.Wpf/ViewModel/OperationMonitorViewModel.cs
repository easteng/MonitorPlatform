/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： OperationMonitorViewModel
*******类 说 明： 运行监测视图实体
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/18/2021 11:46:46 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

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
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class OperationMonitorViewModel : NotifyBase
    {
        public event EventHandler<EventArgs> ReloadImage;
        public event EventHandler<List<DiagramConfigModel>> InitPoint;
        /// <summary>
        /// 监测点
        /// </summary>
        private MonitorModel monitorModel;
        public MonitorModel MonitorModel
        {
            get { return monitorModel; }
            set { monitorModel = value; this.DoNotify(); }
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
        // 监测点树 结构数据
        private List<MonitorModel> monitorModels;
        public List<MonitorModel> MonitorModels
        {
            get { return monitorModels; }
            set { monitorModels = value; this.DoNotify(); }
        }

        // 温度模板样式配置
        private TemplateModel templateModel;
        public TemplateModel TemplateModel
        {
            get { return templateModel; }
            set { templateModel = value; this.DoNotify(); }
        }

        // 预警列表存储
        private List<string> Waring { get; set; }
        //报警列表
        private List<string> Alert { get; set; }

        private int alertCount;

        public int AlertCount
        {
            get { return alertCount; }
            set { alertCount = value; this.DoNotify(); }
        }

        private int warnCount;

        public int WarnCount
        {
            get { return warnCount; }
            set { warnCount = value; this.DoNotify(); }
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

        readonly IBaseRepository<Station, Guid> stationRepository;
        readonly IBaseRepository<Diagram, Guid> diagramrRepositiry;
        readonly IBaseRepository<TemplateStyle, Guid> styleRepository;
        readonly IBaseRepository<DiagramConfig, Guid> diagramConfigRepository;
        readonly IBaseRepository<Sensor, Guid> sensorRepository;
        public Guid ActiveMonitorId { get; set; } // 选中的监测点id
        public OperationMonitorViewModel()
        {
           // this.ConfigModel = new ConfigModel();
            this.MonitorModels = new List<MonitorModel>();
            this.MonitorModels.Add(new MonitorModel() { Name = "请添顶级监测点" });
            stationRepository = ESTRepository.Builder<Station, Guid>();
            diagramrRepositiry = ESTRepository.Builder<Diagram, Guid>();
            styleRepository = ESTRepository.Builder<TemplateStyle, Guid>();
            diagramConfigRepository = ESTRepository.Builder<DiagramConfig, Guid>();
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();

            this.Alert = new List<string>();
            this.Waring=new List<string>();

            BuildTree();
        }

        // 加载树结构
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
                                //if (b.Terminals.Any())
                                //{
                                //    node2.Children = new List<TreeViewModel>();
                                //    b.Terminals?.ForEach(c =>
                                //    {
                                //        var node3 = new TreeViewModel();
                                //        node3.ParentId = b.Id;
                                //        node3.NodeName = c.Name;
                                //        node3.NodeType = TreeNodeType.Termianl;
                                //        node3.Id = c.Id;
                                //        node2.Children.Add(node3);
                                //    });
                                //}
                                node1.Children.Add(node2);
                            });
                        }
                        tree.Add(node1);
                    });
                    this.TreeViewModels = tree;
                }
            });
        }


        public void TreeSelected(TreeViewModel node)
        {
            this.TreeViewModel = node;
            // 如果时配电室，则显示 线路图
            if (node.NodeType ==TreeNodeType.Room)
            {
                // 查询并读取上传的图片资源,绑定图片的名称
                ReadImgdata();
                // 读取当前监测点的温度模板
                GetTemplateStyle();
                // 读取当前监测点的温度点数据
                RefreshDiagramConfigs();
            }
            else
            {
                // 不是配电室，展示该站点下的配电室列表及设备的信息等
            }
        }

        /// <summary>
        /// 读取图纸信息
        /// </summary>
        private void ReadImgdata()
        {
            var diagram = diagramrRepositiry.Where(a => a.PowerRoomId == this.TreeViewModel.Id).First();
            if (diagram != null)
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
                    Task.Run(() =>
                    {
                        var data = diagram.Data;
                        File.WriteAllBytes(filePath, data);
                        this.ReloadImage?.Invoke(this, new EventArgs());
                    });
                }
            }
            else
            {
                this.ReloadImage?.Invoke(false, new EventArgs());
            }
        }

        /// <summary>
        /// 更新温度点
        /// </summary>
        public void RefreshDiagramConfigs()
        {
            // 获取该配电室中的温度配置点数据
            var points = diagramrRepositiry.Select
                .Where(a => a.PowerRoomId == this.TreeViewModel.Id)
                .IncludeMany(a=>a.Configs).ToList()
                .SelectMany(a=>a.Configs).ToList();
            this.DiagramConfigModels = ObjectMapper.Map<List<DiagramConfigModel>>(points).CreateIndex();
            this.InitPoint?.Invoke(this, DiagramConfigModels);
        }

        // 获取当前监测点的温度模板
        private void GetTemplateStyle()
        {
            var temp = styleRepository.Where(a => a.PowerRoomId == this.TreeViewModel.Id).First();
            if (temp != null)
            {
                this.TemplateModel = ObjectMapper.Map<TemplateModel>(temp);
            }
            else
            {
                this.TemplateModel = null;
            }
        }

        /// <summary>
        /// 通过传感器编码查找温度点的名称
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string[] GetPointPropNameBySensorCode(string code)
        {
            var names = diagramConfigRepository
                .Where(a => a.SensorCode == code)
                .ToList()
                .Select(a => a.PropName);
            return names?.ToArray();
        }
        /// <summary>
        /// 设置实时状态
        /// </summary>
        /// <param name="name"></param>
        /// <param name="status"></param>
        public void SetStatus(string[] names,PointStatus status)
        {
         
            foreach (var name in names)
            {
                this.Waring.Remove(name);
                this.Alert.Remove(name);
                if (status == PointStatus.Normal)
                {
                    // 移除所有的报警
                    this.Waring.Remove(name);
                    this.Alert.Remove(name);
                }
                else if (status == PointStatus.Warning)
                {
                    this.Waring.Add(name);
                    this.Alert.Remove(name);

                }
                else
                {
                    this.Alert.Add(name);
                }
            }

            this.AlertCount = Alert.Count();
            this.WarnCount = Waring.Count();
        }
    }
}
