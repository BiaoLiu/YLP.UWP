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

        private IncrementalLoading<ArticleV2> _article;
        public IncrementalLoading<ArticleV2> Articles { get; set; }

        public MainViewModel()
        {
            SlideArticles = new ObservableCollection<SlideArticle>();

            ////页容量35
            //R3Aritcles = new IncrementalLoading<ArticleV2>(35, (pageIndex, pageSize) => _api.GetR3ArticleList(pageIndex, pageSize));
            ////页容量5
            //R2Articles = new IncrementalLoading<ArticleV2>(5, (pageIndex, pageSize) => _api.GetR2ArticleList(pageIndex, pageSize));

            Articles = new IncrementalLoading<ArticleV2>(new int[] { 35, 5 }, (p, s) => _api.GetR3ArticleList(p, s), (p, s) => _api.GetR2ArticleList(p, s));
            Articles.ResultProcess = Test;

            Update();
        }

        public async void Update()
        {
            var result = await _api.GetSlideArticles();

            result.Data?.ForEach(SlideArticles.Add);
        }


        public void Test(List<ArticleV2> articles )
        {

            var r2Article = articles.Where(a => a.region == "R2").ToList();
            var r2Count = r2Article.Count();

            var r3Article = articles.Where(a => a.region == "R3").ToList();
            var r3Count = r3Article.Count();

            articles.Clear();

            int r3 = 0;
            int u = 0;
            for (int i = 0; i < r2Count; i++)
            {
                for (; r3 < r3 + 3; r3++)
                {
                    if (r3Count <= r3)
                    {
                        break;
                    }

                    articles.Add(r3Article[r3]);
                }

                //输出i

                articles.Add(r2Article[i]);

                for (; u < u + 1; u++)
                {

                }

                for (; r3 < r3 + 4; r3++)
                {
                    articles.Add(r3Article[r3]);
                }
            }

            //if (r3 < r3Count - 1)
            //{
            //    for (; r3 < r3 + 3; r3++)
            //    {
            //        if (r3Count <= r3)
            //        {
            //            break;
            //        }
            //    }

            //    for (; u < u + 1; u++)
            //    {

            //    }

            //    for (; r3 < r3 + 4; r3++)
            //    {

            //    }
            //}

        }
    }
}
