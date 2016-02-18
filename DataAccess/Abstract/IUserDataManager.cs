using System.Collections.Generic;
using DataModel.Model;

namespace DataAccess.Abstract
{
    public interface IUserDataManager
    {
        List<UserInfo> GetAllUSers();
        void AddUser(UserInfo user);
        bool isValid(string login, string password);
    }
}
