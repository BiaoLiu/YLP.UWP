using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Extensions
{
    public static class StringExtension
    {
        public static bool CheckSuccess(this string instance)
        {
            return instance.Equals(((int)OperateCode.Success).ToString());
        }

        public static IEnumerable<KeyValuePair<string, string>> AsKVP(this NameValueCollection source)
        {
            return source.AllKeys.SelectMany(source.GetValues, (k, v) => new KeyValuePair<string, string>(k, v));
        }
    }
}
