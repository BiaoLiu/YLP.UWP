using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

namespace YLP.UWP.Core.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainService _api = new MainService();

        private ObservableCollection<SlideArticle> _slideArticles;
        public ObservableCollection<SlideArticle> SlideArticles
        {
            get
            {
                return _slideArticles;
            }
            set
            {
                SetProperty(ref _slideArticles, value);
            }
        }

        public MainViewModel()
        {
            SlideArticles = new ObservableCollection<SlideArticle>();

            Update();
        }

        public async void Update()
        {
            var result = await _api.GetSlideArticles();

            result?.Data.ForEach(SlideArticles.Add);
        }
    }
}
