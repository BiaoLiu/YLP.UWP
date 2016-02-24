using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Core;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Models;
using YLP.UWP.Test;
using YLP.UWP.ViewModels;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace YLP.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; set; }

        public MainPage()
        {
            ViewModel = new MainViewModel();

            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {


        }
    }

    public class MainDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate R2ArtilceTemplate { get; set; }
        public DataTemplate R3ArticleTemplate { get; set; }
        public DataTemplate UArticleTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var article = item as MainModelBase;
            if (article == null)
            {
                return null;
            }

            //R3区域文章
            if (article.region == RegionType.R3.ToString())
            {
                return R3ArticleTemplate;
            }
            //R2区域文章
            if (article.region == RegionType.R2.ToString())
            {
                return R2ArtilceTemplate;
            }
            //用户作品
            return UArticleTemplate;
        }
    }
}
