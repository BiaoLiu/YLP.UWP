using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using YLP.UWP.Core.Common;
using YLP.UWP.Core.Services;

namespace YLP.UWP.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly MemberService _memberService = new MemberService();

        public LoginViewModel()
        {
            LoginCommand = new Command(p =>
            {
                var model = p as LoginViewModel;
                if (model == null)
                {
                    return false;
                }

                return !string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password);
            },
            async p =>
            {
                var model = p as LoginViewModel;
                if (model == null)
                {
                    return;
                }

                var result = await _memberService.Login(model.UserName, model.Password);
                if (result.Success)
                {
                    // 让当前代码在主线程中执行
                    //var rootFrame = Window.Current.Content as Frame;
                    //rootFrame?.Navigate(typeof(MainPage), user);

                    await new MessageDialog("登录成功").ShowAsync();
                }
                else
                {
                    await new MessageDialog(result.Msg).ShowAsync();
                }
            });

            Avatar = "/Assets/YLBLogo.png";
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                SetProperty(ref _userName, value);
                LoginCommand.OnCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                SetProperty(ref _password, value);
                LoginCommand.OnCanExecuteChanged();
            }
        }

        public string Avatar { get; set; }

        public Command LoginCommand { get; set; }
    }
}
