/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.ViewModel
*******类 名 称： ServerManagerViewModel
*******类 说 明： 采集服务配置
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 11:09:35 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using ESTCore.ORM.FreeSql;

using FreeSql;

using HandyControl.Controls;

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
    public class ServerManagerViewModel : NotifyBase
    {
        private CollectionClientModel collectionClientModel;

        public CollectionClientModel CollectionClientModel
        {
            get { return collectionClientModel; }
            set { collectionClientModel = value; this.DoNotify(); }
        }

        private List<CollectionClientModel> collectionList;

        public List<CollectionClientModel> CollectionClientModels
        {
            get { return collectionList; }
            set { collectionList = value; this.DoNotify(); }
        }
        private List<ComboxItem> ptotocolList;
        public List<ComboxItem> PtotocolList
        {
            get => ptotocolList;
            set
            {
                ptotocolList=value;
                this.DoNotify();
            }
        }
        private List<ComboxItem> type;
        public List<ComboxItem> TypeList
        {
            get => type;
            set
            {
                type = value;
                this.DoNotify();
            }
        }
        private bool show;

        public bool Show
        {
            get { return show; }
            set { show = value; this.DoNotify(); }
        } 
        private bool bottomShow;

        public bool BottomShow
        {
            get { return bottomShow; }
            set { bottomShow = value; this.DoNotify(); }
        }

        public ICommand SaveCommand { get { return new CommandBase(SaveAction); } }
        public ICommand EditCommand { get { return new CommandBase(EditAction); } }
        public ICommand SearchCommand { get { return new CommandBase(SearchAction); } }
        public ICommand DebuggerCommand { get { return new CommandBase(OpenBottomAction); } }
        public ICommand CreateCommand { get { return new CommandBase(OpenDrawAction); } }
        public ICommand DeleteCommand { get { return new CommandBase(DeleteAction); } }
        readonly IBaseRepository<CollectionClient, Guid> collectionRepository;
        public ServerManagerViewModel()
        {
            collectionRepository= ESTRepository.Builder<CollectionClient, Guid>();
            PtotocolList=new  List<ComboxItem>();
            TypeList = new  List<ComboxItem>();
            var dic1 = typeof(PtotocolType).GetDescriptionAndValue();//协议类型
            var dic2 = typeof(DeviceCollectionType).GetDescriptionAndValue();//采集类型
            foreach (var item in dic1)
            {
                PtotocolList.Add(new ComboxItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
            foreach (var item in dic2)
            {
                TypeList.Add(new ComboxItem
                {
                    Key = item.Key,
                    Value = item.Value
                });
            }
            this.Refresh();
        }
        // 刷新数据
        private void Refresh(string code = "")
        {
            Task.Run(() =>
            {
                var list = collectionRepository
                .WhereIf(code != "", a => a.Name.Contains(code))
                .Where(a => true)
                .ToList();
                this.CollectionClientModels = ObjectMapper.Map<List<CollectionClientModel>>(list).CreateIndex();
            });
        }

        /// <summary>
        /// 打开抽屉
        /// </summary>
        /// <param name="data"></param>
        public void OpenDrawAction(object data)
        {
            this.CollectionClientModel = new CollectionClientModel();
            this.Show = bool.Parse(data.ToString());
        }
        public void OpenBottomAction(object data)
        {
            this.BottomShow = bool.Parse(data.ToString());
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="obj"></param>
        private void SearchAction(object obj)
        {
            if (obj is string)
            {
                this.Refresh(obj.ToString());
            }
        }
        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <param name="obj"></param>
        public void EditAction(object obj)
        {
            var sensor = collectionRepository.Get(Guid.Parse(obj.ToString()));
            var model = ObjectMapper.Map<CollectionClientModel>(sensor);
            this.CollectionClientModel = model;
            this.Show = true;
        }

        /// <summary>
        /// 编辑保存
        /// </summary>
        /// <param name="obj"></param>
        public void SaveAction(object obj)
        {

            var collectionModel = (CollectionClientModel)obj;
            var collection = ObjectMapper.Map<CollectionClient>(collectionModel);
            if (collection.Id != Guid.Empty)
            {
                collectionRepository.Update(collection);
            }
            else
            {
                collectionRepository.Insert(collection);
            }

            this.Show = false;
            Growl.Info("操作成功");
            this.Refresh();
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="obj"></param>
        public void DeleteAction(object obj)
        {
            if (obj != null)
            {
                var id = Guid.Parse(obj.ToString());
                collectionRepository.Delete(id);
                this.Refresh();
            }
        }

        /// <summary>
        /// 停止远程服务
        /// </summary>
        /// <param name="obj"></param>
        public void StopServerAction(object obj)
        {
            // todo 
        }
        /// <summary>
        /// 远程重启服务
        /// </summary>
        /// <param name="obj"></param>
        public void RestartServerAction(object obj)
        {
            // todo 
        }
        /// <summary>
        /// 挂起服务
        /// </summary>
        /// <param name="obj"></param>
        public void PuceServerAction(object obj)
        {
            // todo 
        }
    }
}
