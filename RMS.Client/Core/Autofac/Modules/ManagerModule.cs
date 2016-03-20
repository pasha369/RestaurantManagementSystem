using Autofac;
using DataAccess.Abstract;
using DataAccess.Abstract.Menu;
using DataAccess.Concrete;
using DataAccess.Concrete.Menu;
using DataAccess.Concrete.Order;
using DataAccess.Concrete.User;
using DataModel.Model;

namespace RMS.Client.Core.Autofac.Modules
{
    public class ManagerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(r => new RestaurantManager())
                .As<IDataManager<Restaurant>>().InstancePerRequest();

            builder.Register(r => new ReviewManager())
                .As<IDataManager<Review>>().InstancePerRequest();
            builder.Register(r => new ReservationManager())
                .As<IDataManager<Reservation>>().InstancePerRequest();
            builder.Register(r => new UserManager())
                .As<IDataManager<UserInfo>>().InstancePerRequest();
            builder.Register(r => new ReservationManager())
                .As<IDataManager<Reservation>>().InstancePerRequest();
            builder.Register(r => new ClientManager())
                .As<IDataManager<ClientInfo>>().InstancePerRequest();
            builder.Register(r => new CountryManager())
                .As<IDataManager<Country>>().InstancePerRequest();
            builder.Register(r => new CuisineManager())
                .As<IDataManager<Cuisine>>().InstancePerRequest();
            builder.Register(r => new DinnerTableManager())
                .As<IDataManager<DinnerTable>>().InstancePerRequest();
            builder.Register(r => new CategoryManager())
                .As<ICategoryManager>().InstancePerRequest();
            builder.Register(r => new DishManager())
                .As<IDishManager>().InstancePerRequest();
            builder.Register(r => new MenuManager())
                .As<IMenuManager>().InstancePerRequest();
            builder.Register(r => new IngredientManager())
                .As<IManager<Ingredient>>().InstancePerRequest();
            builder.Register(r => new OrderManager())
                .As<IDataManager<Order>>().InstancePerRequest();
            builder.Register(r => new ReceiptManager())
                .As<IDataManager<Receipt>>().InstancePerRequest();
            base.Load(builder);
        }

    }
}