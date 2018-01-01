using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebSite.Datas.Config
{
    public class MyHeader
    {
        public header header { set; get; }
        public Body Body { set; get; }
    }

    public class header
    {
        public string account_type { set; get; }
        public string password { set; get; }
        public string token { set; get; }
        public string username { set; get; }
    }
    public class Body
    {

    }
}
