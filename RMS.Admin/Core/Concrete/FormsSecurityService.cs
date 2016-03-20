using System.Security.Principal;
using System.Web;
using System.Web.Security;
using DataAccess.Abstract;
using DataAccess.Concrete.User;
using RMS.Admin.Core.Abstract;

namespace RMS.Admin.Core.Concrete
{
    public class FormsSecurityService : ISecurityService
    {
        private IUserDataManager userDataManager = new UserDataManager();

        public bool Login(string login, string password, bool rememberMe = false)
        {
            if(!userDataManager.isValid(login, password))
            {
                HttpContext.Current.User = null;
                return false;
            }
            DefinePrincipal(login);
            return true;
        }

        private void DefinePrincipal(string login)
        {
            IIdentity identity = new GenericIdentity(login);
            HttpContext.Current.User = new GenericPrincipal(identity, new []{"Admin"});
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.User = null;
        }

        public void RefreshPrincipal()
        {
            IPrincipal principal = HttpContext.Current.User;
            if(principal != null 
                && principal.Identity != null 
                && principal.Identity.IsAuthenticated)
            {
                DefinePrincipal(principal.Identity.Name);
            }
        }
    }
}