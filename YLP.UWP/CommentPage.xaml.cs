using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
    public sealed partial class CommentPage : Page
    {
        private string _type;
        public CommentViewModel ViewModel { get; set; }

        public CommentPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var parameterType = e.Parameter?.GetType();
            if (parameterType == null)
            {
                return;
            }

            var articleid = parameterType.GetProperty("articleid").GetValue(e.Parameter).ToString();
            _type = parameterType.GetProperty("type").GetValue(e.Parameter).ToString();

            ViewModel = new CommentViewModel(articleid, _type);
        }

        /// <summary>
        /// 评论点赞
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnCommentGoods_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedItem = VisualTreeHelper.FindVisualParent<ListViewItem>(btn);

            var comment = selectedItem.Content as Comment;
            if (comment?.goodstatus != false)
            {
                return;
            }
            //点赞数加1
            comment.commentgoods += 1;
            comment.goodstatus = true;

            var api = new CommonService();
            await api.CreateUserAction(comment.commentid, UserAction.task.ToString(), UserActionType.commentgoods.ToString());
        }
    }
}
