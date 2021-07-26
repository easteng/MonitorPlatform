using ESTHost.ServerManager;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
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
        private ServiceController[] serviceList { get; set;  }
        private static void Init()
        {
            // 定义系统检查来行
            // 1、 数据库  2、redis   3、rabbitmq  4、数据存储服务   5、采集服务1  6、采集服务2  7、短息服务
           
            Task.Run(async () =>
            {
                var databaseCheck=await EnvironmentCheck.DatabaseCheck();

            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 卸载存储服务
            var service = serviceList.FirstOrDefault(a => a.ServiceName == "EST_DataStorage");
            if (service.Status == ServiceControllerStatus.Running)
            {
                service.Stop();
                Thread.Sleep(1000);
            }

           // Task.Run(() => Process.Start("sc.exe", $"delete EST_DataStorage")).Result);
            MessageBox.Show("服务已卸载");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 数据存储服务安装
            var serviceName = "EST_DataStorage";

            //判断是否存在


            var startModel = "auto";
            var binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ESTDataStorage\\ESTHost.StorageService.exe");
            var displayName = "\u0045\u0053\u0054\u6d4b\u6e29\u6570\u636e\u5b58\u50a8\u670d\u52a1";
            var startInfo = new ProcessStartInfo
            {
                FileName = "sc.exe",
                Arguments = $"create \"{serviceName}\" start={startModel} binPath=\"{binPath}\" DisplayName=\"{displayName}\"",
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            try
            {
                using (var installationProcess = Process.Start(startInfo))
                {
                    installationProcess.OutputDataReceived += InstallationProcess_OutputDataReceived;
                    installationProcess.BeginOutputReadLine();
                    installationProcess.WaitForExit();

                    MessageBox.Show("已安装成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "服务安装过程遇到了问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void InstallationProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data))
                return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            serviceList = ServiceController.GetServices();

            if(serviceList.Any(a=>a.ServiceName== "EST_DataStorage"))
            {
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
            }

            foreach (var item in serviceList)
            {
                if (item.ServiceName == "EST_DataStorage")
                {
                    
                    if (item.Status == ServiceControllerStatus.Running)
                    {
                        label5.Text = "正在运行";
                        button1.Enabled = false;
                    }
                    else if(item.Status == ServiceControllerStatus.Stopped){
                        button2.Enabled = true;
                        label5.Text = "已停止";
                    }else if(item.Status == ServiceControllerStatus.Paused)
                    {
                        button2.Enabled = true;
                        label5.Text = "已挂起";
                    }
                }
            }
        }
    }
}
