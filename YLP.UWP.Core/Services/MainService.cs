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

        /// <summary>
        /// 获取R3区域文章列表 (单张图)
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<MainModelBase>>> GetR3ArticleList(int pageIndex, int pageSize)
        {
            FormData.Clear();

            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<MainModelBase>>();

            var response = await GetResponse(ServiceURL.Main_R3ArticleList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                result.Data = JsonConvert.DeserializeObject<List<MainModelBase>>(data.ToString());
            }

            return result;
        }

        /// <summary>
        /// 获取R2区域文章列表 (多张图)
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<MainModelBase>>> GetR2ArticleList(int pageIndex, int pageSize)
        {
            FormData.Clear();

            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<MainModelBase>>();

            var response = await GetResponse(ServiceURL.Main_R2ArticleList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                result.Data = JsonConvert.DeserializeObject<List<MainModelBase>>(data.ToString());
            }

            return result;
        }


        /// <summary>
        /// 获取用户作品列表
        /// </summary>
        /// <returns></returns>
        public async Task<OperationResult<List<MainModelBase>>> GetUArticleList(int pageIndex, int pageSize)
        {
            FormData.Clear();

            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<MainModelBase>>();

            var response = await GetResponse(ServiceURL.Main_UArticleList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                var uarticles = JsonConvert.DeserializeObject<List<UArticle>>(data.ToString());

                result.Data = uarticles.Select(u => new MainModelBase(u)).ToList();
            }

            return result;
        }

        /// <summary>
        /// 获取专题列表
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="type"></param>
        /// <param name="categoryId"></param>
        /// <param name="sort"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<OperationResult<List<Subject>>> GetSubjectList(string subjectId, string type, string categoryId, string sort, int pageIndex, int pageSize)
        {
            FormData.Clear();

            FormData["subjectid"] = subjectId ?? "";
            FormData["type"] = type ?? "";
            FormData["categoryid"] = categoryId ?? "";
            FormData["sort"] = sort;
            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<Subject>>();

            var response = await GetResponse(ServiceURL.Subject_GetSubjectList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");
                result.Data = JsonConvert.DeserializeObject<List<Subject>>(data.ToString());
            }

            return result;
        }
    }
}
