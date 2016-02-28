using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataModel.Model;
using RMS.Client.Models.View.MenuModels;
using RMS.Client.Models.View.OrderModels;

namespace RMS.Client.Controllers.WebApi.Menu
{
    public class OrderController : ApiController
    {
        private IDishManager _dishManager;
        private IDataManager<Order> _orderManager;
        private IDataManager<UserInfo> _userManager;
        private IDataManager<Restaurant> _restaurantManager;

        public OrderController(IDishManager dishManager, IDataManager<Order> orderManager, IDataManager<UserInfo> userManager, IDataManager<Restaurant> restaurantManager)
        {
            _dishManager = dishManager;
            _orderManager = orderManager;
            _userManager = userManager;
            _restaurantManager = restaurantManager;
        }

        [HttpPost]
        public void AddDishToOrder(OrderItemModel order)
        {
            var orderEntity = new Order();
            orderEntity.Dish = Mapper.Map<Dish>(order.Dish);
            orderEntity.Restaurant = _restaurantManager.Get(order.RestaurantId);
            orderEntity.Created = DateTime.Now;

            _orderManager.Add(orderEntity);
        }

        private UserInfo GetUserByLogin()
        {
            var login = System.Web.HttpContext.Current.User.Identity.Name;
            var user = _userManager.Get().FirstOrDefault(u => u.Login == login);

            return user;
        }
    }
}
