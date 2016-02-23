using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class Comment : Author
    {
        public string commentid { get; set; }

        public string articleid { get; set; }

        public string comment { get; set; }

        public string pubtime { get; set; }

        private int _commentgoods;
        public int commentgoods
        {
            get
            {
                return _commentgoods;
            }
            set
            {
                SetProperty(ref _commentgoods, value);
            }
        }

        public string atuserid { get; set; }

        public string atcommentid { get; set; }

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

        public int floor { get; set; }

        public string lonx { get; set; }

        public string laty { get; set; }

        public string location { get; set; }

        public string commentstyle { get; set; }

        public string deviceid { get; set; }

        public string type { get; set; }

        // public SmilieDto smilies { get; set; } = new SmilieDto();
    }

    public class HotComment : Author
    {
        public string commentid { get; set; }

        public string comment { get; set; }
    }
}
