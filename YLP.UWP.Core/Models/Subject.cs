using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Models
{
    public class Subject
    {
        public string subjectid { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }

        #region MyRegion
        /// <summary>
        /// 描述
        /// </summary>
        public string descript { get; set; }

        /// <summary>
        /// 专题类型
        /// </summary>
        public string type { get; set; }

        public Picture pics { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string pubtime { get; set; } 
        #endregion

        /// <summary>
        /// 专题推荐文章
        /// </summary>
        public ObservableCollection<Article> recommendarticles { get; set; } 
    }
}
