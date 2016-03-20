using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete
{
    public class ReservationManager : IDataManager<Reservation>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Reservation item)
        {
            var reservation = _ctx.Reservations.FirstOrDefault(r => r.Id == item.Id);

            _ctx.Reservations.Remove(reservation);
            _ctx.SaveChanges();
        }

        public void Add(Reservation item)
        {
            
            _ctx.Reservations.Add(item);

            _ctx.SaveChanges();
           
        }

        public void Update(Reservation item)
        {
            var reservation = _ctx.Reservations.FirstOrDefault(r => r.Id == item.Id);
            reservation.Status = item.Status;

            _ctx.Entry(reservation).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Reservation GetById(Reservation item)
        {
            throw new NotImplementedException();
        }

        public Reservation Get(int id)
        {
            return _ctx.Reservations.FirstOrDefault(r => r.Id == id);
        }

        public List<Reservation> Get()
        {
            return _ctx.Reservations.Include(x => x.Table.Restaurant).ToList();
        }

    }
}
