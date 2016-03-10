using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace YLP.UWP
{
    public class SubjectTypeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var type = value.ToString();
            if (type == "今日")
            {
                return new SolidColorBrush(Color.FromArgb(255, 236, 56, 72));
            }
            if (type == "更新")
            {
                return new SolidColorBrush(Color.FromArgb(255, 62, 195, 127));
            }
            if (type == "完结")
            {
                return new SolidColorBrush(Color.FromArgb(255, 106, 91, 96));
            }

            return new SolidColorBrush(Color.FromArgb(255, 236, 56, 72));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
