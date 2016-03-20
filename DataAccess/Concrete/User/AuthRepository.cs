using System;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.User
{
    public class AuthRepository :IAuthRepository, IDisposable
    {
        private RestorauntDbContext _ctx = new RestorauntDbContext();

        public UserInfo Login(string login, string password)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Login == login && u.Password == password);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
