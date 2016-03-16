using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Models.View.MenuModels;
using RMS.Client.Models.View.OrderModels;

namespace RMS.Client.Controllers.WebApi.Order
{
    /// <summary>
    /// Represents order controller.
    /// </summary>
    public class OrderController : ApiController
    {
        private IDishManager _dishManager;
        private IDataManager<DataModel.Model.Order> _orderManager;
        private IDataManager<UserInfo> _userManager;
        private IDataManager<Restaurant> _restaurantManager;
        private IDataManager<Receipt> _receiptManager;
        private IDataManager<DinnerTable> _tableManager;
        private readonly int NEW = 0;

        public OrderController(IDishManager dishManager, IDataManager<DataModel.Model.Order> orderManager, IDataManager<UserInfo> userManager, IDataManager<Restaurant> restaurantManager, IDataManager<Receipt> receiptManager, IDataManager<DinnerTable> tableManager)
        {
            _dishManager = dishManager;
            _orderManager = orderManager;
            _userManager = userManager;
            _restaurantManager = restaurantManager;
            _receiptManager = receiptManager;
            _tableManager = tableManager;
        }

        /// <summary>
        /// Create new order or add dish to existing.
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage MakeOrder(OrderItemModel order)
        {
            var receipt = new Receipt();
            if (order.Id != NEW)
            {
                receipt = _receiptManager.Get(order.Id);
            }

            var clientOrderList = new List<DataModel.Model.Order>();
            foreach (var dish in order.Dishes)
            {
                var orderEntity = new DataModel.Model.Order();
                orderEntity.Dish = _dishManager.Get(dish.Id);
                orderEntity.Restaurant = _restaurantManager.Get(order.RestaurantId);
                orderEntity.Created = DateTime.Now;

                clientOrderList.Add(orderEntity);
            }
            receipt.Client = GetCurrentUser();
            receipt.ClientOrders = clientOrderList;
            receipt.CurrentDateTime = DateTime.Now;
            receipt.Table = _tableManager.Get(order.TableId);
            receipt.ReceiptStatus = ReceiptStatus.Open;

            _receiptManager.Add(receipt);
            return this.Request.CreateResponse(HttpStatusCode.OK, receipt.Id);
        }

        /// <summary>
        /// Get active orders.
        /// </summary>
        /// <returns>Order list</returns>
        [HttpPost]
        public HttpResponseMessage GetActiveOrders()
        {
            var clientManager = new ClientManager();
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var client = clientManager.Get().FirstOrDefault(c => c.UserInfo.Login == login);
            var activeOrders = _receiptManager.Get()
                .Where(x => x.ReceiptStatus == ReceiptStatus.Open && x.Table?.Restaurant.Id == client?.Restaurant.Id)
                .Select(x => new
                {
                    OrderId = x.Id,
                    TableNumber = x.Table != null ? x.Table.Number : 0,
                    Dishes = x.ClientOrders.Select(d => d != null ? new
                    {
                        Name = d.Dish.Name,
                        Description = d.Dish.Description
                    } : null).ToList()
                })
                .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, activeOrders);
        }

        /// <summary>
        /// Get open order data by table id.
        /// </summary>
        /// <param name="id">Table id</param>
        /// <returns>Order data</returns>
        [HttpPost]
        public HttpResponseMessage GetOpenOrder(int id)
        {
            var orderDIshes = _receiptManager.Get()
                .Where(x => x.Table != null && x.Table.Id == id && x.ReceiptStatus == ReceiptStatus.Open)
                .Select(x => new
                {
                    Dishes = x.ClientOrders.Select(d => Mapper.Map<DishModel>(d.Dish)),
                    OrderId = x.Id
                })
                .FirstOrDefault();

            return Request.CreateResponse(HttpStatusCode.OK, orderDIshes);
        }

        /// <summary>
        /// Get order by id and set status to closed.
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Action result</returns>
        [HttpPost]
        public HttpResponseMessage CloseOrder(int id)
        {
            var order = _receiptManager.Get(id);
            order.ReceiptStatus = ReceiptStatus.Closed;
            _receiptManager.Update(order);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private UserInfo GetCurrentUser()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);

            return user;
        }
    }
}
