using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyWebSite.Core
{
    public static class JsonExt
    {
        /// <summary>
        /// 对象序列化json格式，忽略嵌套
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static JsonResult ToJsonResult(this object obj)
        {
           return new JsonResult(obj, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
