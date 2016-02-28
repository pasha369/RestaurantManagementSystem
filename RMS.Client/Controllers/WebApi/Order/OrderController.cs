using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataModel.Model;
using RMS.Client.Models.View.OrderModels;

namespace RMS.Client.Controllers.WebApi.Order
{
    public class OrderController : ApiController
    {
        private IDishManager _dishManager;
        private IDataManager<DataModel.Model.Order> _orderManager;
        private IDataManager<UserInfo> _userManager;
        private IDataManager<Restaurant> _restaurantManager;
        private IDataManager<Receipt> _receiptManager; 

        public OrderController(IDishManager dishManager, IDataManager<DataModel.Model.Order> orderManager, IDataManager<UserInfo> userManager, IDataManager<Restaurant> restaurantManager, IDataManager<Receipt> receiptManager)
        {
            _dishManager = dishManager;
            _orderManager = orderManager;
            _userManager = userManager;
            _restaurantManager = restaurantManager;
            _receiptManager = receiptManager;
        }

        [HttpPost]
        public HttpResponseMessage MakeOrder(OrderItemModel order)
        {
            var receipt = new Receipt();
            var clientOrderList = new List<DataModel.Model.Order>();
            foreach (var dish in order.Dishes)
            {
                var orderEntity = new DataModel.Model.Order();
                orderEntity.Dish = _dishManager.Get(dish.Id);
                orderEntity.Restaurant = _restaurantManager.Get(order.RestaurantId);
                orderEntity.Created = DateTime.Now;

                clientOrderList.Add(orderEntity);
            }
            receipt.Client = GetUserByLogin();
            receipt.ClientOrders = clientOrderList;
            receipt.CurrentDateTime = DateTime.Now;
            receipt.ReceiptStatus = ReceiptStatus.Open;

            _receiptManager.Add(receipt);
            return this.Request.CreateResponse(HttpStatusCode.OK, receipt);
        }

        private UserInfo GetUserByLogin()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);

            return user;
        }
    }
}
