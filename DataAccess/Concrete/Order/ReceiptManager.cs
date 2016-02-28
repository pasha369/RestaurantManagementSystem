using System;
using System.Collections.Generic;
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
        }

        public void Update(Receipt item)
        {
            throw new NotImplementedException();
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
            return _ctx.Receipts.ToList();
        }
    }
}
