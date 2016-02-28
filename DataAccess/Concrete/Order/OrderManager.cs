using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;

namespace DataAccess.Concrete.Order
{
    /// <summary>
    /// Represents data manager for orders.
    /// </summary>
    public class OrderManager : IDataManager<DataModel.Model.Order>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(DataModel.Model.Order item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add order to orders.
        /// </summary>
        /// <param name="item">Order item</param>
        public void Add(DataModel.Model.Order item)
        {
            _ctx.Orders.Add(item);
        }

        public void Update(DataModel.Model.Order item)
        {
            throw new NotImplementedException();
        }

        public DataModel.Model.Order GetById(DataModel.Model.Order item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="Id">Order id</param>
        /// <returns>Order data</returns>
        public DataModel.Model.Order Get(int Id)
        {
            return _ctx.Orders.Find(Id);
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>Order list</returns>
        public List<DataModel.Model.Order> Get()
        {
            return _ctx.Orders.ToList();
        }
    }
}
