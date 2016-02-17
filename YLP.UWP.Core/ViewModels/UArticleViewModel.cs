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

        private IncrementalLoading<UArticle> _uarticles;
        public IncrementalLoading<UArticle> UArticles
        {
            get
            {
                return _uarticles;
            }
            set
            {
                SetProperty(ref _uarticles, value);
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
            UArticles = new IncrementalLoading<UArticle>((p, s) => _api.GetUArticles(otherUserId, type, tag, p, s));

            UArticles.DataLoading += DataLoading;
            UArticles.DataLoaded += DataLoaded;
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
