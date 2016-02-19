using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace YLP.UWP
{
    public class AvatarValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format("/Assets/avatar/{0}.png", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var id = Path.GetFileNameWithoutExtension(value.ToString());
            return int.Parse(id);
        }
    }
}
