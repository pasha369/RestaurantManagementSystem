using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using DataModel.Contexts;
using DataModel.Model;

namespace DataAccess.Concrete.Menu
{
    /// <summary>
    /// Represents data manager for orders.
    /// </summary>
    public class OrderManager : IDataManager<Order>
    {
        private RestorauntDbContext _ctx = new ContextManager().Context;

        public void Delete(Order item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Add order to orders.
        /// </summary>
        /// <param name="item">Order item</param>
        public void Add(Order item)
        {
            _ctx.Orders.Add(item);
        }

        public void Update(Order item)
        {
            throw new NotImplementedException();
        }

        public Order GetById(Order item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get order by id.
        /// </summary>
        /// <param name="Id">Order id</param>
        /// <returns>Order data</returns>
        public Order Get(int Id)
        {
            return _ctx.Orders.Find(Id);
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>Order list</returns>
        public List<Order> Get()
        {
            return _ctx.Orders.ToList();
        }
    }
}
