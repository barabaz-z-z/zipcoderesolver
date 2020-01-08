using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using ZIPCodeResolver.Core;
using ZIPCodeResolver.Services;

namespace ZIPCodeResolver.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IConfigurationService, ConfigurationService>(Lifestyle.Singleton);
            container.Register<ICityService, CityService>(Lifestyle.Scoped);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            GlobalConfiguration.Configure(Startup.Register);
        }
    }
}
