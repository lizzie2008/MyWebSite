using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyWebSite.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(256)]
        public string NickName { get; set; }
    }
}
