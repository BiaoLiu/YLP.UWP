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

            //    int r3=0;
            //    int u=0;
            //    for (int i = 0; i < ViewModel.R2Articles.Count; i++)
            //    {
            //        for (; r3 <r3+3; r3++)
            //        {
            //            if (ViewModel.R3Aritcles.Count <= r3)
            //            {
            //                break;
            //            }
            //        }

            //        //输出i

            //        for (; u < u+1; u++)
            //        {

            //        }

            //        for (; r3 < r3+4; r3++)
            //        {

            //        }
            //    }

            //    if (r3 < ViewModel.R3Aritcles.Count - 1)
            //    {
            //        for (; r3 < r3 + 3; r3++)
            //        {
            //            if (ViewModel.R3Aritcles.Count <= r3)
            //            {
            //                break;
            //            }
            //        }

            //        for (; u < u + 1; u++)
            //        {

            //        }

            //        for (; r3 < r3 + 4; r3++)
            //        {

            //        }
            //    }

            //    if (e.NavigationMode == NavigationMode.New)
            //    {
            //        this.FramePage.Navigate(typeof(Login));

            //        base.OnNavigatedTo(e);
            //    }
        }
    }

    public class MainDataTemplateSelector : DataTemplateSelector
    {
        private int r3 = 0;
        public DataTemplate R2ArtilceTemplate { get; set; }

        public DataTemplate R3ArticleTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var article = item as ArticleV2;

            var listViewItem = container as ListViewItem;

            var listView = YLP.UWP.Helpers.VisualTreeHelper.FindVisualParent<ListView>(listViewItem);

            var articles =( listView.ItemsSource as IncrementalLoading<ArticleV2>).ToList();

           

            if (article.region == "R3")
            {
                return R3ArticleTemplate;
            }

            return R2ArtilceTemplate;
        }
    }
}
