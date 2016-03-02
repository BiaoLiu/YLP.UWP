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
    public class ArticleViewModel : BindableBase
    {
        private string _articleId;
        public ArticleViewModel(string articleId)
        {
            _articleId = articleId;

            Update();
        }

        public async void Update()
        {
            var api = new ArticleService();

            var result = await api.GetArticleInfo(_articleId);

            Article= result.Data;

            Items = new ObservableCollection<mutilmediaLabel>();
            foreach (var item in result.Data.mutilmedia.Items)
            {
                Items.Add(item as mutilmediaLabel);
            }
        }

        private Article _article;

        public Article Article
        {
            get { return _article; }
            set { SetProperty(ref _article, value); }
        }

        public ObservableCollection<mutilmediaLabel> Items { get; set; } 
    }
}
