using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Client.Controllers.WebApi;
using RMS.Client.Core.Autofac.Modules;

namespace RMS.Client.Core.Autofac
{
    public static class AutofacConfig
    {
        public static void Config()
        {
            var builder = new ContainerBuilder();

            // Register dependencies in controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // Register dependencies in filter attributes
            builder.RegisterFilterProvider();

            // Register dependencies in custom views
            builder.RegisterSource(new ViewRegistrationSource());

            // Get your HttpConfiguration.  
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(typeof(ReservationController).Assembly);

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);


            builder.RegisterModule(new ManagerModule());

           

            var container = builder.Build();
            var webApiResolver = new AutofacWebApiDependencyResolver(container);
            // Mechanism for setting the dependency resolver for Web API and MVC is different.
            // Web API uses GlobalConfiguration.Configuration.DependencyResolver
            GlobalConfiguration.Configuration.DependencyResolver = webApiResolver;

            var mvcResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcResolver);


        }

        public static void ApiConfig(ContainerBuilder builder, IContainer container)
        {
            var configuration = GlobalConfiguration.Configuration;


            // Register API controllers using assembly scanning.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register API controller dependencies.
            builder.Register<IDataManager<Reservation>>(c => new ReservationManager()).SingleInstance();

            // Set the dependency resolver implementation.
            var resolver = new AutofacWebApiDependencyResolver(container);
            configuration.DependencyResolver = resolver;
        }
    }
}