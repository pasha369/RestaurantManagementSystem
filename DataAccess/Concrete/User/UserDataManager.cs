using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.User
{
    public class UserDataManager : IUserDataManager
    {
        static RestorauntDbContext _ctx = new ContextManager().Context;

        public List<UserInfo> GetAllUSers()
        {

            return _ctx.UserInfos.Where(u => u.IsBanned == false).ToList();
        }

        public void AddUser(UserInfo user)
        {

            _ctx.UserInfos.Add(user);
            _ctx.SaveChanges();
        }

        public bool isValid(string login, string password)
        {

            if (login == "avel" && password == "123")
            {
                return true;
            }
            return false;
        }


        public void RemoveFromBlackList(int userId)
        {

            var curUser = _ctx.UserInfos.FirstOrDefault(u => u.Id == userId);
            if (curUser != null)
                curUser.IsBanned = false;
            _ctx.SaveChanges();

        }

        public UserInfo GetById(int id)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Id == id);
        }

        public void SetToBlackList(UserInfo user)
        {
            var curUser = _ctx.UserInfos.FirstOrDefault(u => u.Id == user.Id);
            if (curUser != null)
                curUser.IsBanned = true;
            _ctx.Entry(curUser).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public List<UserInfo> GetBlackList()
        {

            var lstBanUsers = _ctx.UserInfos.Where(u => u.IsBanned);
            return lstBanUsers.ToList();

        }

    }
}
