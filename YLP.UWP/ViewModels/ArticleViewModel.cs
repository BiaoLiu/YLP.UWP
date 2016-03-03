using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.ViewModels
{
    public delegate void CallbackDegate();

    public class ArticleViewModel : BindableBase
    {
        public CallbackDegate Callback { get; set; }

        private readonly string _articleId;
        public ArticleViewModel(string articleId)
        {
            _articleId = articleId;

            Initiallize();
        }

        public async void Initiallize()
        {
            var api = new ArticleService();

            Article = new Article();

            var result = await api.GetArticleInfo(_articleId);

            Article = result.Data;

            Items = Article.mutilmedias.ToList();

            Callback?.Invoke();
        }

        private Article _article;

        public Article Article
        {
            get { return _article; }
            set { SetProperty(ref _article, value); }
        }

        public List<object> Items { get; set; }
    }
}
