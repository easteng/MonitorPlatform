/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Common
*******类 名 称： PaswordHelp
*******类 说 明： 密码帮助类
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 10:03:47 PM
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
using System.Windows;
using System.Windows.Controls;

namespace MonitorPlatform.Wpf.Common
{

    public class PaswordHelp
    {
        // 依赖属性的方式
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PaswordHelp),
                new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnChangedCallback)));
        public static readonly DependencyProperty AttachProperty =
            DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(PaswordHelp),
                new FrameworkPropertyMetadata(default(bool), new PropertyChangedCallback(OnAttached)));
        // 附加属性
        public static void SetPassword(DependencyObject d, string password)
        {
            d.SetValue(PasswordProperty, password);
        }
        public static string GetPassword(DependencyObject d)
        {
            return d.GetValue(PasswordProperty).ToString();
        }

        public static bool GetAttach(DependencyObject d)
        {
            return (bool)d.GetValue(AttachProperty);
        }
        public static void SetAttach(DependencyObject d, bool value)
        {
            d.SetValue(AttachProperty, value);
        }
        // 当密码框属性被触发时。
        private static void OnAttached(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;

        }

        // 属性发生改变时激活
        private static bool _isChanged = false;
        private static void OnChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var passwordBox = d as PasswordBox;
            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;
            if (!_isChanged)
                passwordBox.Password = e.NewValue.ToString();
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private static void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = (PasswordBox)sender;
            _isChanged = true;
            SetPassword(passwordBox, passwordBox.Password);
            _isChanged = false;
        }
    }
}
