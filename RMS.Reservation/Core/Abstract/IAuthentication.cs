using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using DataModel.Model;

namespace RMS.Reservation.Core.Abstract
{
    public interface IAuthentication
    {
        HttpContext HttpContext { set; get; }
        IPrincipal CurrentUser { get; }


        UserInfo Login(string login, string password, bool rememberMe);
        UserInfo Login(string login);
        void Logout();
        
    }
}