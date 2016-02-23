using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Annotations;

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

        private int _goods;
        public int goods
        {
            get
            {
                return _goods;
            }
            set
            {
                SetProperty(ref _goods, value);
            }
        }

        private int _shares;
        public int shares
        {
            get
            {
                return _shares;
            }
            set
            {
                SetProperty(ref _shares, value);
            }
        }

        private int _comments;
        public int comments
        {
            get
            {
                return _comments;
            }
            set
            {
                SetProperty(ref _comments, value);
            }
        }

        public int hits { get; set; }

        public int favorites { get; set; }

        private bool _favioritestatus;
        public bool favioritestatus
        {
            get
            {
                return _favioritestatus;
            }
            set
            {
                SetProperty(ref _favioritestatus, value);
            }
        }

        private bool _attentionstatus;
        public bool attentionstatus
        {
            get
            {
                return _attentionstatus;

            }
            set
            {
                SetProperty(ref _attentionstatus, value);
            }
        }

        private bool _goodstatus;
        public bool goodstatus
        {
            get
            {
                return _goodstatus;
            }
            set
            {
                SetProperty(ref _goodstatus, value);
            }
        }

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
