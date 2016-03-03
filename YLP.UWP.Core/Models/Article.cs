using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class Article : Author
    {
        public string articleid { get; set; }
        public string categoryid { get; set; }
        public string subjectid { get; set; }
        public string subjectname { get; set; }
        public string title { get; set; }
        public Picture pics { get; set; }
        public int piccount { get; set; }
        public string content { get; set; }
        public string tags { get; set; }
        public string pubtime { get; set; }
        public int goods { get; set; }
        public int shares { get; set; }
        public int comments { get; set; }
        public int hits { get; set; }
        public int favorites { get; set; }
        public string region { get; set; }
        public string wapurl { get; set; }

        public object[] mutilmedias { get; set; }
    }
}