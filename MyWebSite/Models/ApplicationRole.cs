using Microsoft.AspNetCore.Identity;

namespace MyWebSite.Models
{
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
