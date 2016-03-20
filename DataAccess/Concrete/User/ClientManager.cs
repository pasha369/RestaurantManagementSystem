using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.User
{
    public class ClientManager : IDataManager<ClientInfo>
    {
        private static RestorauntDbContext _ctx = new RestorauntDbContext();

        public void Delete(ClientInfo item)
        {
            var client = _ctx.ClientInfos.FirstOrDefault(c => c.Id == item.Id);
            _ctx.ClientInfos.Remove(client);
            _ctx.SaveChanges();
        }

        public void Add(ClientInfo item)
        {
            var client = new ClientInfo()
            {
                Restaurant = _ctx.Restoraunts.FirstOrDefault(r => r.Id == item.Restaurant.Id),
                UserInfo = _ctx.UserInfos.FirstOrDefault(u => u.Id == item.UserInfo.Id),
            };

            _ctx.ClientInfos.Add(client);
            _ctx.SaveChanges();
        }

        public void Update(ClientInfo item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ClientInfo Get(int Id)
        {
            return _ctx.ClientInfos.FirstOrDefault(c => c.Id == Id);
        }

        public IQueryable<ClientInfo> Get()
        {
            return _ctx.ClientInfos;
        }
    }
}
