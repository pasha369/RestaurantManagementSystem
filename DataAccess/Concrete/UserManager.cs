using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;
using Microsoft.VisualBasic.ApplicationServices;

namespace DataAccess.Concrete
{
    public class UserManager : IDataManager<UserInfo>
    {
        private static RestorauntDbContext _ctx = RestorauntDbContext.context;

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

        public UserInfo GetById(UserInfo item)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Id == item.Id);
        }

        public List<UserInfo> GetAll()
        {
                return _ctx.UserInfos.ToList();                
        }

        public List<UserInfo> GetAllApproved()
        {
            return _ctx.UserInfos.Where(u => u.IsBanned == false).ToList();
        }

        public UserInfo GetById(int Id)
        {
            return _ctx.UserInfos.FirstOrDefault(u => u.Id == Id);
        }
    }
}
