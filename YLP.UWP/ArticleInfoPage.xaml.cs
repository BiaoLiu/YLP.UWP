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
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;
using YLP.UWP.ViewModels;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ArticleInfoPage : Page
    {
        public ArticleViewModel ViewModel { get; set; }

        public ArticleInfoPage()
        {
            ViewModel = new ArticleViewModel("f3e1738c-805c-43e4-b5ee-a59500eb36e2");

            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }

    public class ArticleInfoDataTemplate : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }

        public DataTemplate ImageTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var text = item as mutilmediaLabel;

            if (string.IsNullOrEmpty(text.url))
            {
                return TextTemplate;
            }

            return ImageTemplate;
        }
    }
}
