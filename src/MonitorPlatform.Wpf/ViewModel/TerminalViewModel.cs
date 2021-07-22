using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Wpf.ViewModel
{
    public class TerminalViewModel: NotifyBase
    {
        private TerminalModel terminalModel;

        public TerminalModel TerminalModel
        {
            get { return terminalModel; }
            set { terminalModel = value; this.DoNotify(); }
        }




    }
}
