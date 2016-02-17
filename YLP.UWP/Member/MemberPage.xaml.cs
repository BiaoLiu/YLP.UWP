using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Services;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP.Member
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MemberPage : Page
    {
        public MemberPage()
        {
            this.InitializeComponent();
        }

        async private void UpdateInfoAll_OnClick(object sender, RoutedEventArgs e)
        {
            this.progressRing.IsActive = true;
            var api = new MemberService();

            this.progressRing.IsActive = false;
            await new MessageDialog($"成功更新条").ShowAsync();
        }

        private void BackHome_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (RegisterPage));
        }
    }
}
