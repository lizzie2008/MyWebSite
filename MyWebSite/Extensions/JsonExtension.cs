using System;
using Newtonsoft.Json;
using System.IO;

namespace MyWebSite.Extensions
{
    public static class JsonExtension
    {
        //格式化json字符串  
        public static string ToJsonString(this string str)
        {
            try
            {
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }
                else
                {
                    return str;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"格式化Json字符串出错：{e.Message}");
            }
        }
    }
}
