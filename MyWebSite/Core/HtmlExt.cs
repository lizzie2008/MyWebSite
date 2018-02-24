using System.Text.RegularExpressions;

namespace MyWebSite.Core
{
    public static class HtmlExt
    {
        public static string StripHTML(this string strHtml, int count = 160)
        {
            string[] aryReg ={
                @"<script[^>]*?>.*?</script>",

                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                @"([\r\n])[\s]+",
                @"&(quot|#34);",
                @"&(amp|#38);",
                @"&(lt|#60);",
                @"&(gt|#62);",
                @"&(nbsp|#160);",
                @"&(iexcl|#161);",
                @"&(cent|#162);",
                @"&(pound|#163);",
                @"&(copy|#169);",
                @"&#(\d+);",
                @"-->",
                @"<!--.*\n"

            };

            string[] aryRep = {
                "",
                "",
                "",
                "\"",
                "&",
                "<",
                ">",
                " ",
                "\xa1",//chr(161),
                "\xa2",//chr(162),
                "\xa3",//chr(163),
                "\xa9",//chr(169),
                "",
                "\r\n",
                ""
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput.Length > count ? strOutput.Substring(0, count) : strOutput;
        }
    }
}
