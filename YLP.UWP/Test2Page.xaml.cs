using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;
using YLP.UWP.ViewModels;
using VisualTreeHelper = YLP.UWP.Helpers.VisualTreeHelper;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Test2Page : Page
    {
        public UArticleViewModel ViewModel { get; set; }

        public Test2Page()
        {
            ViewModel = new UArticleViewModel("", "");

            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// 点赞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnGoods_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedItem = VisualTreeHelper.FindVisualParent<ListViewItem>(btn);

            var uarticle = selectedItem.Content as UArticle;
            if (uarticle?.goodstatus != false)
            {
                return;
            }
            //点赞数加1
            uarticle.goods += 1;
            uarticle.goodstatus = true;

            var api = new CommonService();
            await api.CreateUserAction(uarticle.articleid, UserAction.task.ToString(), UserActionType.goods.ToString());
        }

        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnComment_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedItem = VisualTreeHelper.FindVisualParent<ListViewItem>(btn);

            var uarticle = selectedItem.Content as UArticle;
            Frame.Navigate(typeof(CommentPage), new { uarticle?.articleid, type = UserAction.task.ToString() });
        }

        private void ListViewBase_OnItemClick(object sender, ItemClickEventArgs e)
        {
           var uarticle= e.ClickedItem as UArticle;
        }
    }
}
