using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Services
{
    public class MainService : APIBaseService
    {
        /// <summary>
        /// 获取轮播区域列表
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<SlideArticle>>> GetSlideArticles()
        {
            FormData.Clear();

            var result = new OperationResult<List<SlideArticle>>();

            var response = await GetResponse(ServiceURL.Main_SlideArticleList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                result.Data = JsonConvert.DeserializeObject<List<SlideArticle>>(data.ToString());
            }

            return result;
        }
    }
}
