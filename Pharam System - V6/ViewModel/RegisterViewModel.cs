using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Pharam_System___V6.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "This not the same password")]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterListViewModel
    {
        public List<IdentityUser> Users { get; set; }
    }

    public class RegisterEditViewModel
    {

        public string Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class RegisterLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

