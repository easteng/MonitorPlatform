/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Converter
*******类 名 称： StringToIconConverter
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/7/2021 12:41:42 AM
*******联系方式： 1301485237@qq.com
***********************************************************************
******* ★ Copyright @easten company 2021-2022. All rights reserved ★ *********
***********************************************************************
 */
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
    public class StringToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return Regex.Unescape(StringToUnicode(value.ToString()));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>  
        /// 字符串转为UniCode码字符串  
        /// </summary>  
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
