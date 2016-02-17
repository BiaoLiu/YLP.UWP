using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    /// <summary>
    /// 轮播区域文章
    /// </summary>
    public class SlideArticle
    {
        public string articleid { get; set; }

        public string title { get; set; }

        public Picture pics { get; set; }

        public string tags { get; set; }

        public string pubtime { get; set; }

        //点击类型(1,跳转文章,2.跳转网页)
        public string clicktype { get; set; }
    }
}

