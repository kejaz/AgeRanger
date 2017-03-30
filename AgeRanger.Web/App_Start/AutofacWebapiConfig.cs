using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AgeRanger.Data.Infrastructure;
using AgeRanger.Data.Repositories;
using AgeRanger.Services;
using Autofac;
using Autofac.Integration.WebApi;

namespace AgeRanger.Web.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;
        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerRequest();
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerRequest();
            builder.RegisterType<AgeGroupRepository>().As<IAgeGroupRepository>().InstancePerRequest();
            builder.RegisterType<AgeGroupService>().As<IAgeGroupService>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}