using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
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
