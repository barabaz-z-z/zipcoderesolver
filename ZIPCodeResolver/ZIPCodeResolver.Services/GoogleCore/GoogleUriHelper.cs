using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZIPCodeResolver.Services.GoogleCore
{
    public static class GoogleUriHelper
    {
        public static string GetUri(GoogleServiceTypes type, string query)
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

            return $"{GoogleContants.APIHost}/{serviceType}/{GoogleContants.Format}?key={GoogleContants.APIKey}&{query}";
        }
    }
}
