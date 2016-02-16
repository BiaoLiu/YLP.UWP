using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YLP.UWP.Core.Common
{
    /// <summary>
    /// ICommand 实现
    /// </summary>
    public class Command : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        /// <summary>
        /// 构造一个执行命令对象
        /// </summary>
        /// <param name="canExecute">命令是否可以被执行</param>
        /// <param name="execute">执行委托</param>
        public Command(Func<object, bool> canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        /// <summary>
        /// 当前 Command 是否可以被执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可以被执行</returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        /// <summary>
        /// 逻辑上发生影响当前CanExecute执行结果操作过后需要调用
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 通知判断是否可以执行依据发生变化
        /// </summary>
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 命令执行
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
