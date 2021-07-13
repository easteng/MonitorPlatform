using ESTHost.ServerManager;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESTHostServerManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private static void Init()
        {
            // 定义系统检查来行
            // 1、 数据库  2、redis   3、rabbitmq  4、数据存储服务   5、采集服务1  6、采集服务2  7、短息服务

            Task.Run(async () =>
            {
                var databaseCheck=await EnvironmentCheck.DatabaseCheck();
            });
        }
    }
}
