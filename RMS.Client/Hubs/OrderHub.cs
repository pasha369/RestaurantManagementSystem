using System;
using System.Linq;
using DataAccess.Abstract;
using DataAccess.Concrete.User;
using DataModel.Model;
using Microsoft.AspNet.SignalR;
using RMS.Client.Models.View.MenuModels;
using RMS.Client.Models.View.OrderModels;

namespace RMS.Client.Hubs
{
    public class OrderHub : Hub
    {
        private IDataManager<UserInfo> _userManager;
        private IDataManager<Restaurant> _restaurantManager;
        private IDataManager<Receipt> _receiptManager;
        private IDataManager<Order> _orderManager;

        public OrderHub(IDataManager<UserInfo> userManager, IDataManager<Restaurant> restaurantManager,IDataManager<Receipt> receiptManager, IDataManager<Order> orderManager)
        {
            _userManager = userManager;
            _restaurantManager = restaurantManager;
            _receiptManager = receiptManager;
            _orderManager = orderManager;
        }

        public void Send(OrderItemModel order, int tableId)
        {
            var clientManager = new ClientManager();
            var restaurant = _restaurantManager.Get(order.RestaurantId);
            var client = clientManager.Get().FirstOrDefault(x => x.UserInfo.Login == Context.User.Identity.Name);
            var table = restaurant.Halls.SelectMany(x => x.Tables).FirstOrDefault(x => x.Id == tableId);
            var user = clientManager.Get().FirstOrDefault(x => x.Restaurant.Id == order.RestaurantId);
            order.ClientName = client?.UserInfo.Name;
            Clients.User(user.UserInfo.Login).addOrderToPage(order, table.Number);
        }

        public void ChangeDishStatus(DishModel dish, int receiptId, string status)
        {
            var receipt = _receiptManager
                .Get(receiptId);
            var order = receipt.ClientOrders
                .FirstOrDefault(x => x.Dish.Name == dish.Name);
            var prevStatus = order.Status.ToString();
            order.Status = (OrderStatus) Enum.Parse(typeof(OrderStatus), status);
            _orderManager.Update(order);
            Clients.User(receipt.Client.Login).onChangeDishStatus(dish, status, prevStatus);
        }
    }
}