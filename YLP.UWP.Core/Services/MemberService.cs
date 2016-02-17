using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;

namespace YLP.UWP.Core.Services
{
    public class MemberService : APIBaseService
    {
        #region Register 会员注册

        /// <summary>
        /// 会员注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        async public Task<OperationResult> Register(string userName, string password)
        {
            FormData.Clear();

            FormData["username"] = userName;
            FormData["password"] = EncryptHelper.MD5Encrypt(password);
            FormData["deviceid"] = DeviceHelper.GetDeviceId().ToString();

            var response = await GetResponse(ServiceURL.Member_Register);

            var result = new OperationResult();
            result.Retcode = response?.GetNamedString("retcode");
            result.Error = response?.GetNamedString("errmsg");

            return result;
        }

        #endregion

        #region Login 会员登录

        /// <summary>
        /// 会员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        async public Task<OperationResult> Login(string userName, string password)
        {
            FormData.Clear();

            FormData["username"] = userName;
            FormData["password"] = EncryptHelper.MD5Encrypt(password);
            FormData["deviceid"] = DeviceHelper.GetDeviceId().ToString();

            var result = new OperationResult();

            var response = await GetResponse(ServiceURL.Member_Login);
            if (response?.GetNamedString("retcode").CheckSuccess() == true)
            {
                var data = response.GetNamedObject("data");

                LocalSetting.Current.SetValue("userid", data.GetNamedString("userid"));
                LocalSetting.Current.SetValue("sessionid", data.GetNamedString("sid"));
            }

            result.Retcode = response?.GetNamedString("retcode");
            result.Error = response?.GetNamedString("errmsg");

            return result;
        }

        #endregion

        #region UpdateInfo 更新会员信息

        /// <summary>
        /// 更新会员信息
        /// </summary>
        /// <param name="editType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        async public Task<OperationResult> UpdateInfo(string editType, string value)
        {
            FormData.Clear();

            FormData["userid"] = LocalSetting.Current.GetValue<string>("userid");
            FormData["sid"] = LocalSetting.Current.GetValue<string>("sessionid");
            FormData["edittype"] = editType;
            FormData["value"] = value;
            FormData["deviceid"] = DeviceHelper.GetDeviceId().ToString();

            var response = await GetResponse(ServiceURL.Member_UpdateInfo);

            var result = new OperationResult();
            result.Retcode = response?.GetNamedString("retcode");

            return result;
        }

        #endregion

        #region UpdateAvatar 更新会员头像
      
        /// <summary>
        /// 更新会员头像
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public async Task<OperationResult> UpdateAvatar(IEnumerable<KeyValuePair<string, byte[]>> fileData)
        {
            FormData.Clear();

            FormData["userid"] = LocalSetting.Current.GetValue<string>("userid");
            FormData["sid"] = LocalSetting.Current.GetValue<string>("sessionid");

            var result = new OperationResult<string>();

            var response = await GetResponse(ServiceURL.Member_UpdateAvatar, fileData);
            result.Retcode = response?.GetNamedString("retcode");

            return result;
        } 

        #endregion
    }
}
