using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using YLP.UWP.Core;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.ViewModels
{
    public class MainViewModel : BindableBase
    {
        private readonly MainService _api = new MainService();

        /// <summary>
        /// 轮播图
        /// </summary>
        public ObservableCollection<SlideArticle> SlideArticles { get; set; }

        public IncrementalLoading<MainModelBase> Articles { get; set; }

        public IncrementalLoading<Subject> Subjects { get; set; }

        public MainViewModel()
        {
            SlideArticles = new ObservableCollection<SlideArticle>();

            Articles = new IncrementalLoading<MainModelBase>(new int[] { 35, 5, 5 }, (p, s) => _api.GetR3ArticleList(p, s),
                (p, s) => _api.GetR2ArticleList(p, s),
                (p, s) => _api.GetUArticleList(p, s));
            //结果处理
            Articles.ResultProcessDelegate = ResultProcess;

            Subjects = new IncrementalLoading<Subject>((p, s) => _api.GetSubjectList(null, null, null, SortType.latest.ToString(), p, s));

            Initialize();
        }

        public async void Initialize()
        {
            //获取轮播区域文章列表
            var result = await _api.GetSlideArticles();

            result.Data?.ForEach(SlideArticles.Add);
        }

        public void ResultProcess(List<MainModelBase> articles)
        {
            var r2Article = articles.Where(a => a.region == RegionType.R2.ToString()).ToList();
            var r2Count = r2Article.Count();

            var r3Article = articles.Where(a => a.region == RegionType.R3.ToString()).ToList();
            var r3Count = r3Article.Count();

            var uArticle = articles.Where(a => a.region == RegionType.R4.ToString()).ToList();
            var uCount = uArticle.Count;

            articles.Clear();

            int r3 = 0;
            int u = 0;
            for (int i = 0; i < r2Count; i++)
            {
                //三张单图
                var index = r3;
                for (; r3 < index + 3; r3++)
                {
                    if (r3Count <= r3)
                    {
                        break;
                    }
                    articles.Add(r3Article[r3]);
                }

                //一张多图
                articles.Add(r2Article[i]);

                //四张单图
                index = r3;
                for (; r3 < index + 4; r3++)
                {
                    if (r3Count <= r3)
                    {
                        break;
                    }
                    articles.Add(r3Article[r3]);
                }

                //一张用户作品
                if (u < uCount)
                {
                    articles.Add(uArticle[u++]);
                }
            }

            //超出比例的 用户作品
            for (; u < uCount - 1; u++)
            {
                //三张单图
                var index = r3;
                for (; r3 < index + 3; r3++)
                {
                    if (r3Count <= r3)
                    {
                        break;
                    }
                    articles.Add(r3Article[r3]);
                }

                //四张单图
                index = r3;
                for (; r3 < index + 4; r3++)
                {
                    if (r3Count <= r3)
                    {
                        break;
                    }
                    articles.Add(r3Article[r3]);
                }

                //一张用户作品
                articles.Add(uArticle[u]);
            }

            //超出比例的 R3区域文章
            for (; r3 < r3Count - 1; r3++)
            {
                articles.Add(r3Article[r3]);
            }
        }
    }
}
