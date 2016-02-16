using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Common;

namespace YLP.UWP.Core.ViewModels
{
    /// <summary>
    /// 视图模型基类
    /// </summary>
    public abstract class ViewModelBase : NotifyPropertyChanged
    {
        /// <summary>
        /// 设置属性变化，内部通知修改
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="current">当前属性</param>
        /// <param name="value">新设置的值</param>
        /// <param name="propertyName">属性名称</param>
        protected void SetProperty<T>(ref T current, T value, [CallerMemberName]string propertyName = null)
        {
            if (object.Equals(current, value))
            {
                // 如果值没有发生修改，没有必要修改
                return;
            }
            current = value;
            // 通知修改
            this.OnPropertyChanged(propertyName);
        }
    }
}
