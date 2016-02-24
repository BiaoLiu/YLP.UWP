using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class MainModelBase
    {
        public MainModelBase()
        { }

        public MainModelBase(UArticle uArticle)
        {
            articleid = uArticle.articleid;
            title = uArticle.title;
            pubtime = uArticle.pubtime;
            goods = uArticle.goods;
            comments = uArticle.comments;
            hits = uArticle.hits;
            region = RegionType.R4.ToString();
            userid = uArticle.userid;
            username = uArticle.username;

            pics = new Picture[] { new Picture() { url = uArticle.picurl } };
        }
        public string articleid { get; set; }
        public string subjectname { get; set; }
        public string title { get; set; }
        public Picture[] pics { get; set; }
        public string pubtime { get; set; }
        public int goods { get; set; }
        public int comments { get; set; }
        public int hits { get; set; }
        public string region { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
}
