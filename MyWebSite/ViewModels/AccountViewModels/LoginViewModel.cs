using System.ComponentModel.DataAnnotations;

namespace MyWebSite.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "请输入邮箱")]
        [EmailAddress(ErrorMessage = "邮箱格式非法")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}
