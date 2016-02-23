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

        public async Task<OperationResult<List<Comment>>> GetComments(string articleid, string type, string sort,int pageIndex,int pageSize)
        {
            FormData.Clear();

            FormData["articleid"] = articleid;
            FormData["userid"] = LocalSetting.Current.GetValue<string>("userid"); ;
            FormData["sid"] = LocalSetting.Current.GetValue<string>("sessionid"); ;
            FormData["type"] = type;
            FormData["sort"] = sort;
            FormData["deviceid"] = DeviceHelper.GetDeviceId().ToString();
            FormData["pindex"] = pageIndex.ToString();
            FormData["psize"] = pageSize.ToString();

            var result = new OperationResult<List<Comment>>();

            var response = await GetResponse(ServiceURL.Comment_CommentList);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedValue("data");

                result.Data = JsonConvert.DeserializeObject<List<Comment>>(data.ToString());
            }

            return result;
        }

        public async Task<OperationResult<string>> CreateComment(Comment comment)
        {
            FormData.Clear();

            FormData["userid"] = comment.userid;
            FormData["sid"] = comment.sid;
            FormData["deviceid"] = comment.deviceid;
            FormData["type"] = comment.type;
            FormData["articleid"] = comment.articleid;
            FormData["comment"] = comment.comment;
            //FormData["atuserid"] = comment.atuserid;
            //FormData["atcommentid"] = comment.atcommentid;
            //FormData["commentstyle"] = comment.commentstyle;
            //FormData["floor"] = comment.floor.ToString();

            var result = new OperationResult<string>();

            var response = await GetResponse(ServiceURL.Comment_CreateComment);
            result.Retcode = response?.GetNamedString("retcode");

            if (response != null && result.Retcode?.CheckSuccess() == true)
            {
                var data = response.GetNamedObject("data");

                result.Data = data.GetNamedString("commentid");
            }

            return result;
        }
    }
}
