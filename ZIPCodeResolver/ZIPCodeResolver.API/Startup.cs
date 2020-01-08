using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Mime;
using System.Web.Configuration;
using System.Web.Http;
using Newtonsoft.Json;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace ZIPCodeResolver.API
{
    public static class Startup
    {
        public static void Register(HttpConfiguration config)
        {
            var jsonTypeMapping = new RequestHeaderMapping(
                "Accept",
                MediaTypeNames.Text.Html,
                StringComparison.InvariantCultureIgnoreCase,
                true,
                "application/json");
            var jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.MediaTypeMappings.Add(jsonTypeMapping);
            jsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
