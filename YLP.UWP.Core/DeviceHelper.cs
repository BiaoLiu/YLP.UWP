using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core
{
    public class DeviceHelper
    {
        /// <summary>
        /// 获取设备唯一Id
        /// </summary>
        /// <returns></returns>
        public static Guid GetDeviceId()
        {
            var easc = new Windows.Security.ExchangeActiveSyncProvisioning.EasClientDeviceInformation();

            return easc.Id;
        }
    }
}
