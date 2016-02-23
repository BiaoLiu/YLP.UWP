using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;
using YLP.UWP.Core.ViewModels;

namespace YLP.UWP.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainService _api = new MainService();

        /// <summary>
        /// 轮播图
        /// </summary>
        public ObservableCollection<SlideArticle> SlideArticles { get; set; }

        /// <summary>
        /// R3区域文章(单图)
        /// </summary>
        public IncrementalLoading<ArticleV2> R3Aritcles { get; set; }

        /// <summary>
        /// R2区域文章(多图)
        /// </summary>
        public IncrementalLoading<ArticleV2> R2Articles { get; set; }

        public MainViewModel()
        {
            SlideArticles = new ObservableCollection<SlideArticle>();

            //页容量35
            R3Aritcles = new IncrementalLoading<ArticleV2>((pageIndex, pageSize) => _api.GetR3ArticleList(pageIndex, pageSize), 35);
            //页容量5
            R2Articles = new IncrementalLoading<ArticleV2>((pageIndex, pageSize) => _api.GetR2ArticleList(pageIndex, pageSize), 5);

            Update();
        }

        public async void Update()
        {
            var result = await _api.GetSlideArticles();

            result.Data?.ForEach(SlideArticles.Add);
        }
    }
}
