using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete;
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

            base.Load(builder);
        }

    }
}