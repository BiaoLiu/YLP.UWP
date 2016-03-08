using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace YLP.UWP.Core
{
    public static class StringUtil
    {
        static ResourceLoader _resLoader = new ResourceLoader();

        public static string GetString(string id)
        {
            return _resLoader.GetString(id);
        }

        public static string DateToWeek(DateTime date)
        {
            switch(date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return GetString("Monday");
                case DayOfWeek.Tuesday:
                    return GetString("Tuesday");
                case DayOfWeek.Wednesday:
                    return GetString("Wednesday");
                case DayOfWeek.Thursday:
                    return GetString("Thursday");
                case DayOfWeek.Friday:
                    return GetString("Friday");
                case DayOfWeek.Saturday:
                    return GetString("Saturday");
                case DayOfWeek.Sunday:
                    return GetString("Sunday");
                default:
                    return string.Empty;
            }
        }

        public static string DisplayTime(DateTime date)
        {
            const int MINUTE = 60;
            TimeSpan ts = DateTime.Now - date;
            double delta = ts.TotalSeconds;

            if (delta < 0)
            {
                return "";
            }

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds + GetString("BeforeSec");
            }

            if (delta < 60 * MINUTE)
            {
                return ts.Minutes + GetString("BeforeMin");
            }

            return date.ToString("MM-dd HH:mm");
        }
    }
}
