using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config;
using MyWebSite.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyWebSite.Areas.Tools.Controllers
{
    [Area("Tools")]
    public class ApiSimulatorController : AppController
    {
        private readonly MyRequest _myRequest;

        public ApiSimulatorController(IOptions<MyRequest> myRequest)
        {
            _myRequest = myRequest.Value;
        }

        /// <summary>
        /// API模拟主页
        /// </summary>
        /// <param name="selectedApiCode"></param>
        /// <returns></returns>
        public IActionResult Index(string selectedApiCode = null)
        {
            UpdateDropDownList(selectedApiCode);
            if (selectedApiCode == null)
            {
                return View("Index", new ApiRequest());

            }
            var selectedApi = _myRequest.ApiRequests.FirstOrDefault(s => s.ApiCode == selectedApiCode);
            if (selectedApi != null && selectedApi.Methord == "POST")
                selectedApi.ApiDatas = selectedApi.ApiDatas.ToJsonString();
            return View("Index", selectedApi);
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

                ViewBag.SendContent = getUrl;
                ViewBag.ReturnResult = (await hc.HttpGetAsync(getUrl)).ToJsonString();
            }
            else if (request.Methord == "POST")
            {
                if (!string.IsNullOrEmpty(request.ApiDatas))
                {
                    ViewBag.SendContent = request.Url;
                    ViewBag.ReturnResult = (await hc.HttpPostAsync(request.Url, request.ApiDatas)).ToJsonString();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "请输入Json格式请求参数");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "请求方式非法");
            }

            UpdateDropDownList(request.ApiCode);
            return View("Index", request);
        }

        /// <summary>
        /// 初始化下拉选择框
        /// </summary>
        private void UpdateDropDownList(string selectedApiCode = null)
        {
            List<SelectListItem> listApiName = new List<SelectListItem>();
            foreach (var request in _myRequest.ApiRequests.Select(s => new { s.ApiName, s.ApiCode }))
            {
                listApiName.Add(new SelectListItem
                {
                    Value = request.ApiCode,
                    Text = request.ApiName,
                    Selected = request.ApiCode == selectedApiCode
                });
            }
            ViewBag.ApiCodes = listApiName;
        }
    }
}