using ESTCore.ORM.FreeSql;
using FreeSql;
using HandyControl.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;
using MonitorPlatform.Wpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MonitorPlatform.Wpf.ViewModel
{
   public class SMSConfigViewModel: NotifyBase
    {
        private SMSConfigModel sMSConfig;

        public SMSConfigModel SMSConfig
        {
            get { return sMSConfig; }
            set { sMSConfig = value; this.DoNotify(); }
        }

        private List<SMSConfigModel> smsConfigs;

        public List<SMSConfigModel> SMSConfigs
        {
            get { return smsConfigs; }
            set { smsConfigs = value; this.DoNotify(); }
        }
        public List<string> ComNames
        {
            get;set;
        }
        public List<int> BaudRates { get; set; }

        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand OpenDrawCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand CreateCommand { get { return new CommandBase(CreateAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        private bool isOpen;
        public bool Open
        {
            get { return isOpen; }
            set { isOpen = value; this.DoNotify(); }
        }
        private bool IsEdit { get; set; }

        readonly IBaseRepository<SmsConfig, Guid> smsRepository;
        public SMSConfigViewModel()
        {
            smsRepository = ESTRepository.Builder<SmsConfig, Guid>();

            this.Refresh();
            ComNames = new List<string>();
            ComNames.Add("COM1");
            ComNames.Add("COM2");
            ComNames.Add("COM3");
            ComNames.Add("COM4");
            ComNames.Add("COM5");
            ComNames.Add("COM6");
            ComNames.Add("COM7");
            ComNames.Add("COM8");
            ComNames.Add("COM9");
            ComNames.Add("COM10");

            BaudRates = new List<int>();
            BaudRates.Add(4800);
            BaudRates.Add(9600);
            BaudRates.Add(19200);

            SMSConfig = new SMSConfigModel();
        }

        private void Refresh()
        {
            Task.Run(() =>
            {
                var userList = smsRepository
               .Where(a => true).ToList();
                this.SMSConfigs = ObjectMapper.Map<List<SMSConfigModel>>(userList).CreateIndex();
            });
        }

        private void CreateAction(object data)
        {
            var user = (SMSConfigModel)data;
            var entity = ObjectMapper.Map<SmsConfig>(user);

            if (entity.Id != Guid.Empty)
            {
                smsRepository.Update(entity);
            }
            else
            {
                smsRepository.Insert(entity);
            }
            // 添加成功
            this.Open = false;
            Refresh();
            Growl.Info("操作成功");
        }
        private void OpenDrawAction(object data)
        {
            this.Open = bool.Parse(data.ToString());
        }
        public void EditAction(object data)
        {
            this.Open = true;
            this.IsEdit = true;
            var user = smsRepository.Get(Guid.Parse(data.ToString()));
            this.SMSConfig = ObjectMapper.Map<SMSConfigModel>(user);
        }
        public void DeleteAction(object data)
        {
            if (data != null)
            {
                var id = Guid.Parse(data.ToString());
                smsRepository.Delete(a => a.Id == id);
                this.Refresh();
            }
        }

        public void Enable(Guid id)
        {
            var sms=smsRepository.Get(id);
            if(sms != null)
            {
                sms.Enable = true;
                smsRepository.Update(sms);
                Growl.Info("启用成功");
            }
        }
    }
}
