using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RMS.Admin.Core.Abstract
{
    public interface ISecurityService
    {
        bool Login(string login, string password, bool rememberMe = false);
        void Logout();

        void RefreshPrincipal();
    }
}