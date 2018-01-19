using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Core;
using MyWebSite.Datas.Config.Home;
using MyWebSite.Extensions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebSite.Areas.Tools.Controllers
{
    [Area("Tools")]
    public class ApiSimulatorController : AppController
    {
        private readonly MyRequest _myRequest;
        private readonly PrivateInfo _privateInfo;

        public ApiSimulatorController(IOptions<MyRequest> myRequest, IOptions<PrivateInfo> privateInfo)
        {
            _myRequest = myRequest.Value;
            _privateInfo = privateInfo.Value;

            //格式化Json
            _myRequest.ApiRequests.ToList().ForEach(s =>
            {
                if (s.Methord == "POST")
                    s.ApiDatas = s.ApiDatas.ToJsonString();
            });
        }

        /// <summary>
        /// 获取API请求列表
        /// </summary>
        /// <returns></returns>
        public IActionResult GetApiRequestList()
        {
            return new JsonResult(_myRequest.ApiRequests);
        }

        /// <summary>
        /// 调用API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IActionResult> InvokApi(ApiRequest request)
        {
            var hc = new HttpClient();
            if (request.Methord == "GET")
            {
                var getUrl = request.Url + "?";
                foreach (var para in request.ApiParas)
                {
                    getUrl += "&" + para.ParaName + "=" + para.ParaValue;
                }
                //隐匿隐私信息
                foreach (var valuePair in _privateInfo.InfoDic)
                {
                    getUrl = getUrl.Replace($"<{valuePair.Key}>", valuePair.Value);
                }

                return new JsonResult(new
                {
                    SendContent = getUrl,
                    ReturnResult = (await hc.HttpGetAsync(getUrl)).ToJsonString()
                });
            }
            else if (request.Methord == "POST")
            {
                if (!string.IsNullOrEmpty(request.ApiDatas))
                {
                    //隐匿隐私信息
                    foreach (var valuePair in _privateInfo.InfoDic)
                    {
                        request.ApiDatas = request.ApiDatas.Replace($"<{valuePair.Key}>", valuePair.Value);
                    }
                    return new JsonResult(new
                    {
                        SendContent = request.Url,
                        ReturnResult = (await hc.HttpPostAsync(request.Url, request.ApiDatas)).ToJsonString()
                    });
                }
                else
                {
                    return new BadRequestResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}