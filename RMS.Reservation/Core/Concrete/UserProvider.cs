using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using DataAccess.Abstract;
using DataModel.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace RMS.Reservation.Core.Concrete
{
    /*
    public class UserProvider : IPrincipal
    {
        public bool IsInRole(string role)
        {
            if(UserIdentity.User == null)
            {
                return false;
            }
            return UserIdentity.User.InRoles(roles);
        }

        public UserIdentity UserIdentity { get; private set; }

        public UserProvider(string name, IAuthRepository repository)
        {
            UserIdentity = new UserIdentity();
            UserIdentity.Init(name, repository);
        }
    }

    public class UserIdentity : IIdentity
    {
        public User User ;

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
     * */
}