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
    public class ReservationManager : IDataManager<Reservation>
    {
        private RestorauntDbContext _ctx =  RestorauntDbContext.context;

        public void Delete(Reservation item)
        {
            throw new NotImplementedException();
        }

        public void Add(Reservation item)
        {
            _ctx.Reservations.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Reservation item)
        {
            throw new NotImplementedException();
        }

        public Reservation GetById(Reservation item)
        {
            throw new NotImplementedException();
        }
        public Reservation GetById(int id)
        {
            return _ctx.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> GetAll()
        {
            return _ctx.Reservations.ToList();
        }
    }
}
