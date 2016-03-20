using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Order
{
    public class ReceiptManager: IDataManager<Receipt>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Receipt item)
        {
            throw new NotImplementedException();
        }

        public void Add(Receipt item)
        {
            _ctx.Receipts.Add(item);
            _ctx.SaveChanges();
        }

        public void Update(Receipt item)
        {
            _ctx.Entry(item).State = EntityState.Modified;
            _ctx.SaveChanges();
        }

        public Receipt GetById(Receipt item)
        {
            throw new NotImplementedException();
        }

        public Receipt Get(int Id)
        {
            return _ctx.Receipts.Find(Id);
        }

        public List<Receipt> Get()
        {
            return _ctx.Receipts
                .Include(x => x.Table)
                .Include(x => x.ClientOrders)
                .ToList();
        }
    }
}
