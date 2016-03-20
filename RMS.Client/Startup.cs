using DataAccess.Concrete;
using DataAccess.Concrete.Order;
using DataAccess.Concrete.User;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using RMS.Client.Hubs;

[assembly: OwinStartup(typeof(RMS.Client.Startup))]

namespace RMS.Client
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register( typeof(OrderHub),
                () => new OrderHub(
                    new UserManager(),
                    new RestaurantManager(),
                    new ReceiptManager(), 
                    new OrderManager()));
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
