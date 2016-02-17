using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using YLP.UWP.Core.Extensions;

namespace YLP.UWP.Core.Https
{
    public class APIBaseService
    {
        //版本号
        private const string AppVersion = "20001a";
        //应用ID
        private const string AppId = "1dce319b3a2ca219406de274767772cb";
        public Dictionary<string, string> FormData = new Dictionary<string, string>();

        /// <summary>
        /// 向服务器发送GET请求 返回JSON格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<JsonObject> GetResponseSendGetRequest(string url)
        {
            var response = await BaseService.SendGetRequestAsync(url);
            if (response != null)
            {
                return JsonObject.Parse(response);
            }

            return null;
        }

        /// <summary>
        /// 向服务器发送POST请求 返回JSON格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<JsonObject> GetResponse(string url)
        {
            //构建请求参数字典
            GenerateRequestParams();

            var response = await BaseService.SendPostRequestAsync(url, FormData);
            if (response != null)
            {
                return JsonObject.Parse(response);
            }

            return null;
        }

        /// <summary>
        /// 向服务器发送POST请求 返回JSON格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public async Task<JsonObject> GetResponse(string url, IEnumerable<KeyValuePair<string, byte[]>> fileData)
        {
            //构建请求参数字典
            GenerateRequestParams();

            foreach (var item in FormData.Keys)
            {
                FormData["\"" + item + "\""] = FormData[item];
                FormData.Remove(item);
            }

            var response = await BaseService.SendPostFileRequestAsync(url, FormData, fileData);
            if (response != null)
            {
                var jsonData = JsonObject.Parse(response);

                return jsonData;
            }

            return null;
        }

        /// <summary>
        /// 向服务器发送GET请求 返回HTML格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetStringResponseSendGetRequest(string url)
        {
            return await BaseService.SendGetRequestAsync(url);
        }

        /// <summary>
        ///     向服务器发送GET请求 返回HTML格式数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<string> GetStringResponse(string url)
        {
            //构建请求参数字典
            GenerateRequestParams();

            return await BaseService.SendPostRequestAsync(url, FormData);
        }

        /// <summary>
        /// 向服务器发送请求 获取图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<WriteableBitmap> GetImageResponse(string url)
        {
            try
            {
                var buffer = await BaseService.SendGetRequestAsBytesAsync(url);
                if (buffer != null)
                {
                    var bitmapImage = new BitmapImage();

                    using (var memoryStream = new InMemoryRandomAccessStream())
                    {
                        var streamWriter = memoryStream.AsStreamForWrite();
                        await streamWriter.WriteAsync(buffer.ToArray(), 0, (int)buffer.Length);

                        await streamWriter.FlushAsync();
                        memoryStream.Seek(0);

                        await bitmapImage.SetSourceAsync(memoryStream);
                        var writeableBitmap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
                        memoryStream.Seek(0);

                        await writeableBitmap.SetSourceAsync(memoryStream);

                        return writeableBitmap;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 构建请求参数字典
        /// </summary>
        private void GenerateRequestParams()
        {
            //版本号
            FormData["ver"] = AppVersion;
            //字典序
            var sortValue = FormData.Where(d => d.Key != "ts" && d.Key != "sign").OrderBy(d => d.Value.ToString())
                .Select(d => d.Value.ToString())
                .Aggregate((a, b) => a + b);

            var appId = AppId;
            var strConnect = "&YLB&";
            //时间戳
            var ts = (DateTime.Now - DateTime.Parse("2015-01-01")).TotalMilliseconds.ToString();
            //参数签名
            var sign = EncryptHelper.MD5Encrypt(appId + strConnect + ts + strConnect + sortValue);

            FormData["ts"] = ts;
            FormData["sign"] = sign;
        }
    }
}