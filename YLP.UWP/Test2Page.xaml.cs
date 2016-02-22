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
using YLP.UWP.Core.Services;
using YLP.UWP.Core.ViewModels;
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
            ViewModel = new UArticleViewModel("", UArticleType.latest.ToString(), "");

            this.InitializeComponent();
        }

        private async void BtnGoods_OnClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var selectedItem =VisualTreeHelper.FindVisualParent<ListViewItem>(btn);

            var uArticleId = VisualTreeHelper.FindVisualChildByName<TextBlock>(selectedItem, "UArticleId").Text;

            var api=new CommonService();

           var result= await api.CreateUserAction(uArticleId, UserAction.task.ToString(), UserActionType.goods.ToString());
            if (result.Success)
            {
                
            }
        }
    }

    public class People
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }
    }
}
