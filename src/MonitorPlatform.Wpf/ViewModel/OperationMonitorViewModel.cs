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
        private List<string> waring;

        public List<string> Waring
        {
            get { return waring; }
            set { waring = value; }
        }
        //报警列表

        private List<string> alert;

        public List<string> Alert
        {
            get { return alert; }
            set { alert = value; }
        }


        readonly IBaseRepository<Monitor, Guid> monitorRepositiry;
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
            monitorRepositiry = ESTRepository.Builder<Monitor, Guid>();
            diagramrRepositiry = ESTRepository.Builder<Diagram, Guid>();
            styleRepository = ESTRepository.Builder<TemplateStyle, Guid>();
            diagramConfigRepository = ESTRepository.Builder<DiagramConfig, Guid>();
            sensorRepository = ESTRepository.Builder<Sensor, Guid>();

            this.Alert = new List<string>();
            this.Waring=new List<string>();

            Refresh();
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

        public void TreeSelected(MonitorModel model)
        {
            if (model.Type == StationType.Region)
            {
                // 查询并读取上传的图片资源,绑定图片的名称
                ReadImgdata();
                // 读取当前监测点的温度模板
                GetTemplateStyle();
                // 读取当前监测点的温度点数据
                RefreshDiagramConfigs();
            }
        }

        /// <summary>
        /// 读取图纸信息
        /// </summary>
        private void ReadImgdata()
        {
            var id = this.ActiveMonitorId;// 监测点id
            var diagram = diagramrRepositiry.Where(a => a.MonitorId == id).First();
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
                    var data = diagram.Data;
                    File.WriteAllBytes(filePath, data);
                    this.ReloadImage?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// 更新温度点
        /// </summary>
        public void RefreshDiagramConfigs()
        {
            var list = diagramrRepositiry
                .Orm
                .Select<Diagram, DiagramConfig>()
                .Where((d, c) => d.MonitorId == this.ActiveMonitorId && c.DiagramId == d.Id)
                .ToList<DiagramConfig>();
            this.DiagramConfigModels = ObjectMapper.Map<List<DiagramConfigModel>>(list).CreateIndex();
            this.InitPoint?.Invoke(this, DiagramConfigModels);
        }

        // 获取当前监测点的温度模板
        private void GetTemplateStyle()
        {
            var monitorId = this.ActiveMonitorId;
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
        }
    }
}
