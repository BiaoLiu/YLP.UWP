﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace YLP.UWP.Core.Https
{
    public class HttpRequestClient
    {
        public async Task<string> SendPostFileRequest(string url, IEnumerable<KeyValuePair<string, string>> formData,
            IEnumerable<KeyValuePair<string, byte[]>> fileData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded")); //设定要响应的数据格式
                using (var content = new MultipartFormDataContent()) //表明是通过multipart/form-data的方式上传数据
                {
                    var formDatas = this.GetFormDataByteArrayContent(formData); //获取键值集合对应的ByteArrayContent集合
                    var files = this.GetFileByteArrayContent(fileData); //获取文件集合对应的ByteArrayContent集合
                    Action<List<ByteArrayContent>> act = (dataContents) =>
                     {
                         //声明一个委托，该委托的作用就是将ByteArrayContent集合加入到MultipartFormDataContent中
                         foreach (var byteArrayContent in dataContents)
                         {
                             content.Add(byteArrayContent);
                         }
                     };

                    act(formDatas);
                    act(files);

                    try
                    {
                        var response = await client.PostAsync(url, content); //post请求
                        response.EnsureSuccessStatusCode();

                        var bytes = await response.Content.ReadAsByteArrayAsync();
                        return Encoding.UTF8.GetString(bytes);
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// 获取文件集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        private List<ByteArrayContent> GetFileByteArrayContent(IEnumerable<KeyValuePair<string, byte[]>> files)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var item in files)
            {
                var fileContent = new ByteArrayContent(item.Value);
                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = item.Key
                };
                list.Add(fileContent);
            }
            return list;
        }

        /// <summary>
        /// 获取键值集合对应的ByteArrayContent集合
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private List<ByteArrayContent> GetFormDataByteArrayContent(IEnumerable<KeyValuePair<string, string>> collection)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();
            foreach (var item in collection)
            {
                var dataContent = new ByteArrayContent(Encoding.UTF8.GetBytes(item.Value));
                dataContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    Name = item.Key
                };
                list.Add(dataContent);
            }
            return list;
        }
    }
}
