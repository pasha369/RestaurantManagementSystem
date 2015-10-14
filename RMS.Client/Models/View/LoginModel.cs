using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMS.Client.Models.View
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        [DisplayName("Login")]
        [StringLength(50)]
        public string Username { set; get; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        public string Password { set; get; }
    }
}