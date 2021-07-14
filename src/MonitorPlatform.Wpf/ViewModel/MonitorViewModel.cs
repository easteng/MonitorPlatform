﻿/**********************************************************************
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
    public class MonitorViewModel: NotifyBase
    {
        // 监测点内容
        private MonitorModel monitorModel;

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

        public ICommand OpenLeftDrawCommand { get { return new CommandBase(OpenLeftDrawAction); } }
        public ICommand SaveMonitorCommand { get { return new CommandBase(SaveMonitorAction); } }
        public ICommand CloseDrawCommand { get { return new CommandBase(CloseDrawAction); } }
        public MonitorViewModel()
        {
            this.MonitorModels = new List<MonitorModel>();
            this.MonitorModels.Add(new MonitorModel() { Name = "请添顶级监测点" });
            monitorRepositiry = ESTRepository.Builder<Monitor, Guid>();
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

        // 上传图片操作
        public void UploadAction(object data)
        {

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


        // 打开右侧
        public void OpenRightDrawAction(object data)
        {
            this.RightShow = true;  
        }
        // 打开底部
        public void OpenBottomDrawAction(object data)
        {

        }
        // 查看 监测点的配置
        public void GetConfigAction(object obj)
        {
            if (this.MonitorModel.Type == Share.StationType.Device)
            {
                // 查询当前区域的配置信息
            }
        }




        // 新增监测点 
        public void CreateMonitorAction(object data)
        {

        }
    }
}