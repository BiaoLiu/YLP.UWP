using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Extensions;
using YLP.UWP.Core.Https;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Services
{
    public class ArticleService : APIBaseService
    {
        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        public async Task<OperationResult<Article>> GetArticleInfo(string articleId)
        {
            FormData.Clear();

            FormData["articleid"] = articleId;

            var result = new OperationResult<Article>();

            var response = await GetResponse(ServiceURL.Article_GetArticleInfo);
            result.Retcode = response?.GetNamedString("retcode");
            try
            {

                if (response != null && result.Retcode?.CheckSuccess() == true)
                {
                    var data = response.GetNamedValue("data");
                    var article = JsonConvert.DeserializeObject<Article>(data.ToString());

                    var buffer = await GetBytesResponse(article.content);
                    using (var memoryStream = new MemoryStream(buffer.ToArray()))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(mutilmedia));
                        article.mutilmedias = ((mutilmedia)serializer.Deserialize(memoryStream)).Items;
                    }

                    result.Data = article;
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return result;
        }
    }
}
