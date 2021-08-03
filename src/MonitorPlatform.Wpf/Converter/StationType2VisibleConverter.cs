/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Converter
*******类 名 称： StationType2VisibleConverter
*******类 说 明： 
*******作    者： Easten
*******机器名称： EASTEN
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/30/2021 12:54:01 PM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @Easten 2020-2021. All rights reserved ★ *********
***********************************************************************
 */
using MonitorPlatform.Share;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MonitorPlatform.Wpf.Converter
{
    ///// <summary>
    /////  站点类型转空间可视化
    ///// </summary>
    //public class StationType2VisibleConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var type = (TreeNodeType)value;
    //        switch (type)
    //        {
    //            case TreeNodeType.Station:
    //                break;
    //            case TreeNodeType.Room:
    //                break;
    //            case TreeNodeType.Termianl:
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //}

    public class StationType2IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (TreeNodeType)value;
            if (type == TreeNodeType.Station)
            {
                return Regex.Unescape(StringToUnicode("&#xe666;"));
            }
            else if (type == TreeNodeType.Termianl)
            {
                return Regex.Unescape(StringToUnicode("&#xe66c;"));
            }
            else
            {
                return Regex.Unescape(StringToUnicode("&#xe668;"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static string StringToUnicode(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                //这里把格式&#xe625; 转为 \ue625
                return s.Replace(@"&#x", @"\u").Replace(";", "");
            }
            return s;
        }
    }
}
