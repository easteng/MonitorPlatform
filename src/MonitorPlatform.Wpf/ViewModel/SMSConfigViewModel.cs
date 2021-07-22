using MonitorPlatform.Wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.ViewModel
{
   public  class SMSConfigViewModel: NotifyBase
    {
        private SMSConfig sMSConfig;

        public SMSConfig SMSConfig
        {
            get { return sMSConfig; }
            set { sMSConfig = value; }
        }
    }
}
