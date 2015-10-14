using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;

namespace DataAccess.Abstract
{
    public interface IAuthRepository
    {
        UserInfo Login(string login, string password);

    }
}
