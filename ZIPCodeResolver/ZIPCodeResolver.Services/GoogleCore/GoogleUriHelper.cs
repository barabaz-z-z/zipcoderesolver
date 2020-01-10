using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZIPCodeResolver.Core;

namespace ZIPCodeResolver.Services.GoogleCore
{
    public sealed class GoogleUriHelper
    {
        private readonly IConfigurationService _configurationService;

        public GoogleUriHelper(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        public string GetUri(GoogleServiceTypes type, string query)
        {
            string serviceType;
            switch (type)
            {
                case GoogleServiceTypes.Geocode:
                    serviceType = "geocode";
                    break;
                case GoogleServiceTypes.Elevation:
                    serviceType = "elevation";
                    break;
                case GoogleServiceTypes.Timezone:
                    serviceType = "timezone";
                    break;
                default:
                    serviceType = "place/autocomplete";
                    break;
            }

            var host = _configurationService.GetValue("GoogleAPIHost");
            var key = _configurationService.GetValue("GoogleAPIKey");

            return $"{host}/{serviceType}/{GoogleContants.Format}?key={key}&{query}";
        }
    }
}
