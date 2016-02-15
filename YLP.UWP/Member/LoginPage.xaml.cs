﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using YLP.UWP.Common;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Models;
using YLP.UWP.Core.Services;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace YLP.UWP.Member
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        async private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPwd.Password))
            {
                await new MessageDialog("用户名、密码不能为空").ShowAsync();
                return;
            }

            var api = new MemberService();
            var result = await api.Login(txtName.Text, txtPwd.Password);

            if (result.Success)
            {
                await new MessageDialog("登录成功").ShowAsync();
            }
            else
            {
                await new MessageDialog(result.Msg).ShowAsync();
            }
        }

        private void UpdateInfo_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MemberPage));
        }

        private void BackHome_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }
    }
}
