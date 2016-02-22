using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    /// <summary>
    /// 用户作品
    /// </summary>
    public class UArticle : Author
    {
        public string articleid { get; set; }

        public string categoryid { get; set; }

        public string title { get; set; }

        public Picture pics { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string picurl
        {
            get
            {
                if (!string.IsNullOrEmpty(pics.url))
                {
                    var fileName = Path.GetFileNameWithoutExtension(pics.url);
                    var thumbFileName = fileName + "-thumb";

                    return pics.url.Replace(fileName, thumbFileName);
                }

                return string.Empty;
            }
        }

        public int piccount { get; set; }

        public int goods { get; set; }

        public int shares { get; set; }

        public int comments { get; set; }

        public int hits { get; set; }

        public int favorites { get; set; }

        public bool favioritestatus { get; set; }

        public bool attentionstatus { get; set; }

        public bool goodstatus { get; set; }

        public bool isgood { get; set; }

        public string tags { get; set; }

        public string lonx { get; set; }

        public string laty { get; set; }

        public string location { get; set; }

        public string tool { get; set; }

        public string pubtime { get; set; }

        public List<HotComment> hotcomments { get; set; }

        public string wapurl { get; set; }
    }
}
