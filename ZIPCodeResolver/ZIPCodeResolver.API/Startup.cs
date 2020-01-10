using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Mime;
using System.Web.Http;

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


            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
