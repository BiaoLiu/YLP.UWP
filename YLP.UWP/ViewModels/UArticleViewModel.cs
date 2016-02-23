using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;
using YLP.UWP.Core.ViewModels;

namespace YLP.UWP.ViewModels
{
    public class UArticleViewModel : ViewModelBase
    {
        private readonly UArticleService _api = new UArticleService();

        /// <summary>
        /// 热门用户作品
        /// </summary>
        public IncrementalLoading<UArticle> HotUArticles { get; set; }
    
        /// <summary>
        /// 最新用户作品
        /// </summary>
        public IncrementalLoading<UArticle> LatestUArticles { get; set; }

        /// <summary>
        /// 关注用户作品
        /// </summary>
        public IncrementalLoading<UArticle> AttentionUArticles { get; set; }

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

        public UArticleViewModel(string otherUserId, string tag)
        {
            //热门用户作品
            HotUArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, UArticleType.hot.ToString(), tag, p, s));
            HotUArticles.DataLoading += DataLoading;
            HotUArticles.DataLoaded += DataLoaded;

            //最新用户作品
            LatestUArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, UArticleType.latest.ToString(), tag, p, s));
            LatestUArticles.DataLoading += DataLoading;
            LatestUArticles.DataLoaded += DataLoaded;

            //关注用户作品
            AttentionUArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, UArticleType.focus.ToString(), tag, p, s));
            AttentionUArticles.DataLoading += DataLoading;
            AttentionUArticles.DataLoaded += DataLoaded;
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
