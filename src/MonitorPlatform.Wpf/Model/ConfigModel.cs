/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Model
*******类 名 称： ConfigModel
*******类 说 明： 电路图配置实体定义
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/15/2021 12:39:55 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Share;
using MonitorPlatform.Wpf.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.Model
{
    public class ConfigModel: NotifyBase
    {
        public Guid CurrentId { get; set;  }
        /// <summary>
        /// 当前的监测点
        /// </summary>
        private string currentMonitor;

        public string CurrentMonitor
        {
            get { return currentMonitor; }
            set { currentMonitor = value; this.DoNotify(); }
        }

        //private StationType stationType;

        //public StationType StationType
        //{
        //    get { return stationType; }
        //    set { stationType = value; this.DoNotify(); }
        //}

        /// <summary>
        /// 是否可以上传
        /// </summary>
        private bool canUpload;

        public bool CanUpload
        {
            get { return canUpload; }
            set { canUpload = value; this.DoNotify(); }
        }
        /// <summary>
        /// 是否编辑状态
        /// </summary>
        private bool isEdit;

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value; this.DoNotify(); }
        }

        /// <summary>
        /// 监测点关联的图片的名称
        /// </summary>
        private string picName;

        public string PicName
        {
            get { return picName; }
            set { picName = value; this.DoNotify(); }
        }

        public string  SelectedFilePath { get; set; }
    }
}
