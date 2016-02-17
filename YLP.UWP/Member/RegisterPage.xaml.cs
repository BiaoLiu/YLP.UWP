using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Perception.Spatial;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP.Member
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        async private void Register_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPwd.Password))
            {
                await new MessageDialog("用户名、密码不能为空").ShowAsync();
                return;
            }

            var api = new MemberService();
            var result = await api.Register(txtName.Text, txtPwd.Password);
            if (result.Success)
            {
                await new MessageDialog("注册成功").ShowAsync();
            }
            else
            {
                await new MessageDialog(result.Msg).ShowAsync();
            }
        }

        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void UArticle_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UAritclePage));
        }

        private void AddUArticle_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddUArticlePage));
        }

        private async void UpdateAvatarAll_OnClick(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();

            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".png");

            var storageFile = await openPicker.PickSingleFileAsync();
            if (storageFile == null)
            {
                await new MessageDialog("请选择图片").ShowAsync();
                return;
            }

            this.progressRing.IsActive = true;

            IRandomAccessStreamWithContentType accessStream = await storageFile.OpenReadAsync();
            var fileName = storageFile.Name;

            byte[] content;
            using (Stream stream = accessStream.AsStreamForRead((int)accessStream.Size))
            {
                content = new byte[stream.Length];
                await stream.ReadAsync(content, 0, (int)stream.Length);
            }

            var fileData = new List<KeyValuePair<string, byte[]>>();
            fileData.Add(new KeyValuePair<string, byte[]>(fileName, content));

            var api=new MemberService();
            var result= await api.UpdateAvatar(fileData);

            this.progressRing.IsActive = false;

            if (result.Success)
            {
                await new MessageDialog("更新成功").ShowAsync();
            }
            else
            {
                await new MessageDialog(result.Msg).ShowAsync();
            }
        }
    }
}
