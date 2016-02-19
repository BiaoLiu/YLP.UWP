using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.Core.ViewModels
{
    public class UArticleViewModel : ViewModelBase
    {
        private readonly UArticleService _api = new UArticleService();

        /// <summary>
        /// 热门用户作品
        /// </summary>
        private IncrementalLoading<UArticle> _hotUarticles;
        public IncrementalLoading<UArticle> HotUArticles
        {
            get
            {
                return _hotUarticles;
            }
            set
            {
                SetProperty(ref _hotUarticles, value);
            }
        }

        /// <summary>
        /// 最新用户作品
        /// </summary>
        private IncrementalLoading<UArticle> _latestUarticles;
        public IncrementalLoading<UArticle> LatestUArticles
        {
            get
            {
                return _latestUarticles;
            }
            set
            {
                SetProperty(ref _latestUarticles, value);
            }
        }

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

        public UArticleViewModel(string otherUserId, string type, string tag)
        {
            //热门用户作品
            HotUArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, UArticleType.hot.ToString(), tag, p, s));
            HotUArticles.DataLoading += DataLoading;
            HotUArticles.DataLoaded += DataLoaded;

            //最新用户作品
            LatestUArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, UArticleType.latest.ToString(), tag, p, s));
            LatestUArticles.DataLoading += DataLoading;
            LatestUArticles.DataLoaded += DataLoaded;
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
