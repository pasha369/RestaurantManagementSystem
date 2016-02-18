using System.ComponentModel.DataAnnotations;

namespace RMS.Client.Models.View
{
    public class RegisterModel
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}