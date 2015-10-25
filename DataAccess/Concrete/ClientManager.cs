using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
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
            _ctx.ClientInfos.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(ClientInfo item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public ClientInfo GetById(ClientInfo item)
        {

            throw new NotImplementedException();
        }

        public ClientInfo GetById(int Id)
        {
            return _ctx.ClientInfos.FirstOrDefault(c => c.Id == Id);
        }
        public List<ClientInfo> GetAll()
        {
            return _ctx.ClientInfos.ToList();
        }
        public Restaurant GetByClientId(int Id)
        {
            return _ctx.ClientInfos.FirstOrDefault(c => c.Id == Id).Restaurant;
        }

 
    }
}
