using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RMS.Reservation.Models.ViewModel
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