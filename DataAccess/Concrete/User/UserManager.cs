using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.User
{
    public class UserManager : IDataManager<UserInfo>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(UserInfo item)
        {
            var user = _ctx.UserInfos.FirstOrDefault(u => u.Id == item.Id);
            _ctx.UserInfos.Remove(user);
            _ctx.Entry(user).State = EntityState.Deleted;
            _ctx.SaveChanges();
        }

        public void Add(UserInfo item)
        {
            _ctx.UserInfos.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(UserInfo item)
        {
            var user = _ctx.UserInfos.FirstOrDefault(u => u.Id == item.Id);
            user.Name = item.Name;
            user.Password = item.Password;
            user.Position = item.Position;
            _ctx.Entry(user).State = EntityState.Modified;
            _ctx.SaveChanges();

        }

        public List<UserInfo> GetAllApproved()
        {
            return _ctx.UserInfos.Where(u => u.IsBanned == false).ToList();
        }

        public UserInfo Get(int Id)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Id == Id);
        }

        public IQueryable<UserInfo> Get()
        {
            return _ctx.UserInfos;
        }

        public UserInfo GetUserByLogin(string login)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Login == login);
        }
    }
}
