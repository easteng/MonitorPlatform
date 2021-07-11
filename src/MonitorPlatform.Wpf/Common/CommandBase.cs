/**********************************************************************
*******命名空间： MonitorPlatform.Wpf.Common
*******类 名 称： CommandBase
*******类 说 明： 
*******作    者： Easten
*******机器名称： DESKTOP-EC8U0GP
*******CLR 版本： 4.0.30319.42000
*******创建时间： 7/4/2021 10:02:04 PM
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
using System.Windows.Input;

namespace MonitorPlatform.Wpf.Common
{
    /// <summary>
    /// 命令公共类
    /// </summary>
    //public class CommandBase : ICommand
    //{
    //    public event EventHandler CanExecuteChanged;

    //    public bool CanExecute(object parameter) => DoCanExecute?.Invoke(parameter) ?? false;

    //    public void Execute(object parameter) => DoExecute?.Invoke(parameter);

    //    public Action<object> DoExecute { get; set; }
    //    public Func<object, bool> DoCanExecute { get; set; }

    //    public static CommandBase Builder(Action<object> action, bool can = false)
    //    {
    //        var command = new CommandBase();
    //        command.DoCanExecute = DoCan(can);
    //        command.DoExecute = Do(action);
    //        return command;
    //    }

    //    private static Action<object> Do(Action<object> action)
    //    {
    //        return new Action<object>(action);
    //    }

    //    private static Func<object, bool> DoCan(bool can = true)
    //    {
    //        return new Func<object, bool>(a => can);
    //    }
    //}
    public class CommandBase : ICommand
    {
        private readonly Action<object> _commandpara;
        private readonly Action _command;
        private readonly Func<bool> _canExecute;

        public CommandBase(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }
            _canExecute = canExecute;
            _command = command;
        }

        public CommandBase(Action<object> commandpara, Func<bool> canExecute = null)
        {
            if (commandpara == null)
            {
                throw new ArgumentNullException();
            }
            _canExecute = canExecute;
            _commandpara = commandpara;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                _commandpara(parameter);
            }
            else
            {
                if (_command != null)
                {
                    _command();
                }
                else if (_commandpara != null)
                {
                    _commandpara(null);
                }
            }
        }
    }
}
