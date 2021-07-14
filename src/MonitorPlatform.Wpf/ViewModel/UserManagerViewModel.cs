/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： UserManagerViewModel
*******类 说 明： 用户管理
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:34:23 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.ORM.FreeSql;

using FreeSql;

using MonitorPlatform.Domain.Entities;
using MonitorPlatform.Wpf.Common;
using MonitorPlatform.Wpf.Model;

using Silky.Lms.AutoMapper;
using Silky.Lms.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using HandyControl.Controls;
namespace MonitorPlatform.Wpf.ViewModel
{

    public class UserManagerViewModel : NotifyBase
    {
        private List<UserModel> userList;

        public List<UserModel> UserList
        {
            get { return userList; }
            set { userList = value; this.DoNotify(); }
        }
        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand SearchCommand { get { return new CommandBase(SearchAction); } }
        public ICommand OpenDrawCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand CreateCommand { get { return new CommandBase(CreateAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        private bool isOpen;
        public bool Open
        {
            get { return isOpen; }
            set { isOpen = value; this.DoNotify(); }
        }
        private bool IsEdit { get; set;  }
        private UserModel userModel;

        public UserModel UserModel
        {
            get { return userModel; }
            set { userModel = value; this.DoNotify(); }
        }

        readonly IBaseRepository<User,Guid> userRepository;
        public UserManagerViewModel()
        {
            userRepository = ESTRepository.Builder<User,Guid>();
            Refresh();
        }

        private void Refresh(string key="")
        {
            Task.Run(() =>
            {
                var userList = userRepository
               .WhereIf(!string.IsNullOrWhiteSpace(key), a => a.Name.Contains(key))
               .Where(a => true).ToList();
                this.UserList = ObjectMapper.Map<List<UserModel>>(userList).CreateIndex();
            });
        }

        // 查询命令
        private void SearchAction(object data)
        {
            Refresh(data.ToString());
        }
        private void CreateAction(object data)
        {
            var user=(UserModel)data;
            var entity = ObjectMapper.Map<User>(user);

            if(entity.Id!=Guid.Empty)
            {
                userRepository.Update(entity);
            }
            else
            {
                userRepository.Insert(entity);
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
            var user = userRepository.Get(Guid.Parse(data.ToString()));
            this.UserModel= ObjectMapper.Map<UserModel>(user);
        }  
        public void DeleteAction(object data)
        {
            if (data != null) 
            { 
                var id = Guid.Parse(data.ToString());
                userRepository.Delete(a => a.Id == id);
                this.Refresh();
            }
        }
    }
}
