using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
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
            this.InitializeComponent();
        }

        private async void CallbackProcess()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                this.RichTextBlock.Blocks.Add(GetRichText());
            });
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var articleId = e.Parameter?.ToString();

            if (articleId == null)
            {
                return;
            }

            ViewModel = new ArticleViewModel(articleId);
            ViewModel.Callback = CallbackProcess;
        }

        private Paragraph GetRichText()
        {
            var richText = new StringBuilder();

            mutilmediaLabel label;
            mutilmediaImage image;

            foreach (var item in ViewModel.Items)
            {
                if (item.GetType() == typeof(mutilmediaLabel))
                {
                    label = (mutilmediaLabel)item;
                    richText.Append($"<Run Text = \"{label.text}\" Foreground=\"Black\" FontSize=\"{label.size}\"/>");
                }
                if (item.GetType() == typeof (mutilmediaImage))
                {
                    image = (mutilmediaImage) item;
                    richText.Append( $"<InlineUIContainer><Image Source = \"{image.url}\"/></InlineUIContainer> ");
                }
            }

            //richText = richText.Replace("\r\n", "<LineBreak/>"); //将换行符转换成<LineBreak/>,用于实现换行。

            //生成xaml
            var xaml = string.Format(@"<Paragraph 
                                        xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""
                                        xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
                                    <Paragraph.Inlines>
                                    <Run></Run>
                                      {0}
                                    </Paragraph.Inlines>
                                </Paragraph>", richText.ToString());
            var p = (Paragraph)XamlReader.Load(xaml);
            p.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            return p;
        }
    }

    public class ArticleInfoDataTemplate : DataTemplateSelector
    {
        public DataTemplate TextTemplate { get; set; }

        public DataTemplate ImageTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {

            if (item.GetType() == typeof(mutilmediaLabel))
            {
                return TextTemplate;
            }

            return ImageTemplate;
        }
    }
}
