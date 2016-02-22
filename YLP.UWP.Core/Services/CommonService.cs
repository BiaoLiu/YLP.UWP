using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Utilities;

namespace YLP.UWP.Core.Services
{
    public class CommonService : APIBaseService
    {
        public async Task<OperationResult> CreateUserAction(string relationId, string type, string action)
        {
            FormData.Clear();

            FormData["userid"] = LocalSetting.Current.GetValue<string>("userid"); ;
            FormData["sid"] = LocalSetting.Current.GetValue<string>("sessionid"); ;
            FormData["relationid"] = relationId;
            FormData["type"] = type;
            FormData["action"] = action;
            FormData["deviceid"] = DeviceHelper.GetDeviceId().ToString();

            var response = await GetResponse(ServiceURL.Common_CreateUserAction);
            var result = new OperationResult()
            {
                Retcode = response?.GetNamedString("retcode")
            };

            return result;
        }
    }
}
