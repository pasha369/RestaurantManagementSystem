using System;
using System.Web.UI.WebControls;
using RMS.Admin.Core.Abstract;
using RMS.Admin.Core.Concrete;

namespace RMS.Admin.Login
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ctrLogin_OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            string username = ctrLogin.UserName;
            string pass = ctrLogin.Password;

            bool rememberMe = ctrLogin.RememberMeSet;

            ISecurityService securityService = new FormsSecurityService();
            e.Authenticated = securityService.Login(username, pass, rememberMe);
        }

    }
}