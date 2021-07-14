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
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class MonitorViewModel: NotifyBase
    {
        public event EventHandler<EventArgs> ReloadImage;
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
        // 监测点类型
        private List<ComboxItem> monitorTypes;

        public List<ComboxItem> MonitorTypes
        {
            get { return monitorTypes; }
            set { monitorTypes = value; this.DoNotify(); }
        }
        readonly IBaseRepository<Monitor, Guid> monitorRepositiry;
        readonly IBaseRepository<Diagram, Guid> diagramrRepositiry;

        public ICommand OpenLeftDrawCommand { get { return new CommandBase(OpenLeftDrawAction); } }
        public ICommand SaveMonitorCommand { get { return new CommandBase(SaveMonitorAction); } }
        public ICommand CloseDrawCommand { get { return new CommandBase(CloseDrawAction); } }
        public ICommand GetConfigCommand { get { return new CommandBase(GetConfigAction); } }
        public ICommand OpenRightDrawCommand { get { return new CommandBase(OpenRightDrawAction); } }
        public MonitorViewModel()
        {
            this.ConfigModel=new ConfigModel();
            this.MonitorModels = new List<MonitorModel>();
            this.MonitorModels.Add(new MonitorModel() { Name = "请添顶级监测点" });
            monitorRepositiry = ESTRepository.Builder<Monitor, Guid>();
            diagramrRepositiry = ESTRepository.Builder<Diagram, Guid>();
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
        private bool AddChild { get; set; }
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
            }else
            {
                this.BottomShow = false;
            }
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
                // 查询并读取上传的图片资源
                ReadImgdata();
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
                    }
                    else
                    {
                        diagram = new Diagram();
                        diagram.Data = data;
                        diagram.MonitorId = this.ConfigModel.CurrentId;
                        diagramrRepositiry.Insert(diagram);
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
    }
}
