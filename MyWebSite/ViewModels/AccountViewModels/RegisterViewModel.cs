using System.ComponentModel.DataAnnotations;
using MyWebSite.Extensions;

namespace MyWebSite.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "请输入邮箱")]
        [EmailAddress(ErrorMessage = "邮箱格式非法")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100, ErrorMessage = "密码长度必须介于 {2} 和 {1} 之间", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "两次输入密码不一致")]
        public string ConfirmPassword { get; set; }

        [MustBeTrue(ErrorMessage = "请勾选阅读并接受用户协议")]
        public bool IsAgree { get; set; }
    }
}
