/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： TemplateViewModel
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/15/2021 11:18:19 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class TemplateViewModel : NotifyBase
    {
        private TemplateModel templateModel;

        public TemplateModel TemplateModel
        {
            get { return templateModel; }
            set { templateModel = value; this.DoNotify(); }
        }

        public TemplateViewModel()
        {
            InitDefaultTemplateValue();
        }

        // 赋初始值
        private void InitDefaultTemplateValue()
        {
            templateModel = new TemplateModel()
            {
                BorderBackground = "#286A7B",
                BorderThickness = 2,
                CornerRadius = 5,
                BorderBrush = "#30B1D2",
                BorderHeight = 30,
                BorderWidth = 60,
                FontSize=14,
                DefaultValueForeground = "#DCF911",
                WaringValueForegrund= "#FD7E07",
                AlertValueForegrund= "#FF1900"
            };
            templateModel.ValueForeground = templateModel.DefaultValueForeground;
            GetDefaultColor(templateModel);
        }

        private void GetDefaultColor(TemplateModel templateModel)
        {
            templateModel.BadgeBackground = "#0000";
            templateModel.AlertBadgeColor = "#FB0606";
            templateModel.WaringBadgeColor = "#FF8305";
            templateModel.NormalBadgeColor = "#0000";
        }
        /// <summary>
        /// 外部控件传值并初始化
        /// </summary>
        /// <param name="model"></param>
        public TemplateModel InitTemplate(TemplateModel model)
        {
            if(model == null)
            {
                InitDefaultTemplateValue();
            }
            else
            {
                this.TemplateModel = model;
                GetDefaultColor(this.TemplateModel);
            }

            return this.TemplateModel;
        }
        /// <summary>
        /// 更新温度数据
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="status"></param>
        public void Update(string code,double value, PointStatus status)
        {
            if (this.TemplateModel.SensorCode == code)
            {
                this.TemplateModel.Value = value;
                switch (status)
                {
                    case PointStatus.Normal:
                        this.TemplateModel.ValueForeground = this.TemplateModel.DefaultValueForeground;
                        this.TemplateModel.BadgeBackground = this.TemplateModel.NormalBadgeColor;
                        break;
                    case PointStatus.Warning:
                        this.TemplateModel.ValueForeground = this.TemplateModel.WaringValueForegrund;
                        this.TemplateModel.BadgeBackground = this.TemplateModel.WaringBadgeColor;
                        break;
                    case PointStatus.Alerting:
                        this.TemplateModel.ValueForeground = this.TemplateModel.AlertValueForegrund;
                        this.TemplateModel.BadgeBackground = this.TemplateModel.AlertBadgeColor;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
