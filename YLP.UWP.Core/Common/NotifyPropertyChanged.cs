using System.ComponentModel;

namespace YLP.UWP.Core.Common
{
    /// <summary>
    /// INotifyPropertyChanged 基本实现
    /// </summary>
    public abstract class NotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 属性变化
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

}
