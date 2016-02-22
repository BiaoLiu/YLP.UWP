using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace YLP.UWP.Helpers
{
    public class VisualTreeHelper
    {
        /// <summary>
        /// 利用VisualTreeHelper寻找指定依赖对象的子级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            try
            {
                for (int i = 0; i < Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(parent); i++)
                {
                    var child = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(parent, i);
                    string controlName = child.GetValue(Control.NameProperty) as string;
                    if ((string.IsNullOrEmpty(name) || controlName == name) && child is T)
                    {
                        return child as T;
                    }

                    T result = FindVisualChildByName<T>(child, name);
                    if (result != null)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 利用VisualTreeHelper寻找指定依赖对象的父级对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            DependencyObject parent = Windows.UI.Xaml.Media.VisualTreeHelper.GetParent(obj);
            if (parent != null)
            {
                if (parent is T)
                {
                    return parent as T;
                }

                return FindVisualParent<T>(parent);
            }

            return null;
        }
    }
}
