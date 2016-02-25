using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.NetworkOperators;
using YLP.UWP.Core;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.ViewModels
{
    public class CommentViewModel : BindableBase
    {
        private readonly CommonService _api = new CommonService();

        /// <summary>
        /// 最新评论
        /// </summary>
        public IncrementalLoading<Comment> LatestComments { get; set; }

        /// <summary>
        /// 热门评论
        /// </summary>
        public IncrementalLoading<Comment> HotComments { get; set; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public CommentViewModel(string articleId, string type)
        {
            LatestComments = new IncrementalLoading<Comment>((pageIndex, pageSize) => _api.GetComments(articleId, type, SortType.latest.ToString(), pageIndex, pageSize));
            LatestComments.DataLoading += DataLoading;
            LatestComments.DataLoaded += DataLoaded;

            HotComments = new IncrementalLoading<Comment>((pageIndex, pageSize) => _api.GetComments(articleId, type, SortType.hot.ToString(), pageIndex, pageSize));
            HotComments.DataLoading += DataLoading;
            HotComments.DataLoaded += DataLoaded;
        }

        private void DataLoading()
        {
            IsLoading = true;
        }

        private void DataLoaded()
        {
            IsLoading = false;
        }
    }
}
