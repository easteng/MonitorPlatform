/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Common
*******类 名 称： BaseExtensions
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/11/2021 7:18:24 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MonitorPlatform.Wpf
{
    public static class BaseExtensions
    {
        public  static List<T> CreateIndex<T>(this List<T> list) where T : class
        {
            if(list == null|| !list.Any()) return null;
            var index = 0;
            foreach (var item in list)
            {
                var props = item.GetType().GetProperties();
                foreach (var prop in props)
                {
                    if (prop.Name == "Index")
                    {
                        prop.SetValue( item, ++index);
                    }
                }
            }
            return list;
        }

        public static void ExpandAll(this TreeView treeView)
        {
            SetNodeExpandedState(treeView, true);
        }
      
        private static void SetNodeExpandedState(ItemsControl control, bool expandNode)
        {
            try
            {
                if (control != null)
                {
                    foreach (object item in control.Items)
                    {
                        TreeViewItem treeItem = control.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;

                        if (treeItem != null && treeItem.HasItems)
                        {

                            treeItem.IsExpanded = expandNode;
                            if (treeItem.ItemContainerGenerator.Status != System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
                            {
                                treeItem.UpdateLayout();
                            }

                            SetNodeExpandedState(treeItem as ItemsControl, expandNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
