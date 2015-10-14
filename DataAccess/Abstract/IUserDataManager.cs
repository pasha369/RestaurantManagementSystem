using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace DataAccess.Abstract
{
    public interface IUserDataManager
    {
        List<UserInfo> GetAllUSers();
        void AddUser(UserInfo user);
        bool isValid(string login, string password);
    }
}
